using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class EndScreenMusicPlayer : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.Play();
        StartCoroutine(WaitForMusicEnd());
    }

    private System.Collections.IEnumerator WaitForMusicEnd()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
