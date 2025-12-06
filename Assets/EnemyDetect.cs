using UnityEngine;

public class EnemyDetect : MonoBehaviour
{

    private EnemyBehaviour enemy_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy_script = GetComponentInParent<EnemyBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      enemy_script.enabled = true;
    enabled = false;
    }
    
  }
}
