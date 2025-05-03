using UnityEngine;

public class GetCheckpoint : MonoBehaviour
{
    public enum Owner { Ladybug, Cat }
    public Owner belongsTo;

    public Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (belongsTo == Owner.Ladybug && other.GetComponent<LadyBugController>() != null)
        {
            CheckpointManager.Instance.SetCheckpoint_Ladybug(respawnPoint.position);
            Destroy(gameObject); // albo animacja znikania
        }
        else if (belongsTo == Owner.Cat && other.GetComponent<CatPlayerController>() != null)
        {
            CheckpointManager.Instance.SetCheckpoint_Cat(respawnPoint.position);
            Destroy(gameObject);
        }
    }
}
