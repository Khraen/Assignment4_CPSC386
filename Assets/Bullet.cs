using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
       // --- Public Configuration Variables ---

    [Header("Movement")]
    [Tooltip("The speed at which the projectile moves forward.")]
    public float speed = 7f;
    // rotateSpeed is no longer used for homing, but kept for clarity if re-enabled.
    [Tooltip("This value is no longer used, as homing is disabled. (Set to 3.5f)")]
    public float rotateSpeed = 3.5f; 

    [Header("Destruction")]
    [Tooltip("Maximum life span of the projectile before it is destroyed.")]
    public float lifetime = 5f;

    // --- Private Components and State ---

    // Target is kept for the reliable 'passing check' against its position at spawn.
    private Transform target; 
    private Rigidbody2D rb;
    
    // The fixed, normalized direction the projectile will travel.
    private Vector2 fixedDirection;
    // The vector used for checking if the projectile has passed the target's initial plane.
    private Vector2 initialDirectionToTarget; 
    private bool hasLockedTarget = false;

    // --- Unity Lifecycle Methods ---

    void Awake()
    {
        // Get the Rigidbody2D component once at the start.
        rb = GetComponent<Rigidbody2D>();
        
        // Ensure the Rigidbody is set up for non-gravity movement
        if (rb != null)
        {
            rb.gravityScale = 0;
        }
    }

    void Start()
    {
        // Destroy the game object after the defined lifetime.
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        // 1. Check for initialization
        if (!hasLockedTarget) 
        {
            // If the target wasn't set, stop movement and just wait for destruction via lifetime
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 2. Apply constant forward velocity based on the initial fixedDirection.
        // This makes the missile travel in a perfectly straight line.
        rb.linearVelocity = fixedDirection * speed;

        // 3. Check for passing the target (Reliable Method)
        
        // Current displacement vector from the projectile's position to the target's position
        // (Even if the target moves, this check still works against the initial direction plane)
        Vector2 currentDisplacement = (Vector2)transform.position - (Vector2)target.position;

        // If the dot product is positive, it means the current position has crossed 
        // the initial plane and is now "behind" the target's starting point.
        if (Vector2.Dot(currentDisplacement, initialDirectionToTarget) > 0f)
        {
            // The projectile has passed the initial target location.
            Destroy(gameObject);
        }
    }

    // --- Public Initialization Method ---

    /// <summary>
    /// Sets the target for the projectile. The projectile locks onto this position 
    /// and travels in a straight line toward it. Must be called immediately after instantiation.
    /// </summary>
    /// <param name="newTarget">The Transform to shoot toward.</param>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != null)
        {
            // 1. Calculate the fixed direction vector (for movement)
            fixedDirection = ((Vector2)target.position - (Vector2)transform.position).normalized;
            
            // 2. Store this fixed direction for the reliable 'passing check'
            initialDirectionToTarget = fixedDirection; 
            
            // 3. Set the initial rotation (once) so the missile's sprite faces the direction of travel
            float angle = Mathf.Atan2(fixedDirection.y, fixedDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            hasLockedTarget = true;
        }
    }
    

    void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    {
        Debug.Log("Hit object");
      Destroy(gameObject);
    }
  }
}
