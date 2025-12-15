using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Para poder instanciar

    [Header("Player")]
    [SerializeField] private GameObject Player;
    [Header("PanelTexto")]
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI NPCDialogue;
    [SerializeField] private GameObject textPanel;
    [Header("Inventario")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject EmptyBottle;
    [SerializeField] private GameObject WaterBottle;
    [SerializeField] private GameObject WaterBottleUsed;
    [SerializeField] private GameObject Computer;
    [SerializeField] private GameObject Documents;
    [SerializeField] private GameObject DocumentsUsed;
    [SerializeField] private GameObject Needle;
    [SerializeField] private GameObject NeedleUsed;
    [SerializeField] private GameObject Lockpick;
    [SerializeField] private GameObject LockpickUsed;
    [SerializeField] private GameObject Card;

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
        // Busca al nuevo Player en la nueva escena
        Player = GameObject.FindWithTag("Player");
    }

    private void Awake()
    {
        if(Instance == null)
        { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
        EmptyBottle.SetActive(false); WaterBottle.SetActive(false); WaterBottleUsed.SetActive(false); Computer.SetActive(false); Documents.SetActive(false);
        DocumentsUsed.SetActive(false); Needle.SetActive(false); NeedleUsed.SetActive(false); Lockpick.SetActive(false); LockpickUsed.SetActive(false); Card.SetActive(false);

    }

    public void Start()
    {
        textPanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }

    private void Update()
    {
        if (textName.text == "Tú " && Player.GetComponent<PlayerController>().enabled == false && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HideDialogue();
        }
    }

    public void ShowDialogue(string nameNPC,string dialogueNPC)
    {
        textName.text = nameNPC;
        NPCDialogue.text = dialogueNPC;
        textPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        PauseGame();
    }

    public void HideDialogue()
    {
        ResumeGame();
        textPanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }

    public void CantReach()
    {
        textName.text = "Tú ";
        NPCDialogue.text = "Estoy demasiado lejos";
        inventoryPanel.SetActive(false);
        textPanel.SetActive(true);
        PauseGame();
    }

    public void CantHear()
    {
        textName.text = "Tú ";
        NPCDialogue.text = "No escucho nada";
        inventoryPanel.SetActive(false);
        textPanel.SetActive(true);
        PauseGame();
    }

    public void PauseGame()
    {
        Player.GetComponent<PlayerController>().enabled = false;
    }

    public void ResumeGame()
    {
        Player.GetComponent<PlayerController>().enabled = true;
    }

    public void Items(string itemcommand)
    {
        if(itemcommand == "GetEmptyBottle")
        {
            EmptyBottle.SetActive(true);
        }
        else if(itemcommand == "GetWaterBottle")
        {
            WaterBottle.SetActive(true);
        }
        else if(itemcommand == "UseWaterBottle")
        {
            WaterBottleUsed.SetActive(true);
        }
        else if(itemcommand == "GetComputer")
        {
            Computer.SetActive(true);
        }
        else if(itemcommand == "GetDocuments")
        {
            Documents.SetActive(true);
        }
        else if(itemcommand == "UseDocuments")
        {
            DocumentsUsed.SetActive(true);
        }
        else if(itemcommand == "GetNeedle")
        {
            Needle.SetActive(true);
        }
        else if(itemcommand == "UseNeedle")
        {
            NeedleUsed.SetActive(true);
        }
        else if(itemcommand == "GetLockpick")
        {
            Lockpick.SetActive(true);
        }
        else if(itemcommand == "UseLockpick")
        {
            LockpickUsed.SetActive(true);
        }
        else if(itemcommand == "GetCard")
        {
            Card.SetActive(true);
        }
    }

}
