using UnityEngine;
using UnityEngine.UI;
public class BossBoundaries : MonoBehaviour
{
    public BoxCollider2D invisibleBarrier1;
    public BoxCollider2D invisibleBarrier2;
    public GameObject boss;
    private bool alreadyopen = false;
    public GameObject bossHpBar;
    public AudioSource bossMusic;
    public AudioClip bossMusicClip;
    public GameObject gameManager;


    

    void OnTriggerEnter2D(Collider2D collision)
  {if(alreadyopen == true){return; }
    if (collision.CompareTag("Player"))
    {
      gameManager.GetComponent<AudioSource>().Stop();
    bossMusic.Play();
    invisibleBarrier1.enabled = true;
    invisibleBarrier2.enabled = true;
    bossHpBar.SetActive(true);
    

    if(boss != null)
      {
        boss.SetActive(true);

      }
      alreadyopen = true;
    
    }
    
  }

  public void RemoveBoundaries()
  {
    bossMusic.Pause();
    gameManager.GetComponent<AudioSource>().Play();
    invisibleBarrier1.enabled = false;
    invisibleBarrier2.enabled = false;
  }
}
