using UnityEditor;
using UnityEngine;

public class EAttkBox : MonoBehaviour
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
    if (collision.CompareTag("Player"))
    {
        
      PlayerHP playerHP = collision.GetComponent<PlayerHP>();
      playerHP.TakeDmg();
      hit = true;
    }
    
  }
  

  
}
