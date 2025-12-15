using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalEntreEscenas : MonoBehaviour
{
    [Header("Configuración de Viaje")]
    [Tooltip("Nombre exacto de la escena a cargar")]
    [SerializeField] private string escenaACargar;

    [Tooltip("ID único de conexión (ej: PuertaNorte)")]
    [SerializeField] private string connectionPointID;

    public static string spawnPointID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Interactuar();
        }
    }

    private void Interactuar()
    {
        // Guardamos el ID para que la otra escena sepa dónde spawnear
        spawnPointID = connectionPointID;
        SceneManager.LoadScene(escenaACargar);
    }
}
