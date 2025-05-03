using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector3? ladybugCheckpoint = null;
    private Vector3? catCheckpoint = null;

    [SerializeField] private Transform ladybugStartPoint;
    [SerializeField] private Transform catStartPoint;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetCheckpoint_Ladybug(Vector3 pos)
    {
        ladybugCheckpoint = pos;
    }

    public void SetCheckpoint_Cat(Vector3 pos)
    {
        catCheckpoint = pos;
    }

    public Vector3 GetRespawnPosition_Ladybug()
    {
        return ladybugCheckpoint ?? ladybugStartPoint.position;
    }

    public Vector3 GetRespawnPosition_Cat()
    {
        return catCheckpoint ?? catStartPoint.position;
    }
}
