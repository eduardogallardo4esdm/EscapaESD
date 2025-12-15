using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private Image blackScreen;      // Imagen negra en Canvas
    [SerializeField] private float fadeDuration = 5f;

    [Header("Dialogue")]
    [SerializeField] private IntroDialogue introDialogue;

    [Header("Player")]
    [SerializeField] private GameObject Player;

    private void Start()
    {
        StartCoroutine(StartIntro());
    }

    private IEnumerator StartIntro()
    {
        Player.GetComponent<PlayerController>().enabled = false;

        if (blackScreen == null) yield break;

        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(1f);

        float timer = 0;

        while (timer < fadeDuration)
        {
            if (blackScreen == null) yield break;  // <-- La clave

            timer += Time.deltaTime;

            float alpha = 1 - (timer / fadeDuration);

            blackScreen.color = new Color(0, 0, 0, alpha);

            yield return null;
        }

        if (blackScreen == null) yield break;

        blackScreen.color = new Color(0, 0, 0, 0);
        blackScreen.gameObject.SetActive(false);

        if (introDialogue != null)
            introDialogue.StartIntro();
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
