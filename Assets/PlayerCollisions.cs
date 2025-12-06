using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    //public bool is_grounded;
    private PlayerControls controls_script;
    private PlayerAbilities player_ability_script;
    private Animator an;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controls_script = GetComponent<PlayerControls>();
        player_ability_script = GetComponent<PlayerAbilities>();
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            
            controls_script.in_air = false;
            controls_script.canMove = true;
            player_ability_script.SetJumpAttacks(-1);
            player_ability_script.SetAirAttack(true);
            an.SetBool("can_air_attk",true);
        }

    }
  private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            controls_script.in_air = true;
            //controls_script.canMove = false;
        }
    }
}
