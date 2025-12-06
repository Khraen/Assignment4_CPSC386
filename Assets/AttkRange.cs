using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class AttkRange : MonoBehaviour
{
    private Animator an;
    //public GameObject attack_box;
    private EnemyBehaviour enemy_script;
    [SerializeField]public bool isAttacking = false;
    private EnemyHP enemyhp_script;
    public BoxCollider2D attack_box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        an = GetComponentInParent<Animator>();
        enemy_script = GetComponentInParent<EnemyBehaviour>();
        enemyhp_script = GetComponentInParent<EnemyHP>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      
      StartCoroutine(DelayedAttack());
    
    }
    
  }
  private void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      
      StartCoroutine(DelayedAttack());
    
    }
    
  }
  public void StartAttack()
  {
    isAttacking = true;
  }
  public void StartAttackFrames()
  {
   //attack_box.SetActive(true);
   attack_box.enabled = true;
  }
  public void EndAttackFrames()
  {
    //attack_box.SetActive(false);
    attack_box.enabled = false;
    attack_box.GetComponent<EAttkBox>().hit = false;
  }
  public void EndOfEnemyAttack()
  {
    
    isAttacking = false;
    enemy_script.enabled = true;
  }


private IEnumerator DelayedAttack()
{
    if (isAttacking || enemyhp_script.isHurt) yield break;   // Prevent spam
    isAttacking = true;
    enemy_script.enabled = false;
    an.SetBool("isWalking",false);

    yield return new WaitForSeconds(0f);  // Delay before attack
    an.SetTrigger("Attack");
}


}
