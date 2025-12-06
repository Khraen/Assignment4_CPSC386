using UnityEngine;

public class FlyingAEvents : MonoBehaviour
{
    
     private Animator animator;
    private FlyingEAttk attk_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
        attk_script = GetComponentInChildren<FlyingEAttk>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // use these to lock movement when attacking without stopping the exit trigger function from procing the follow.
  public void EndFlyingkAttack()
  {
    attk_script.EndFlyingkAttack();
  }
  public void ThrowProjectile()
  {
    attk_script.ThrowProjectile();
    // happens the frame the projectile is supposed to come out.
  }
}
