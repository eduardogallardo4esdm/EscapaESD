using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (string.IsNullOrEmpty(PortalEntreEscenas.spawnPointID))
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        SpawnPointPlayer[] points =
        FindObjectsByType<SpawnPointPlayer>(FindObjectsSortMode.None);

        foreach (var point in points)
        {
            if (point.pointID == PortalEntreEscenas.spawnPointID)
            {
                player.transform.position = point.transform.position;
                break;
            }
        }

        // IMPORTANTE: limpiar después de usar
        PortalEntreEscenas.spawnPointID = null;
    }
}
