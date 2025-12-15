using UnityEngine;
using UnityEngine.UI;

public class InteractiveBrightness : MonoBehaviour
{
    [Header("Velocidad del parpadeo")]
    [SerializeField] private float blinkSpeed = 4f;  

    [Header("Límites de opacidad")]
    [Range(0f, 1f)] [SerializeField] private float minAlpha = 0.1f;
    [Range(0f, 1f)] [SerializeField] private float maxAlpha = 1f;

    [Header ("Distancia al player")]
    private Collider2D _collider;

    private Renderer rend;
    private Color originalColor;
    private float timer = 0f;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        _collider = GetComponent<Collider2D>();
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime * blinkSpeed;

        // Oscila entre minAlpha y maxAlpha usando una onda seno
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(timer) + 1f) / 2f);

        Color newColor = originalColor;
        newColor.a = alpha;

        rend.material.color = newColor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
