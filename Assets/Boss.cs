using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Boss : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb;
    public Transform player;
    private bool lookingRight = false;
    public GameObject HealthBar;
    public Slider Slider;
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage;
    public BossBoundaries boundaryScript;
     public float flashDuration = 0.1f;
    private Color flashColor = Color.red;
    private Color originalColor;
    private SpriteRenderer sr;

    [Header("Health")]
    [SerializeField] private float health = 15;
    //[SerializeField] private HealthBar health_bar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        float PlayerDistance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("PlayerDistance", PlayerDistance);
    }
    public void TakeDmg()
  {
    
    health -= 1;
    Debug.Log("Health:"+health);
    Slider.value = Slider.value - 0.067f;
    StartCoroutine(FlashRed());
    if(health <= 0)
    {
        HealthBar.SetActive(false);
        boundaryScript.RemoveBoundaries();
      animator.SetTrigger("Die");
    }
  }

  public void Die()
  {
    Destroy(gameObject);
  }

  public void LookAtPlayer()
  {
    Debug.Log("in look at player");
    if(player.position.x > transform.position.x && !lookingRight || (player.position.x < transform.position.x && lookingRight))
    {

      lookingRight = !lookingRight;
      Debug.Log("flip sprite");
      transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
      //GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }
  }
  public void Attack()
  {
    Collider2D[] objects = Physics2D.OverlapCircleAll(attackController.position, attackRadius);

    foreach(Collider2D collision in objects)
    {
      if (collision.CompareTag("Player"))
      {
        collision.GetComponent<PlayerHP>().TakeDmg();
      }
    }
  }
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(attackController.position,attackRadius);
  }

  private IEnumerator FlashRed()
    {
        sr.color = flashColor;     // turn red
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;  // return to normal
    }


}
