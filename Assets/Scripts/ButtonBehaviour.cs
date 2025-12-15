using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    private Button btn;
    private Graphic graphic;

    private void Awake()
    {
        btn = GetComponent<Button>();
        graphic = btn.targetGraphic;

        btn.interactable = false;

        if (graphic != null)
            graphic.raycastTarget = false;
    }

    public void ButtonEnable(bool value)
    {
        btn.interactable = value;

        if (graphic != null)
            graphic.raycastTarget = value;
    }
}
