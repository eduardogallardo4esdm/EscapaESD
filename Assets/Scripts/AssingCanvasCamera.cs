using UnityEngine;

public class AssingCanvasCamera : MonoBehaviour
{
    void Start()
    {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas == null)
                canvas = GetComponentInChildren<Canvas>();

            if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
            {
                canvas.worldCamera = Camera.main;
            }
    }
}
