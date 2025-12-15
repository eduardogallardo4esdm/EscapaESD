using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOutro : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private string escenaInicio = "MainMenu";

    public IEnumerator OutroSequence()
    {
        // Bloquear jugador
        if (UIManager.Instance != null)
            UIManager.Instance.PauseGame();

        // ===== DIÁLOGOS =====

        yield return ShowLine("Tú", 
            "Por fin voy a poder salir de la escuela");

        yield return ShowLine("Tú", 
            "Espera... La puerta sigue sin abrirse");

        yield return ShowLine("Recepcionista", 
            "Corazón, ¿Por qué estás fichando? ¿Acaso no eres estudiante?");

        yield return ShowLine("Tú", 
            "Si, estaba intentando abrir la puerta principal, en secretaría me dijeron que con una tarjeta de profesor se abriría");

        yield return ShowLine("Recepcionista", 
            "Se referirían a las puertas de las aulas... ¿Así que estás intentando salir? Espera que te desbloqueo la puerta");

        yield return ShowLine("Tú", 
            "¿Ah que habías bloqueado tú la puerta?");

        yield return ShowLine("Recepcionista", 
            "Claro, lo hago en circunstancias como un apagón. Pero vamos, es tan fácil como decírmelo y la abro");

        yield return ShowLine("Tú", 
            "No me puedo creer que lleve todo este rato intentando salir...");

        // ===== FIN =====
        UIManager.Instance.ShowDialogue(" ", "Gracias por jugar");
        yield return new WaitForSecondsRealtime(3f);

        // Cargar escena inicio
        SceneManager.LoadScene(escenaInicio);
        UIManager.Instance.PauseGame();
        yield return new WaitForSecondsRealtime(3f);
        UIManager.Instance.ResumeGame();
    }

    private IEnumerator ShowLine(string speaker, string text)
    {
        UIManager.Instance.ShowDialogue(speaker, text);

        // Esperar click del jugador
        bool clicked = false;

        while (!clicked)
        {
            if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
                clicked = true;

            yield return null;
        }

        UIManager.Instance.HideDialogue();
    }
}
