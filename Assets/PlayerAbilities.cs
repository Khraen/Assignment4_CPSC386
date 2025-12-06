using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    private Animator an;
    [SerializeField] private bool isAttacking = false;
    private bool can_air_attk = true;
    [SerializeField] private int jump_attacks = -1;
    private PlayerControls player_control_script;
    private Rigidbody2D rb;
    private float storedYVelocity = 0f;
    private float storedXVelocity = 0f;
    public float weaponRange = 20f;

    public LineRenderer laserLine1;
    public LineRenderer laserLine2;
    public float laserDuration = 0.05f;
    public Transform firePoint1;
    public Transform firePoint2;
    private int TwoWayShotCount = 0;
    private PlayerHP playerHP;
    public GameManager gameManager;
    public AudioSource sword_swing;
    public AudioClip sword_swing_clip;
    public AudioSource gunShot;
    public AudioClip gun_clip;

    [Header("Gun Overheat")]
    public float maxHeat = 100f;         // Max heat before overheat
    public float heatPerShot = 20f;      // Heat added per shot
    public float coolRate = 30f;         // Heat per second to cool down
    private float currentHeat = 0f;      // Current heat level
    private bool isOverheated = false;   // Gun overheated state
    public Slider heatBar;               // Optional UI heat bar

    void Start()
    {
        an = GetComponent<Animator>();
        player_control_script = GetComponent<PlayerControls>();
        rb = GetComponent<Rigidbody2D>();
        playerHP = GetComponent<PlayerHP>();

        laserLine1.sortingLayerName = "Foreground";
        laserLine1.sortingOrder = 50;
        laserLine2.sortingLayerName = "Foreground";
        laserLine2.sortingOrder = 50;

        if (heatBar != null)
        {
            heatBar.minValue = 0f;
            heatBar.maxValue = maxHeat;
            heatBar.value = currentHeat;
        }
    }

    void Update()
    {
        if (playerHP.playerDead)
        {
            enabled = false;
            return;
        }

        if (gameManager.GetPauseStatus()) return;

        // Cool down gun over time
        if (!isOverheated && currentHeat > 0f)
        {
            currentHeat -= coolRate * Time.deltaTime;
            currentHeat = Mathf.Max(currentHeat, 0f);
            if (heatBar != null) heatBar.value = currentHeat;
        }

        if (isOverheated)
        {
            currentHeat -= coolRate * Time.deltaTime;
            if (currentHeat <= 0f)
            {
                currentHeat = 0f;
                isOverheated = false;
                // Optionally: play "cooled" sound
            }
            if (heatBar != null) heatBar.value = currentHeat;
        }

        // Player inputs
        if (Input.GetButtonDown("Attack") && !isAttacking)
        {
            Attk();
        }
        else if (!Input.GetButton("Crouch") && Input.GetButtonDown("Shoot") && !isAttacking)
        {
            if (!isOverheated) Shoot();
        }
        else if (Input.GetButton("Crouch") && Input.GetButtonDown("Shoot") && !isAttacking)
        {
            if (!isOverheated) TwoWayShot();
        }
    }

    void Attk()
    {
        sword_swing.PlayOneShot(sword_swing_clip);

        if (player_control_script.in_air)
        {
            if (!can_air_attk) return;

            an.SetTrigger("attk");
        }
        else
        {
            an.SetTrigger("attk");
        }
    }

    void Shoot()
    {
        gunShot.PlayOneShot(gun_clip);
        an.SetTrigger("shoot");

        AddHeat(heatPerShot);

        Vector2 direction = player_control_script.facing_left ? Vector2.left : Vector2.right;
        Vector3 firepoint = direction == Vector2.right ? firePoint1.position : firePoint2.position;

        RaycastHit2D hit = Physics2D.Raycast(firepoint, direction, weaponRange, LayerMask.GetMask("enemy", "Boss", "Wall", "Ground"));
        Vector2 laserEnd = (hit.collider != null) ? hit.point : (Vector2)firepoint + direction * weaponRange;

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Boss"))
                hit.collider.GetComponent<Boss>().TakeDmg();
            else if (hit.collider.CompareTag("WizardBoss"))
                hit.collider.GetComponent<WizardBoss>().TakeDmg();
            else if (hit.collider.CompareTag("Enemy"))
                hit.collider.GetComponent<EnemyHP>().TakeDMG();
            else if (hit.collider.CompareTag("FEnemy"))
                hit.collider.GetComponent<FlyingEnemyHP>().TakeDMG();
        }

        laserLine1.positionCount = 2;
        laserLine1.SetPosition(0, firepoint);
        laserLine1.SetPosition(1, laserEnd);

        StartCoroutine(ShowLaser(laserLine1));
    }

    void TwoWayShot()
    {
        gunShot.PlayOneShot(gun_clip);
        an.SetTrigger("two_way_shot");

        AddHeat(heatPerShot);

        if (TwoWayShotCount > 1) TwoWayShotCount = 0;

        if (TwoWayShotCount == 0)
        {
            FireLaser(firePoint1.position, Vector2.right, laserLine1);
        }
        else
        {
            FireLaser(firePoint2.position, Vector2.left, laserLine2);
        }

        TwoWayShotCount++;
    }

    void FireLaser(Vector3 firepoint, Vector2 direction, LineRenderer line)
    {
        RaycastHit2D hit = Physics2D.Raycast(firepoint, direction, weaponRange, LayerMask.GetMask("enemy", "Boss", "Wall", "Ground"));
        Vector2 laserEnd = (hit.collider != null) ? hit.point : (Vector2)firepoint + direction * weaponRange;

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Boss"))
                hit.collider.GetComponent<Boss>().TakeDmg();
            else if (hit.collider.CompareTag("WizardBoss"))
                hit.collider.GetComponent<WizardBoss>().TakeDmg();
            else if (hit.collider.CompareTag("Enemy"))
                hit.collider.GetComponent<EnemyHP>().TakeDMG();
            else if (hit.collider.CompareTag("FEnemy"))
                hit.collider.GetComponent<FlyingEnemyHP>().TakeDMG();
        }

        line.positionCount = 2;
        line.SetPosition(0, firepoint);
        line.SetPosition(1, laserEnd);
        StartCoroutine(ShowLaser(line));
    }

    IEnumerator ShowLaser(LineRenderer line)
    {
        line.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        line.enabled = false;
    }

    void AddHeat(float amount)
    {
        currentHeat += amount;
        if (currentHeat >= maxHeat)
        {
            currentHeat = maxHeat;
            isOverheated = true;
            // Optional: play overheat sound or animation
        }
        if (heatBar != null) heatBar.value = currentHeat;
    }

    // ---- Remaining attack / air attack functions ----
    public void StartAttack() { isAttacking = true; player_control_script.canMove = false; }
    public void StartAirAttack() { isAttacking = true; jump_attacks++; }
    public void FreezeMidAir()
    {
        storedYVelocity = rb.linearVelocity.y;
        storedXVelocity = rb.linearVelocity.x;
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 0;
    }
    public void ResumeMidAir()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 4;
        rb.linearVelocity = new Vector2(storedXVelocity, 0);
    }
    public void EndAttack() { isAttacking = false; }
    public void EndAirAttack() { isAttacking = false; }
    public bool AttackStatus() { return isAttacking; }
    public void Jump_animation_event()
    {
        jump_attacks++;
        if (jump_attacks > 0)
        {
            can_air_attk = false;
            an.SetBool("can_air_attk", false);
        }
    }
    public int GetJumpAttacks() { return jump_attacks; }
    public void SetJumpAttacks(int para) { jump_attacks = para; }
    public void SetAirAttack(bool para) { can_air_attk = para; }
    void ThirdAirAttackStart()
    {
        isAttacking = true;
        jump_attacks++;
        if (jump_attacks > 0)
        {
            can_air_attk = false;
            an.SetBool("can_air_attk", false);
        }
    }
}
