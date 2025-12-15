using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private GameObject sewingMachine;

    [Header("Velocidad")]
    private Vector2 direccion = Vector2.right;
    [SerializeField]private float velocidad = 2f; // más alto = parece que corre

    [Header("Fade")]
    [SerializeField] private float tiempoFade = 1.2f;

    [Header("Efecto carrera fake")]
    [SerializeField] private float frecuenciaBounce = 20f;
    [SerializeField] private float intensidadBounce = 0.05f;

    private SpriteRenderer sr;
    private Vector3 posicionInicial;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        posicionInicial = transform.position;
        if (InventoryManager.Instance.hasGone)
        {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator SalidaNPC()
    {
        float t = 0;

        while (t < tiempoFade)
        {
            t += Time.deltaTime;

            // Movimiento rápido
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime);

            // Bounce vertical (simula pasos)
            float bounce = Mathf.Sin(Time.time * frecuenciaBounce) * intensidadBounce;
            transform.position = new Vector3(
                transform.position.x,
                posicionInicial.y + bounce,
                transform.position.z
            );

            // Fade out
            float alpha = Mathf.Lerp(1f, 0f, t / tiempoFade);
            sr.color = new Color(1f, 1f, 1f, alpha);

            yield return null;
        }

        gameObject.SetActive(false);
        InventoryManager.Instance.hasGone = true;
        InventoryManager.Instance.sewingMachineEnabled = true;
        sewingMachine.GetComponent<EnableInteractiveness>().EnableGameObjects();
    }
}
