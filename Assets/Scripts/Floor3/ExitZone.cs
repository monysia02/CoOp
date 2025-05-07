using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitZone : MonoBehaviour
{
    public Animator doorAnimator;

    [Header("WinSound")]
    public AudioClip victorySound;
    public float delayBeforeSceneLoad = 1f;
    public string nextSceneName = "EndScreen";

    private bool ladybugIn = false;
    private bool catIn = false;
    private bool gameEnding = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!doorAnimator.GetBool("isOpen")) return;

        if (other.GetComponent<LadyBugController>() != null)
            ladybugIn = true;

        if (other.GetComponent<CatPlayerController>() != null)
            catIn = true;

        CheckForWin();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<LadyBugController>() != null)
            ladybugIn = false;

        if (other.GetComponent<CatPlayerController>() != null)
            catIn = false;
    }

    private void CheckForWin()
    {
        if (ladybugIn && catIn && !gameEnding)
        {
            gameEnding = true;
            Debug.Log("Oboje weszli do drzwi – KONIEC GRY");

            StartCoroutine(HandleEndSequence());
        }
    }
    
    private IEnumerator HandleEndSequence()
    {
        var ladybug = FindObjectOfType<LadyBugController>()?.gameObject;
        var cat = FindObjectOfType<CatPlayerController>()?.gameObject;

        if (victorySound != null)
            AudioSource.PlayClipAtPoint(victorySound, transform.position);

        if (ladybug != null)
            StartCoroutine(FadeOutAndVanish(ladybug));
        if (cat != null)
            StartCoroutine(FadeOutAndVanish(cat));

        yield return new WaitForSeconds(delayBeforeSceneLoad);

        SceneManager.LoadScene(nextSceneName);
    }
    
    private IEnumerator FadeOutAndVanish(GameObject obj)
    {
        float duration = 0.5f;
        float elapsed = 0f;

        var sr = obj.GetComponent<SpriteRenderer>();
        var rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;
        
        var controller = obj.GetComponent<MonoBehaviour>();
        if (controller != null) controller.enabled = false;

        Color startColor = sr.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        sr.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        obj.SetActive(false);
    }
}
