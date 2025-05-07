using UnityEngine;

public class CageSound : MonoBehaviour
{
    public AudioClip disappearSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDisappearSound()
    {
        if (disappearSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(disappearSound);
        }
    }
}
