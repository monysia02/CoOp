using System.Collections;
using UnityEngine;

public class PointCollector : MonoBehaviour
{
    public static PointCollector Instance;

    private int ladybugPoints = 0;
    private int catPoints = 0;
    
    
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private int pointsToMoveCamera = 10;
    [SerializeField] private float cameraMoveAmount = 5f;
    [SerializeField] private float cameraMoveDuration = 1f;

    private bool cameraMoved = false;
    
    
    private float activeKillZoneY = float.NegativeInfinity;
    private bool killZoneActive = false;
    
    public PointsUI ladybugUI;
    public PointsUI catUI;
    
    
    public bool IsKillZoneActive()
    {
        return killZoneActive;
    }

    public float GetKillZoneY()
    {
        return activeKillZoneY;
    }
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddLadybugPoints(int amount)
    {
        ladybugPoints += amount;
        Debug.Log($"Ladybug: {ladybugPoints}");
        ladybugUI?.UpdatePoints(ladybugPoints);
        TryMoveCamera();
    }

    public void AddCatPoints(int amount)
    {
        catPoints += amount;
        Debug.Log($"Cat: {catPoints}");
        catUI?.UpdatePoints(catPoints);
        TryMoveCamera();
    }
    
    private void TryMoveCamera()
    {
        if (cameraMoved) return;

        if (ladybugPoints >= pointsToMoveCamera && catPoints >= pointsToMoveCamera)
        {
            cameraMoved = true;
            StartCoroutine(MoveCameraUp());
        }
    }
    
    private IEnumerator MoveCameraUp()
    {
        Vector3 startPos = cameraTransform.position;
        Vector3 targetPos = startPos + new Vector3(0, cameraMoveAmount, 0);
        float elapsed = 0f;

        while (elapsed < cameraMoveDuration)
        {
            cameraTransform.position = Vector3.Lerp(startPos, targetPos, elapsed / cameraMoveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = targetPos;
        
        activeKillZoneY = startPos.y - cameraMoveAmount;
        killZoneActive = true;
    }
    
    public int GetLadybugPoints()
    {
        return ladybugPoints;
    }
    
    public bool HasEnoughPoints(int required)
    {
        return ladybugPoints >= required && catPoints >= required;
    }
    
}
