using UnityEngine;

public class AdditionalSoundsManager : MonoBehaviour
{
    public static AdditionalSoundsManager Instance;

    [Header("Audio Sources")]
    public AudioSource jumpSource;
    public AudioSource runSource;
    public AudioSource damageSource;
    public AudioSource pointSource;
    public AudioSource gameOverSource;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Punkty
    public void PlayPointSound(AudioClip clip)
    {
        if (clip != null && pointSource != null)
        {
            pointSource.PlayOneShot(clip);
        }
    }
    
    
    // Skakanie
    public void PlayJump(AudioClip clip)
    {
        if (clip != null && jumpSource != null)
            jumpSource.PlayOneShot(clip);
    }

    
    // Bieganie
    public void PlayRunLoop(AudioClip clip)
    {
        if (clip != null && runSource != null)
        {
            runSource.clip = clip;
            runSource.loop = true;
            runSource.Play();
        }
    }

    public void StopRunLoop()
    {
        if (runSource != null && runSource.isPlaying)
            runSource.Stop();
    }
    
    
    // Obrażenia
    public void PlayDamage(AudioClip clip)
    {
        if (clip != null && damageSource != null)
            damageSource.PlayOneShot(clip);
    }
    
    
    // GameOver
    public void PlayGameOver(AudioClip clip)
    {
        if (clip != null && gameOverSource != null)
            gameOverSource.PlayOneShot(clip);
    }
}
