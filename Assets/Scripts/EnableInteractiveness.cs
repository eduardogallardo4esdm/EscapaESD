using UnityEngine;

public class EnableInteractiveness : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject blink;

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void Start()
    {
        // Estado inicial
        DisableAll();

        if (InventoryManager.Instance.sewingMachineEnabled)
        {
            EnableGameObjects();
        }
    }

    private void DisableAll()
    {
        col.enabled = false;
        blink.SetActive(false);
        canvas.SetActive(false);
    }
    public void EnableGameObjects()
    {
        col.enabled = true;
        blink.SetActive(true);
        canvas.SetActive(true);
    }
}
