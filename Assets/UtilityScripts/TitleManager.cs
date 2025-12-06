using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class TitleManager : MonoBehaviour
{
    public GameObject settings_panel;
    public GameObject instructions_panel;
    public GameObject resumeButton;
    public Image blackoutPanel; // Assign in Inspector
    public float fadeDuration = 1f; // Duration of fade in seconds
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
  {
    if (SaveSystem.HasSave())
        {
            resumeButton.SetActive(true);
        }
        else
        {
            resumeButton.SetActive(false);
        }
  }

    // Update is called once per frame
    void Update()
    {
    }
    public void StartBTN()
{
    SaveSystem.DeleteSave();
    Debug.Log("clicked btn");
    StartCoroutine(StartGameWithFade());
}
    public void QuitBTN()
    {
        Application.Quit();
    }
    public void ResumeBTN()
{
    SaveData data = SaveSystem.LoadGame();

    if (data == null)
    {
        Debug.LogWarning("No save found, cannot resume.");
        return;
    }

    GameManager.loadFromSave = true;
    StartCoroutine(ResumeGameWithFade(data.CurrentSceneIndex));
}

    public void SettingsBTN()
    {
        Debug.Log("clicked settings button");
        settings_panel.SetActive(true);
    }
    public void ExitSettings()
    {
        Debug.Log("Clicked exit setting");
        settings_panel.SetActive(false);
    }
    public void InstructionsBTN()
  {
    instructions_panel.SetActive(true);
  }
  public void ExitInstructions()
  {
    instructions_panel.SetActive(false);
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

    private IEnumerator StartGameWithFade()
{
    yield return StartCoroutine(FadeOut());  // WAIT for fade to finish
    SceneManager.LoadScene(1);
    Time.timeScale = 1f;
}

    private IEnumerator ResumeGameWithFade(int sceneIndex)
{
    yield return StartCoroutine(FadeOut());
    SceneManager.LoadScene(sceneIndex);
    Time.timeScale = 1f;
}





}
