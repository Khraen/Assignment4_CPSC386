using UnityEngine;

public class AEvents : MonoBehaviour
{
     private Animator animator;
    private AttkRange attack_range_script;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
        attack_range_script = GetComponentInChildren<AttkRange>();
        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void StartAttack()
  {
    attack_range_script.StartAttack();
  }
    public void StartAttackFrames()
  {
    attack_range_script.StartAttackFrames();
  }
  public void EndAttackFrames()
  {
    attack_range_script.EndAttackFrames();
  }
  public void EndOfEnemyAttack()
  {
    attack_range_script.EndOfEnemyAttack();
  }
  ///////

  
}
