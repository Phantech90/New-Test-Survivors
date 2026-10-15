using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MeleeSlash : MonoBehaviour
{
    [Header("Slash Settings")]
    public float lifetime = 0.25f;
    public int damage = 5;

    [Header("Debug")]
    public bool showDebugLogs = true;

    private void Awake()
    {
        // Get the Collider2D component from this GameObject.
        Collider2D col = GetComponent<Collider2D>();

        // Check that col is not null.
        // If it exists, set it to be a trigger.
        if (col != null)
        {
            col.isTrigger = true;
        }


    }

    private void Start()
    {
        // Remove the slash after its lifetime has passed.
        Destroy(gameObject, lifetime);
    }

    public void Initialize(int damageAmount)
    {
        // Set damage to the damageAmount passed into this function.
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