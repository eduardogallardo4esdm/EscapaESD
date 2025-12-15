using UnityEngine;
using UnityEngine.InputSystem;

public class IntroDialogue : MonoBehaviour
{
    [Header("Intro Settings")]
    [SerializeField] private string narratorName = " ";

    [TextArea(3, 6)]
    [SerializeField] private string[] introLines;

    private int index = 0;
    private bool isActive = false;

    public void StartIntro()
    {
        index = 0;
        isActive = true;
        ShowLine();
    }

    private void Update()
    {
        if (!isActive) return;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            index++;

            if (index >= introLines.Length)
            {
                EndIntro();
            }
            else
            {
                ShowLine();
            }
        }
    }

    private void ShowLine()
    {
        UIManager.Instance.ShowDialogue(narratorName, introLines[index]);
    }

    private void EndIntro()
    {
        isActive = false;
        UIManager.Instance.HideDialogue();
    }
}
