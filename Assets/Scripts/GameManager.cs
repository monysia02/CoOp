using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CheckGameOver(int ladybugLives, int catLives)
    {
        if (ladybugLives <= 0 || catLives <= 0)
        {
            Debug.Log("GAME OVER!");
            Time.timeScale = 0f;
            //na przyszłość, aby dodać GameOverPanel.SetActive(true);
        }
    }
}
