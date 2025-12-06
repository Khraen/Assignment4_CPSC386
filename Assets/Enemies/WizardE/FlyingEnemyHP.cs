using UnityEngine;
using System.Collections;

public class FlyingEnemyHP : MonoBehaviour
{
     private int HP = 3;
    private Animator an;
    public bool isHurt;
    private FlyingBehavior enemy_script;
    //private AttkRange AttkRange_script;
    private FlyingAEvents AEvents_script;
    private SpriteRenderer sr;
    public float flashDuration = 0.1f;
    private Color flashColor = Color.red;
    private Color originalColor;

    private bool dead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        an = GetComponent<Animator>();
        isHurt = false;
        dead = false;
        enemy_script = GetComponent<FlyingBehavior>();
        AEvents_script = GetComponent<FlyingAEvents>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
       // AttkRange_script = GetComponentInChildren<AttkRange>();


    }

    // Update is called once per frame
    void Update()
    {
        if(HP == 0)
    {
      Die();
    }
    }

    public void TakeDMG()
  {
    if (!dead)
    {
      Debug.Log("in if");
     HP--;
     Debug.Log("Health: "+HP);
     if(HP == 0)
      {
        dead = true;
        Die();
      }
      else
      {
        Debug.Log("Flashing white");
        StartCoroutine(FlashWhite());
      }
    }
    else
      {
        
        return;
      }
    
  }
  void Die()
  {
    Debug.Log("in Die()");
    enemy_script.enabled = false;
    AEvents_script.enabled = false;
    //AttkRange_script.enabled = false;
    an.SetTrigger("Die");
    StartCoroutine(Delete());
  }

  private IEnumerator Delete()
  {
    yield return new WaitForSeconds(1f);
    Destroy(gameObject);
  }
  public void StartOfHurtAnimation()
  {
    isHurt = true;
    enemy_script.enabled = false;
  }
  public void EndOfHurtAnimation()
  {
    isHurt = false;
    //AttkRange_script.isAttacking = false;
    enemy_script.enabled = true;
  }
  private IEnumerator FlashWhite()
    {
        sr.color = flashColor;     // turn white
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;  // return to normal
    }
}
