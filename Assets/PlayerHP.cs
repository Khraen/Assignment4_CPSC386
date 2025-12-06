using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
  private int HP = 3; 
  private int temp_hp = 3;
  public bool playerDead = false;
  public Slider Slider;
  public GameManager gameManagerScript;
  private Animator an;
  public float flashDuration = 0.1f;
    private Color flashColor = Color.red;
    private Color originalColor;
    private SpriteRenderer sr;
    public AudioSource hurtSound;
    public AudioClip hurtClip;
    public AudioSource DeathSound;
    public AudioClip DeathClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        an = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg()
  {
    if(playerDead){return;}
    HP--;
    Slider.value = Slider.value - 0.34f;
    
    if(HP <= 0)
    {
      // turn off fill area
      Slider.fillRect.gameObject.SetActive(false);
      
      Die();
    }
    else
    {
      hurtSound.PlayOneShot(hurtClip);
      an.SetTrigger("Hurt");
      StartCoroutine(FlashRed());
    }
  }
  public void TakeDmg(int dmg)
  {
    if(playerDead == true){ return;}
    HP = HP - dmg;
    Slider.value = Slider.value - 0.34f;
    if(HP <= 0)
    {
      // turn off fill area
      Slider.fillRect.gameObject.SetActive(false);
      //playerDead = true;

      Die();
    }
    else
    {
      an.SetTrigger("Hurt");
      StartCoroutine(FlashRed());
    }
  }

  public void Die()
  {

    // set animator and startcoroutine(wait()) then go to game manager
    playerDead = true;
    DeathSound.PlayOneShot(DeathClip);
    an.SetTrigger("Die");
    StartCoroutine(Wait());
    gameManagerScript.PlayerDie();
  }
  IEnumerator Wait()
  {
    yield return new WaitForSeconds(1f);
  }

  void OnTriggerStay2D(Collider2D collision)
  {
    if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyAttk"))
    {
      TakeDmg();

    }
    
  }
  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("EProjectile"))
    {
      TakeDmg();
      Destroy(collision.gameObject);
    }
    // if(collision.gameObject.layer == LayerMask.NameToLayer("EProjectile"))
    // {
      

    // }
  }
  private IEnumerator FlashRed()
    {
        sr.color = flashColor;     // turn red
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;  // return to normal
    }

public bool GetDeathStatus()
  {
    return playerDead;
  }
  public int GetPlayerHealth()
  {
    return HP;
  }

  public void SetHealth(int hp)
  {
    HP = hp;
  }
  

  
}
