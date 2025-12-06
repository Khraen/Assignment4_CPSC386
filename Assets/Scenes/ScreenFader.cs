using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;       // The black full-screen Image
    public float fadeDuration = 1f; // Duration of fade

    void Start()
    {
        if(fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0); // transparent at start
    }

    // Call this to fade out and load the next scene
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeOut(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);

        float timer = 0f;
        Color c = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            c.a = Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        // Load the next scene
        SceneManager.LoadScene(sceneName);
    }
}
