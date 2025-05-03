using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    void Start()
    {
        UpdatePoints(PointCollector.Instance.GetLadybugPoints());
    }

    public void UpdatePoints(int value)
    {
        pointsText.text = value.ToString();
    }
}
