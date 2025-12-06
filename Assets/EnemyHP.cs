using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;


public class EnemyHP : MonoBehaviour
{
    private int HP = 3;
    private Animator an;
    public bool isHurt;
    private EnemyBehaviour enemy_script;
    private AttkRange AttkRange_script;
    private AEvents AEvents_script;

    public bool dead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        an = GetComponent<Animator>();
        isHurt = false;
        dead = false;
        enemy_script = GetComponent<EnemyBehaviour>();
        AEvents_script = GetComponent<AEvents>();
        AttkRange_script = GetComponentInChildren<AttkRange>();


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
        an.ResetTrigger("hurt");
       an.SetTrigger("hurt");
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
    dead = true;
    enemy_script.enabled = false;
    AEvents_script.enabled = false;
    AttkRange_script.enabled = false;
    an.SetTrigger("die");
  }

  public void DeleteObj()
  {
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
    AttkRange_script.isAttacking = false;
    enemy_script.enabled = true;
  }

}
