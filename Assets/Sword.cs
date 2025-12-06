using UnityEditor;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool hit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  void OnTriggerStay2D(Collider2D collision)
  {
    if(hit){return;}
    if (collision.CompareTag("Enemy"))
    {
        Debug.Log("in trigger stay if");
      EnemyHP enemyHP = collision.GetComponent<EnemyHP>();
      enemyHP.TakeDMG();
      hit = true;
    }
    else if (collision.CompareTag("Boss"))
    {
        Debug.Log("in if statement - trigger stay boss");
    hit = true;
      Boss boss = collision.GetComponent<Boss>();
      boss.TakeDmg();
      
    }else if (collision.CompareTag("WizardBoss"))
    {
        Debug.Log("in if statement - trigger stay boss");
    hit = true;
      WizardBoss boss = collision.GetComponent<WizardBoss>();
      boss.TakeDmg();
    }
    else if (collision.CompareTag("FEnemy"))
    {
      Debug.Log("in trigger stay if");
      FlyingEnemyHP enemyHP = collision.GetComponent<FlyingEnemyHP>();
      enemyHP.TakeDMG();
      hit = true;
    }
    
  }
  

  
}
