using UnityEngine;

public class MushroomClickSound : MonoBehaviour
{
    public AudioClip clickSound;

    private AudioSource audioSource;
    private bool alreadyPressed = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        if (clickSound != null && !alreadyPressed)
        {
            audioSource.PlayOneShot(clickSound);
            alreadyPressed = true;
        }
    }

    public void ResetClickSound()
    {
        alreadyPressed = false;
    }
}
