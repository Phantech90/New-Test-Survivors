using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileAttack : MonoBehaviour
{
    [Header("References")]
    public GameObject bulletPrefab;
    public SpriteRenderer playerSpriteRenderer;
    public PlayerMovement playerMovement;

    [Header("Animation")]
    public Animator animator;
    public bool useAttackAnimation = true;
    public string attackTriggerName = "Attack";

    [Header("Facing")]

    [Header("Attack Settings")]
    public bool attacksEnabled = true;
    public int damage = 5;
    public float cooldown = 1f;
    public int weaponID = 2;

    [Header("Bullet Position")]
    public Transform emitterLocation;

    [Header("Debug")]
    public bool showDebugLogs = true;

    [Header("Debug View")]
    public float attackTimer = 0f;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.hasWeapon[2] == true)
        {
            UpdateAutoAttack();
        }
    }


    private void UpdateAutoAttack()
    {
        if (attacksEnabled == false)
        {
            return;
        }

        // Reduce the attack timer each frame.
        attackTimer -= Time.deltaTime;

        // When the timer reaches 0, attack and reset the timer.
        if (attackTimer <= 0f && Mouse.current.leftButton.isPressed)
        {
            // Call the Attack function.
            Attack();


            // Reset attackTimer to the cooldown value.
            attackTimer = cooldown;

        }
    }

    private void Attack()
    {
        // Call the function that performs the melee slash.
        ShootBullet();


        // Call the function that plays the attack animation.
        PlayAttackAnimation();

    }

    private void PlayAttackAnimation()
    {
        if (useAttackAnimation == false)
        {
            return;
        }

        if (animator == null)
        {
            if (showDebugLogs == true)
            {
                Debug.LogWarning("ProjectileAttack: No Animator found.");
            }

            return;
        }

        // Play the attack animation.
        animator.SetTrigger(attackTriggerName);
        for (int i = 0; i < playerMovement.weaponVisuals.Length; i++)
            playerMovement.weaponVisuals[i].weaponAnimator.SetBool("playerAttacking", true);

        playerMovement.weaponVisuals[weaponID].weaponAnimator.SetTrigger(attackTriggerName);
        playerMovement.weaponVisuals[weaponID].currentIdle = false;

        if (showDebugLogs == true)
        {
            Debug.Log("ProjectileAttack: Attack animation triggered.");
        }
    }

    private void ShootBullet()
    {
        if (bulletPrefab == null)
        {
            return;
        }

        Vector3 localSpawnPosition = emitterLocation.position;

        GameObject bulletObj = Instantiate(bulletPrefab);

        bulletObj.transform.localPosition = localSpawnPosition;
        bulletObj.transform.localRotation = Quaternion.identity;

        // Get the MeleeSlash script from the slash object.
        BulletProjectile bullet = bulletObj.GetComponent<BulletProjectile>();

        // Check that slash is not null.
        if (bullet != null)
        {
            bullet.Initialize(damage);
        }
        // If it exists, call Initialize and give it the player's damage.
    }
}
