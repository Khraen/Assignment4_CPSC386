using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DoorScript : MonoBehaviour
{
  public GameObject text;
  public Image blackoutPanel; // Assign in Inspector
    public float fadeDuration = 1f; // Duration of fade in seconds
    private bool playerInTrigger = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (playerInTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(NextSceneWithFade());
        }
        
    }


    void OnTriggerStay2D(Collider2D collision)
  {
    // Transition to next scene (castle)
    if(collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
    {
      SceneManager.LoadScene("Castle");
    }
    

  }
  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text.SetActive(true);
            playerInTrigger = true;
        }
    }
  void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text.SetActive(false);
            playerInTrigger = false;
        }
    }

  private IEnumerator FadeOut()
    {
        // Ensure panel is active
        blackoutPanel.gameObject.SetActive(true);

        // Fade from transparent to opaque
        Color panelColor = blackoutPanel.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            panelColor.a = Mathf.Clamp01(elapsed / fadeDuration);
            blackoutPanel.color = panelColor;
            yield return null;
        }

        panelColor.a = 1f; // Fully opaque
        blackoutPanel.color = panelColor;
    }

  private IEnumerator NextSceneWithFade()
{
    yield return StartCoroutine(FadeOut());  // WAIT for fade to finish
    SceneManager.LoadScene("Castle");
    Time.timeScale = 1f;
}
}
