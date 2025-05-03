using System.Collections;
using UnityEngine;

public class KillZoneChecker : MonoBehaviour
{
    private CharacterDeathHandler deathHandler;
    private bool hasDied = false;
    private Renderer rend;

    private void Awake()
    {
        deathHandler = GetComponent<CharacterDeathHandler>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (hasDied || deathHandler == null || rend == null || deathHandler.IsDead()) return;

        // Oblicz dolną granicę kamery
        float camBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;

        // Oblicz dolną granicę gracza (jego sprite)
        float playerBottom = rend.bounds.min.y;

        if (playerBottom < camBottom)
        {
            hasDied = true;
            Vector3 respawnPos = new Vector3(
                transform.position.x,
                Camera.main.transform.position.y - Camera.main.orthographicSize + 2.5f, //o ile do góry w przypadku śmierci
                transform.position.z
            );

            StartCoroutine(RespawnAndDie(respawnPos));
        }
    }

    private IEnumerator RespawnAndDie(Vector3 positionAboveCamera)
    {
        // Teleportuj nad kamerę (ale jeszcze nie zabijaj)
        transform.position = positionAboveCamera;

        yield return null; // 1 frame delay

        deathHandler.TriggerDeath();
    }
}
