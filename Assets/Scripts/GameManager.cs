using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static string lastGameScene;
    
    [Header("Game Over Settings")]
    public AudioClip gameOverSound;
    public float delayBeforeGameOverScene = 1f;
    
    private void Awake()
    {    
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void CheckGameOver(int ladybugLives, int catLives)
    {
        lastGameScene = SceneManager.GetActiveScene().name;
        
        if (ladybugLives <= 0 || catLives <= 0)
        {
            StartCoroutine(HandleGameOver());
        }
    }
    
    private IEnumerator HandleGameOver()
    {
        AdditionalSoundsManager.Instance.PlayGameOver(gameOverSound);

        yield return new WaitForSeconds(delayBeforeGameOverScene);

        SceneManager.LoadScene("GameOverScene");
    }
    

    
}
