using UnityEngine;

public class DoorSoundPlayer : MonoBehaviour
{
    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDoorOpenSound()
    {
        if (audioSource != null && openSound != null)
            audioSource.PlayOneShot(openSound);
    }

    public void PlayDoorCloseSound()
    {
        if (audioSource != null && closeSound != null)
            audioSource.PlayOneShot(closeSound);
    }
}
