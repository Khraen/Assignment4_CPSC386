using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttk : MonoBehaviour
{
    //public GameObject hitbox;
    public BoxCollider2D attack_box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstAttackFrame()
  {
    //hitbox.SetActive(true);
    attack_box.enabled = true;
    //
  }
  public void LastAttackFrame()
  {
    attack_box.enabled = false;
    attack_box.GetComponent<Sword>().hit = false;
    //hitbox.SetActive(false);
    //hitbox.GetComponent<Sword>().hit = false;
  }
  public void TurnOffAttkBox()
  {
    attack_box.enabled = false;
    attack_box.GetComponent<Sword>().hit = false;
  }

  
}
