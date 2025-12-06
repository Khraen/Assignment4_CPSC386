using UnityEngine;

public class TestScript : MonoBehaviour
{
    public AudioClip testClip;  // Assign your sword or gun clip here
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.spatialBlend = 0f;  // 2D sound
        audioSource.volume = 1f;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Press space to play the clip
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (testClip != null)
                audioSource.PlayOneShot(testClip);
            else
                Debug.LogWarning("Test clip not assigned!");
        }
    }
}
