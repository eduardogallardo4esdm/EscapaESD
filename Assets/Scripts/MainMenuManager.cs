using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject controlsMenu;

    private void Awake()
    {
        DestroyAllPersistentObjects();
        controlsMenu.SetActive(false);
    }

    public void NewGame()
    {
        DestroyAllPersistentObjects();
        SceneManager.LoadScene(sceneToLoad);
    }
    
    private void Update ()
    {
        if ( Mouse.current.leftButton.wasPressedThisFrame && controlsMenu.activeSelf)
        {
            controlsMenu.SetActive(false);
        }
    }

    public void ShowControls()
    {
        controlsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DestroyAllPersistentObjects()
    {
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in allObjects)
        {
            if (obj.scene.name == null || obj.scene.name == "DontDestroyOnLoad")
            {
                if (obj != gameObject)
                {
                    Destroy(obj);
                }
            }
        }
    }
}
