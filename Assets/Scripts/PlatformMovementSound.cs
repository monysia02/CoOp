using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlatformMovementSound : MonoBehaviour
{
    public float minMovementSpeed = 0.01f;
    public float fadeOutTime = 0.5f;
    public float maxVolume = 1f;

    private Vector3 lastPosition;
    private AudioSource audioSource;
    private bool isFadingOut = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        lastPosition = transform.position;
    }

    void Update()
    {
        float movement = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        if (movement > minMovementSpeed)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.volume = maxVolume;
                audioSource.Play();
            }
            else if (isFadingOut)
            {
                StopAllCoroutines();
                audioSource.volume = maxVolume;
                isFadingOut = false;
            }
        }
        else
        {
            if (audioSource.isPlaying && !isFadingOut)
            {
                StartCoroutine(FadeOutSound());
            }
        }
    }

    System.Collections.IEnumerator FadeOutSound()
    {
        isFadingOut = true;
        float startVolume = audioSource.volume;
        float t = 0f;

        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutTime);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = maxVolume;
        isFadingOut = false;
    }
}
