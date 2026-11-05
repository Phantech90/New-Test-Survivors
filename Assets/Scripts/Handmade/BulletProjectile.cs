using UnityEngine;
using UnityEngine.InputSystem;

public class BulletProjectile : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float lifetime = 0.25f;
    public float bulletSpeed = 5f;
    public int damage = 5;
    public Transform target;
    private Rigidbody2D rb;
    public Camera mainCamera;

    [Header("Debug")]
    public bool showDebugLogs = true;

    private void Awake()
    {
        mainCamera = GameObject.FindAnyObjectByType<Camera>();
        // Get the Collider2D component from this GameObject.
        Collider2D col = GetComponent<Collider2D>();
        // Check that col is not null.
        // If it exists, set it to be a trigger.
        if (col != null)
        {
            col.isTrigger = true;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Remove the slash after its lifetime has passed.
        Destroy(gameObject, lifetime);
    }

    public void Initialize(int damageAmount)
    {
        // Set damage to the damageAmount passed into this function.

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = (mousePosWorld - playerPos).normalized;
        rb.linearVelocity = direction * bulletSpeed;
        damage = damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Try to get the EnemyHealth component from the object that entered the trigger.
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        // Check that enemy is not null.
        if (enemy != null)
        {
            // Call the enemy's TakeDamage function and give it this slash's damage amount.
            enemy.TakeDamage(damage);

            // If debug logs are enabled, print the enemy hit and damage dealt.
            if (showDebugLogs)
                Debug.Log($"enemy {enemy.name} was hit for {damage} damage");

        }
    }
}
