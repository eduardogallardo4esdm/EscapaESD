using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    [Header("NPC Info")]
    [SerializeField] private string nameNPC = "NPC";
    [SerializeField] private GameObject outroManager;

    [Header("Antes de interactuar con objeto")]
    [TextArea(3, 5)]
    [SerializeField] private string[] dialogueBefore;

    [Header("Diálogo de interacción")]
    [TextArea(3, 5)]
    [SerializeField] private string[] dialogueUsing;

    [Header("Diálogo post interacción")]
    [TextArea(3, 5)]
    [SerializeField] private string[] dialogueAfterUsed;

    private int dialogueIndex = 0;
    private bool isTalking = false;
    private GameObject currentPlayer;

    private string[] currentDialogue;
    private bool RewardThisDialogue = false;

    //Se llama UNA VEZ al iniciar conversación
    public void Interact(GameObject player)
    {
        currentPlayer = player;
        dialogueIndex = 0;
        isTalking = true;

        SelectDialogueByState();
        ShowCurrentLine();
    }

    private void Update()
    {
        if (!isTalking) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            dialogueIndex++;

            if (dialogueIndex >= currentDialogue.Length)
            {
                EndDialogue();
            }
            else
            {
                ShowCurrentLine();
            }
        }
    }

    //AQUÍ SE DECIDE QUÉ DIÁLOGO USAR
    private void SelectDialogueByState()
    {
        var inventory = InventoryManager.Instance;
        if(nameNPC == "Tú   ")
        {
            //DIÁLOGO PREVIO
            if (inventory.hasDocuments)
            {
                currentDialogue = dialogueAfterUsed;
                RewardThisDialogue = false;
                return;
            }

            //DIÁLOGO DE RECOMPENSA
            if (inventory.hasPhotoshop && inventory.hasKnowledge)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }

            //DIÁLOGO POST RECOMPENSA
            currentDialogue = dialogueAfterUsed;
            RewardThisDialogue = false;

            currentDialogue = dialogueBefore;
            RewardThisDialogue = false;
        }
        else if (nameNPC == "Recepcionista")
        {
            if (!inventory.hasComputer)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }

            //DIÁLOGO POST RECOMPENSA
            currentDialogue = dialogueAfterUsed;
            RewardThisDialogue = false;
        }
        else if (nameNPC == "Directora")
        {
            if (!inventory.hasDocuments)
            {
                currentDialogue = dialogueBefore;
                RewardThisDialogue = false;
                return;
            }
            if (inventory.hasDocuments && !inventory.documentsUsed)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }
            //DIÁLOGO POST RECOMPENSA
            currentDialogue = dialogueAfterUsed;
            RewardThisDialogue = false;
        }
        else if (nameNPC == "Tú    ")
        {
            if(!inventory.hasLockpick || !inventory.waterBottleUsed)
            {
                currentDialogue = dialogueBefore;
                RewardThisDialogue = false;
                return;
            }
            if(inventory.hasLockpick && !inventory.lockipckUsed && inventory.waterBottleUsed)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }
            //DIÁLOGO POST RECOMPENSA
            currentDialogue = dialogueAfterUsed;
            RewardThisDialogue = false;
        }
        else if (nameNPC == "Tú     ")
        {
            if(!inventory.hasWaterBottle && inventory.hasTried && inventory.hasEmptyBottle && inventory.hasTried2)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }
            if(!inventory.hasEmptyBottle || !inventory.hasTried || !inventory.hasTried2)
            {
                currentDialogue = dialogueBefore;
                RewardThisDialogue = false;
                return;
            }
            //DIÁLOGO POST RECOMPENSA
            currentDialogue = dialogueAfterUsed;
            RewardThisDialogue = false;
        }
        else if (nameNPC == "Tú      ")
        {
            if (!inventory.hasTried)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }
            //DIÁLOGO POST RECOMPENSA
            currentDialogue = dialogueAfterUsed;
            RewardThisDialogue = false;
        }
        else if (nameNPC == "Profesor")
        {
            if (inventory.hasEmptyBottle && (!inventory.hasTried || !inventory.hasTried2))
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = false;
                return;
            }
            if (inventory.hasEmptyBottle && inventory.hasTried && inventory.hasTried2)
            {
                currentDialogue = dialogueAfterUsed;
                RewardThisDialogue = true;
                return;
            }
            currentDialogue = dialogueBefore;
            RewardThisDialogue = true;
        }
        else if (nameNPC == "Tú       ")
        {
            if (inventory.hasSewingMachine)
            {
                currentDialogue = dialogueAfterUsed;
                RewardThisDialogue = false;
                return;
            }
            currentDialogue = dialogueUsing;
            RewardThisDialogue = true;
        }
        else if (nameNPC == "Estudiante")
        {
            if (inventory.isRat)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }
            currentDialogue = dialogueBefore;
            RewardThisDialogue = false;
        }
        else if (nameNPC == "Tú        ")
        {
            if (inventory.isRat)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = false;
                return;
            }
            if (inventory.hasGone)
            {
                currentDialogue = dialogueAfterUsed;
                RewardThisDialogue = false;
                return;
            }
            currentDialogue = dialogueBefore;
            RewardThisDialogue = true;
        }
        else if (nameNPC == "Tú         ")
        {
            if (!inventory.hasTried2)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }
            //DIÁLOGO POST RECOMPENSA
            currentDialogue = dialogueAfterUsed;
            RewardThisDialogue = false;
        }
        else if (nameNPC == "Encargado de taller")
        {
            if(inventory.hasLockpick)
            {
                currentDialogue = dialogueAfterUsed;
                RewardThisDialogue = false;
                return;
            }
            if(inventory.hasSewingMachine && !inventory.sewingMachineUsed)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }
            currentDialogue = dialogueBefore;
            RewardThisDialogue = false;
        }
        else if (nameNPC == "Tú          ")
        {
            if(inventory.hasDocuments)
            {
                currentDialogue = dialogueAfterUsed;
                RewardThisDialogue = false;
                return;
            }
            if(inventory.hasComputer)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }
            currentDialogue = dialogueBefore;
            RewardThisDialogue = false;
        }
        else if (nameNPC == "Tú           ")
        {
            if (inventory.hasTeacherCard)
            {
                currentDialogue = dialogueUsing;
                RewardThisDialogue = true;
                return;
            }
            currentDialogue = dialogueBefore;
            RewardThisDialogue = false;
        }
        else
        {
            currentDialogue = dialogueBefore;
            RewardThisDialogue = false;
        }
    }

    private void ShowCurrentLine()
    {
        currentPlayer.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        currentPlayer.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        currentPlayer.GetComponent<PlayerController>().Stop();
        UIManager.Instance.ShowDialogue(nameNPC, currentDialogue[dialogueIndex]);
    }

    private void EndDialogue()
    {
        if (nameNPC == "Tú   ")
        {
            //SE RECLAMA LA RECOMPENSA
            if (RewardThisDialogue)
            {
                InventoryManager.Instance.Documents();
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Recepcionista")
        {
            if (RewardThisDialogue)
            {
                InventoryManager.Instance.Computer();
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Directora")
        {
            if (RewardThisDialogue)
            {
                InventoryManager.Instance.DocumentsUsed();
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Tú    ")
        {
            if (RewardThisDialogue)
            {
                InventoryManager.Instance.LockpickUsed();
                InventoryManager.Instance.TeacherCard();
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Tú     ")
        {
            if (RewardThisDialogue)
            {
                InventoryManager.Instance.WaterBottle();
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Tú      ")
        {
            if (RewardThisDialogue)
            {
                InventoryManager.Instance.hasTried = true;
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Profesor")
        {
            if (RewardThisDialogue)
            {
                if (!InventoryManager.Instance.hasEmptyBottle)
                {
                    InventoryManager.Instance.EmptyBottle();
                    RewardThisDialogue = false;
                }
                else if (InventoryManager.Instance.hasWaterBottle)
                {
                    InventoryManager.Instance.WaterBottleUsed();
                    RewardThisDialogue = false;
                }
                else
                {
                    RewardThisDialogue = false;
                }
            }
        }
        else if (nameNPC == "Profesora")
        {
            if (!InventoryManager.Instance.hasKnowledge)
            {
                InventoryManager.Instance.hasKnowledge = true;
                RewardThisDialogue = false;
            }
            else
            {
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Tú       ")
        {
            if (RewardThisDialogue)
            {
                InventoryManager.Instance.SewingMachine();
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Estudiante")
        {
            if (RewardThisDialogue)
            {
                StartCoroutine(this.gameObject.GetComponent<FadeOut>().SalidaNPC());
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Tú        ")
        {
            if (RewardThisDialogue)
            {
                StartCoroutine(this.gameObject.GetComponent<RatAppears>().ActivateRatForTime());
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Tú         ")
        {
            if (RewardThisDialogue)
            {
                InventoryManager.Instance.hasTried2 = true;
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Encargado de taller")
        {
            if (RewardThisDialogue)
            {
                InventoryManager.Instance.SewingMachineUsed();
                InventoryManager.Instance.Lockpick();
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Tú          ")
        {
            if (RewardThisDialogue)
            {
                InventoryManager.Instance.hasPhotoshop = true;
                RewardThisDialogue = false;
            }
        }
        else if (nameNPC == "Tú           ")
        {
            if (RewardThisDialogue)
            {
                StartCoroutine(outroManager.GetComponent<GameOutro>().OutroSequence());
                RewardThisDialogue = false;
            }
        }

        isTalking = false;
        UIManager.Instance.HideDialogue();
        StartCoroutine(EnableButtonAfterDelay(1f));
        dialogueIndex = 0;
    }
    private IEnumerator EnableButtonAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (currentPlayer != null)
        {
            currentPlayer.GetComponent<PlayerInteraction>().EnableButton();
        }
        currentPlayer = null;
    }
}
