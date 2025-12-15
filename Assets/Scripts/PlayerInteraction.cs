using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Config")]
    [SerializeField] private NPCDialogue actualNPC;
    [SerializeField] private Button NPCButton;
    [SerializeField] private ButtonBehaviour buttonBehaviour;

    private bool canInteract = false;
    private bool inRange = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("NPC"))
        {
            canInteract = true;
            actualNPC = collision.GetComponent<NPCDialogue>();

            // Selecci�n expl�cita del bot�n correcto
            Transform buttonTransform = collision.transform.Find("Canvas/Button");
            if (buttonTransform != null)
            {
                NPCButton = buttonTransform.GetComponent<Button>();
                buttonBehaviour = buttonTransform.GetComponent<ButtonBehaviour>();
                buttonBehaviour.ButtonEnable(true);

                NPCButton.onClick.RemoveAllListeners();
                NPCButton.onClick.AddListener(Interact);
            }
        }
    else if(collision.CompareTag("Blink"))
    {
        inRange = true;
        actualNPC = collision.GetComponentInParent<NPCDialogue>();

        Transform buttonTransform = actualNPC.transform.Find("Canvas/Button");
        if (buttonTransform != null)
        {
            NPCButton = buttonTransform.GetComponent<Button>();
            buttonBehaviour = buttonTransform.GetComponent<ButtonBehaviour>();
            buttonBehaviour.ButtonEnable(true);

            NPCButton.onClick.RemoveAllListeners();
            NPCButton.onClick.AddListener(Interact);
        }
        
        if(actualNPC.name == "Directora" && !InventoryManager.Instance.documentsUsed)
        {
            // Detener al jugador completamente
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;

            this.gameObject.GetComponent<PlayerController>().Stop();
            // Empujar al jugador hacia atr�s
            Vector2 pushDirection = (transform.position - actualNPC.transform.position).normalized;
            transform.position += (Vector3)pushDirection * 1.5f; // distancia del empuj�n

            // Opcional: desactivar interacci�n
            canInteract = false;
            inRange = false;
            actualNPC.Interact(this.gameObject);
        }
    }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.CompareTag("NPC"))
        {
            canInteract = false;
            actualNPC = null;

        }
        else if(collision.CompareTag("Blink"))
        {
            inRange = false;
            if (buttonBehaviour != null)
                buttonBehaviour.ButtonEnable(false);

            NPCButton = null;
            actualNPC = null; ;
        }
    }

    public void Interact()
    {
        if (NPCButton != null)
        NPCButton.interactable = false;

        if (!canInteract)
        {
            if (inRange)
            {
                UIManager.Instance.CantReach();
                StartCoroutine(EnableButtonAfterDelay(2f));
            }
            return;
        }

        // CASO ESPECIAL: Encargado de taller
        if (actualNPC.name == "EncargadoTaller" || actualNPC.name == "EstudianteTaller")
        {
            if (actualNPC.GetComponent<AudioSource>().isPlaying == true)
            {
                UIManager.Instance.CantHear();
                StartCoroutine(EnableButtonAfterDelay(2f));
                return;
            }
        }

        // Interacción normal
        actualNPC.Interact(this.gameObject);
    }

    public void EnableButton()
    {
        if (NPCButton == null) return;
        NPCButton.interactable = true;
    }

    private IEnumerator EnableButtonAfterDelay(float delay)
    {
        if (NPCButton != null)
            NPCButton.interactable = false;

        yield return new WaitForSecondsRealtime(delay);

        if (NPCButton != null)
            NPCButton.interactable = true;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // IMPORTANT�SIMO: borrar referencias antiguas (eran del NPC de la escena anterior)
        actualNPC = null;
        NPCButton = null;
        buttonBehaviour = null;
        canInteract = false;
        inRange = false;

        // Ahora deja que el Trigger vuelva a asignar cosas
        // PERO solo si realmente entras en el trigger
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
