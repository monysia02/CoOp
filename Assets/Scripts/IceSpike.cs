using UnityEngine;

public class IceSpike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var deathHandler = collision.gameObject.GetComponent<CharacterDeathHandler>();
        if (deathHandler != null)
        {
            deathHandler.TriggerDeath();
        }
    }
    
    public void EnableDamage()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void DisableDamage()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
