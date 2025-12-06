using UnityEngine;
using System;

public class FlyingBehavior : MonoBehaviour
{
     public GameObject player;
    public bool flip;
    public float speed;

    private Animator animator;
    public GameObject projectilePrefab; // The projectile prefab
    public Transform spawnpt;               // The arm where projectiles spawn
    public Transform player_pos;            // Reference to player

    void Start()
    {
        animator = GetComponent<Animator>();
        
    }
        

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        
        Vector3 Scale = transform.localScale;

        bool moving = false;

        if (player.transform.position.x > transform.position.x)
        {
            Scale.x = Math.Abs(Scale.x) * -1 * (flip ? -1 : 1);
            transform.Translate(x: speed * Time.deltaTime, y: 0, z: 0);
            moving = true;
        }
        else
        {
            Scale.x = Math.Abs(Scale.x) * (flip ? -1 : 1);
            transform.Translate(x: speed * Time.deltaTime *-1, y: 0, z: 0);
            moving = true;
        }

        transform.localScale = Scale;

        // Animation support
        if (animator != null)
        {
            animator.SetBool("isWalking", moving);
        }
    }

    public void FireProjectile()
    {

        GameObject projectile = Instantiate(projectilePrefab, spawnpt.position, Quaternion.identity);
        Bullet bullet = projectile.GetComponent<Bullet>();
        if (bullet != null)
            bullet.SetTarget(player_pos);
    }
}
