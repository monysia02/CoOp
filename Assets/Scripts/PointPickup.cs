using UnityEngine;

public class PointPickup : MonoBehaviour
{
    public enum Owner { Ladybug, Cat }
    public Owner belongsTo;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.CompareTag("Player")) return;

        if (belongsTo == Owner.Ladybug && other.GetComponent<LadyBugController>() != null)
        {
            PointCollector.Instance.AddLadybugPoints(1);
            Destroy(gameObject);
        }
        else if (belongsTo == Owner.Cat && other.GetComponent<CatPlayerController>() != null)
        {
            PointCollector.Instance.AddCatPoints(1);
            Destroy(gameObject);
        }
    }
}
