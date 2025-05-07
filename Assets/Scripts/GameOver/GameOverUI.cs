using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RestartGame()
    {
        StartCoroutine(RestartRoutine());
    }

    private IEnumerator RestartRoutine()
    {
        ClearPersistentObjects();
        
        yield return null;

        string sceneToLoad = GameManager.lastGameScene ?? "SampleScene";
        SceneManager.LoadScene(sceneToLoad);
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    private void ClearPersistentObjects()
    {
        foreach (GameObject obj in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (obj.scene.buildIndex == -1)
            {
                Destroy(obj);
            }
        }
    }
}
