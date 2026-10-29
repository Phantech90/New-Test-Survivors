using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    // Complete each section marked TODO.

    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Target")]
    public Transform target;

    [Header("Animation")]
    public Animator animator;
    public string isMovingParameter = "isMoving";

    [Header("Sprite")]
    public SpriteRenderer spriteRenderer;
    public bool faceRightByDefault = true;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Awake()
    {
        // TODO: Get the Rigidbody2D component from this GameObject.
        rb = GetComponent<Rigidbody2D>();


        // TODO: Check if animator is null.
        // If it is, get the Animator component from this GameObject.
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }


        // TODO: Check if spriteRenderer is null.
        // If it is, get the SpriteRenderer from this GameObject or its children.
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

    }

    private void Start()
    {
        // Check if a target has not been assigned.
        if (target == null)
        {
            // Find the GameObject with the Player tag.
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            // Check that playerObject is not null.
            // If it exists, set target to the player's Transform.
            if (playerObject != null)
            {
                target = playerObject.transform;
            }

        }
    }

    private void FixedUpdate()
    {
        // Check if target is null.
        // If it is, stop the enemy moving and stop this function.
        if (target == null)
        {
            moveDirection = Vector2.zero;
            return;
        }


        // TODO: Calculate the direction from the enemy to the target.
        // Normalise the direction and store it in moveDirection.
        moveDirection = ((Vector2)target.position - rb.position).normalized;


        // TODO: Move the enemy using moveDirection and moveSpeed.
        rb.MovePosition((Vector2)transform.position + moveDirection * Time.deltaTime * moveSpeed);


        // TODO: Call the function that updates the animation.
        UpdateAnimation();


        // TODO: Call the function that updates the sprite direction.
        UpdateSpriteFlip();

    }

    private void UpdateAnimation()
    {
        if (animator == null)
        {
            return;
        }
        bool isMoving = false;
        // TODO: Create a bool that checks whether the enemy is moving.
        if (rb.totalForce != Vector2.zero)
        {
            isMoving = true;
        }

        // TODO: Update the Animator's isMoving parameter.
        animator.SetBool(isMovingParameter, isMoving);

    }

    private void UpdateSpriteFlip()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        // Only change direction when moving left or right.
        if (moveDirection.x > 0f)
        {
            // TODO: Set flipX so the enemy faces right.
            spriteRenderer.flipX = false;

        }
        else if (moveDirection.x < 0f)
        {
            // TODO: Set flipX so the enemy faces left.
            spriteRenderer.flipX = true;

        }
    }
}