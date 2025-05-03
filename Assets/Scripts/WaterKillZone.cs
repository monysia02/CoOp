using UnityEngine;

public class WaterKillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var deathHandler = other.GetComponent<CharacterDeathHandler>();
        if (deathHandler != null && !deathHandler.IsDead())
        {
            deathHandler.TriggerDeath();
        }
    }
}
