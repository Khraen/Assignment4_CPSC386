using UnityEngine;

public class BossSpell : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Vector2 box_dimension;
    [SerializeField] private Transform box_position;
    [SerializeField] private float life_time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, life_time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
  {
    Collider2D [] objects = Physics2D.OverlapBoxAll(box_position.position, box_dimension, 0f);
    foreach(Collider2D collisions in objects)
    {
      if (collisions.CompareTag("Player"))
      {
        collisions.GetComponent<PlayerHP>().TakeDmg();
      }
    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireCube(box_position.position, box_dimension);
  }
}
