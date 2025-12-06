using System.Data;
using UnityEngine;
using UnityEngine.Animations;
using System.Collections;

public class PlayerControls : MonoBehaviour
{

    [SerializeField]private float move_speed = 5;

    [SerializeField] private float move_input;
    [SerializeField] private float jump_force = 16;
    private Rigidbody2D rb;
    private PlayerAbilities player_ability_script;
    
    [SerializeField]private bool is_running;
    private float last_direction = 1;
    public bool facing_left;
    private SpriteRenderer sprite_r;
    private Animator an;
    private PlayerCollisions collision_script;
    public bool in_air;
    private bool jump_request = false;
    private bool dash_request = false;
    [SerializeField]private bool isDashing = false;
    public bool canMove = true;
    [SerializeField] private float dashDistance = 5f;   // how far the dash should go
[SerializeField] private float dashDuration = 0.2f; // how long the dash should last
private Vector3 originalScale;
private PlayerHP playerHP;
public GameManager gameManager;
[SerializeField] private Transform colliderChild; // Assign the child with the collider
private Vector3 originalColliderScale;

private float dashSpeed;
    void Start()
    {
        sprite_r = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
        collision_script = GetComponent<PlayerCollisions>();
        rb = GetComponent<Rigidbody2D>();
        player_ability_script = GetComponent<PlayerAbilities>();
        dashSpeed = dashDistance / dashDuration;
        playerHP = GetComponent<PlayerHP>();
        originalScale = transform.localScale;
        originalColliderScale = colliderChild.localScale;


    }

    // Update is called once per frame
    void Update()
    {
    
    // if(gameManager.GetPauseStatus() == true)
    // {
    //   canMove = false;
    // }
    if(playerHP.playerDead == true)
    {
      canMove = false;
    }
        if (canMove == true) //removed in_air check
        {

            move_input = Input.GetAxisRaw("Horizontal");
            if (move_input != 0)
            {
                last_direction = move_input;
                if (last_direction < 0)
                {
                    facing_left = true;
                }
                else
                {
                    facing_left = false;
                }
                is_running = true;
            }
            else
            {
                is_running = false;
            }
            if (move_input > 0)
            {
                sprite_r.flipX = false;
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
                colliderChild.localScale = new Vector3(Mathf.Abs(originalColliderScale.x), originalColliderScale.y, originalColliderScale.z);
            }
            else if (move_input < 0)
            {
                sprite_r.flipX = true;
                //transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
                colliderChild.localScale = new Vector3(-Mathf.Abs(originalColliderScale.x), originalColliderScale.y, originalColliderScale.z);

            }
            else
            {
                if (facing_left)
                {
                    sprite_r.flipX = true;
                    //transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
                    colliderChild.localScale = new Vector3(-Mathf.Abs(originalColliderScale.x), originalColliderScale.y, originalColliderScale.z);
                    

                }
                else
                {
                    sprite_r.flipX = false;
                    //transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
                    colliderChild.localScale = new Vector3(Mathf.Abs(originalColliderScale.x), originalColliderScale.y, originalColliderScale.z);
                    
                }
            }
    }
    else
    {
      is_running = false;
    }

            if (Input.GetButtonDown("Jump") && in_air == false)
            {
                jump_request = true;
            }else if(Input.GetButtonDown("Dash") && in_air == false && isDashing == false)
    {
      dash_request = true;
    }



            Animate();


    

    }
    void FixedUpdate()
    {
      if (isDashing)
    {
        
        //rb.linearVelocity = new Vector2(last_direction * dash_force, 0f);
        return; // Don't allow movement during dash
    }
        if(canMove == true)
    {
      rb.linearVelocity = new Vector2(move_input * move_speed, rb.linearVelocityY);
            if (jump_request == true)
            {
                Jump();
            }else if(dash_request == true)
            {
              
              Dash();
            }
    }
            
    else
    {
      if (!in_air)
      {
        rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
      }
      move_input = 0;
      
    }
    
        
    }

    void Animate()
    {
        an.SetBool("is_running", is_running);
        an.SetBool("in_air", in_air);
        
    }
  
  private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump_force);

        in_air = true;
        jump_request = false;
    }
    private void Dash()
  {
    
  if(canMove == false)
    {
      return;
    }
    dash_request = false;
    canMove = false;
  an.SetTrigger("dash");
    StartCoroutine(DashCoroutine());
    
  }
  private IEnumerator DashCoroutine()
{
    isDashing = true;
    canMove = false;
    
    float elapsed = 0f;

    while (elapsed < dashDuration)
    {
        rb.linearVelocity = new Vector2(last_direction * dashSpeed, 0f);
        elapsed += Time.fixedDeltaTime; // fixed update time
        yield return new WaitForFixedUpdate();
        
    }
    
    rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    isDashing = false;
    an.SetBool("dash",false);
    
    canMove = true;
   
}


    public void StartDash()
  {
    isDashing = true;
    canMove = false;
  }
  public void EndDash()
  {
    canMove = true;
    isDashing = false;
  }
  public void UnlockMovement()
  {
    canMove = true;
  }
}
