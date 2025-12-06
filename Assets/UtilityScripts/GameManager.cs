using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static bool loadFromSave = false;

    private bool gamePaused;
    public GameObject pause_overlay;
    private PlayerHP player_health_script;

    public GameObject player;
    public Slider slider;
    public Image blackoutPanel; // Assign in Inspector
    public float fadeDuration = 1f; // Duration of fade in seconds
    public PlayerControls playerControls;
    public AudioSource sceneMusic;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
{
    gamePaused = false;
    //win_status = false;
    player_health_script = player.GetComponent<PlayerHP>();
    Time.timeScale = 1f;
    // if (AudioManager.Instance != null)
    // {
    //     AudioManager.Instance.ApplyVolumes();
    // }
    // else
    // {
    //     Debug.LogWarning("AudioManager not found in GameManager Start. Make sure it exists in TitleScene and uses DontDestroyOnLoad.");
    // }
    

    if (loadFromSave)
    {
        SaveData loadedData = SaveSystem.LoadGame();
        if (loadedData != null)
        {
            ApplySaveData(loadedData);
            Debug.Log("Loaded saved game data.");
        }
        else
        {
            Debug.LogWarning("No save data found to load.");
        }

        loadFromSave = false; // reset the flag
    }
}


    // Update is called once per frame
    void Update()
{
    if (Input.GetButtonDown("Pause"))
    {
      Debug.Log("Pressed P");
        if (!gamePaused)
        {
            PauseGame();
        }
        else if (!player_health_script.GetDeathStatus())
        {
            ResumeGame();
        }
    }
    
}

public void PlayerDie()
  {
    // Set active set the transparency of the panel to 100 percent to make the blackout transition to scene load look seamless
    // then load same scene(restart scene)
    StartCoroutine(FadeOutAndRestart());
  }

    public void PauseGame()
    {
        if (gamePaused) return;
        Debug.Log("in pausegame()");
        if (pause_overlay != null) Debug.Log("pause_overlay not null");
        pause_overlay.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        if(!gamePaused){ return; }
        pause_overlay.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
  
    }
    public bool GetPauseStatus()
    {
        return gamePaused;
    }
    public void SaveExitBTN()
    {
    SaveData data = GatherSaveData();
    SaveSystem.SaveGame(data);
    Debug.Log("Game saved. Quitting application...");
    SceneManager.LoadScene("TitleScene");
    gamePaused = false;
    Time.timeScale = 1f;
    }

    // public void WinGame()
    // {
    //     win_overlay.SetActive(true);
    //     win_menu.SetActive(true);
    //     Time.timeScale = 0f;
    //     gamePaused = true;

    // }

    private SaveData GatherSaveData()
{
    SaveData data = new SaveData();
    Vector3 playerPos = player.transform.position;
    data.playerX = playerPos.x;
    data.playerY = playerPos.y;
    data.currentHealth = player_health_script.GetPlayerHealth(); 
    data.CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    return data;
}

private void ApplySaveData(SaveData data)
{
    if (data == null)
    {
        Debug.LogWarning("No save data to load.");
        return;
    }

    player.transform.position = new Vector3(data.playerX, data.playerY, player.transform.position.z);
    player_health_script.SetHealth(data.currentHealth);
    if(player_health_script.GetPlayerHealth() == 1)
    {
      slider.value = .32f;
    }else if(player_health_script.GetPlayerHealth() == 2)
    {
      slider.value = .66f;
    }else if(player_health_script.GetPlayerHealth() == 3)
    {
      slider.value = 1f;
    }
    

}

private IEnumerator FadeOutAndRestart()
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

        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }



  
}
