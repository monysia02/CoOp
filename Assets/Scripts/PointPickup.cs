using UnityEngine;

public class PointPickup : MonoBehaviour
{
    public enum Owner { Ladybug, Cat }
    public Owner belongsTo;

    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        bool isLadybug = belongsTo == Owner.Ladybug && other.GetComponent<LadyBugController>() != null;
        bool isCat = belongsTo == Owner.Cat && other.GetComponent<CatPlayerController>() != null;

        if (isLadybug || isCat)
        { 
            AdditionalSoundsManager.Instance.PlayPointSound(pickupSound);
            
            if (isLadybug)
                PointCollector.Instance.AddLadybugPoints(1);
            else
                PointCollector.Instance.AddCatPoints(1);

            Destroy(gameObject);
        }
    }
}
