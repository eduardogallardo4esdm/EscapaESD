using UnityEngine;

public class SpawnPointPlayer : MonoBehaviour
{
    [Tooltip("Este ID debe coincidir con el connectionPointID de la puerta de la escena anterior")]
    public string pointID;

    private void Awake()
    {
        // Cuando la escena inicia, verificamos si este es el punto correcto
        // Comparamos con la variable estática del script anterior
        if (PortalEntreEscenas.spawnPointID == pointID)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                player.transform.position = transform.position;
            }
        }
    }
}
