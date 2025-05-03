using UnityEngine;

public class PointBounce : MonoBehaviour
{
    public float bounceHeight = 0.3f;
    public float bounceSpeed = 2f;

    private Vector3 startPos;
    private float offset;

    private void Start()
    {
        startPos = transform.position;
        offset = Random.Range(0f, 2f * Mathf.PI); // przesunięcie fazowe
    }

    private void Update()
    {
        float newY = Mathf.Sin(Time.time * bounceSpeed + offset) * bounceHeight;
        transform.position = startPos + new Vector3(0, newY, 0);
    }
}
