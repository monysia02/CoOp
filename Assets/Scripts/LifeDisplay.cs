using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    public Image[] hearts; 
    private int maxLives = 3;

    public void UpdateLives(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentLives; 
        }
    }

    public void ResetLives()
    {
        UpdateLives(maxLives);
    }
}
