using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Mixer Reference")]
    public AudioMixer audioMixer;

    [Header("Volume Settings")]
    [Range(0.0001f, 1f)] public float masterVolume = 1f;
    [Range(0.0001f, 1f)] private float musicVolume = 1f;
    [Range(0.0001f, 1f)] private float sfxVolume = 1f;

    // Public properties to allow reading from other scripts
    public float MasterVolume => masterVolume;
    public float MusicVolume => musicVolume;
    public float SFXVolume => sfxVolume;
    

    private const float MinVolume = 0.0001f;
    private const float MaxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolumes();
            ApplyVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadVolumes()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    public void ApplyVolumes()
    {
        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
       
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = Mathf.Clamp(value, MinVolume, MaxVolume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp(value, MinVolume, MaxVolume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = Mathf.Clamp(value, MinVolume, MaxVolume);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20f);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
}
