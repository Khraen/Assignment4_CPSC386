using Unity.Profiling;
using UnityEngine;
using System.Collections;

public class FlyingEAttk : MonoBehaviour
{
    
    private FlyingBehavior enemy_script;
    private Animator an;
    private bool detected = false;
    private bool isAttacking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy_script = GetComponentInParent<FlyingBehavior>();
        an = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
        if(detected == true && isAttacking == false)
    {
      
      enemy_script.enabled = true;
    }
    else
    {
      
      enemy_script.enabled = false;
    }
    }

    void OnTriggerEnter2D(Collider2D collision)
  {
    
    if (collision.CompareTag("Player"))
    {
      
      detected = true;
      StartCoroutine(DelayedAttack());
      // attack an
    }
    
  }
  void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
       
    StartCoroutine(DelayedAttack());
    // keep attacking
    }
   
  }

  private IEnumerator DelayedAttack()
{
    if (isAttacking) yield break;   // Prevent spam
    isAttacking = true;
    //enemy_script.enabled = false;
    an.SetBool("isWalking",false);

    yield return new WaitForSeconds(1f);  // Delay before attack
    an.SetTrigger("Attack");
}
  // use these to lock movement when attacking without stopping the exit trigger function from procing the follow.
  public void EndFlyingkAttack()
  {
    isAttacking = false;
  }
  public void ThrowProjectile()
  {
    // happens the frame the projectile is supposed to come out.
  }
  
}
