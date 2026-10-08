using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // Complete each section marked TODO.

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Animation")]
    public Animator animator;
    public string isMovingParameter = "isMoving";

    [Header("Weapons")]
    public bool hasAcidBlade = true;
    public SpriteRenderer acidBladeSprite;
    public Animator acidBladeAnimator;

    [Header("Sprite")]
    public SpriteRenderer playerSpriteRenderer;
    public bool faceRightByDefault = true;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (playerSpriteRenderer == null)
        {
            playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        acidBladeSprite.enabled = hasAcidBlade;
    }

    private void Update()
    {
        ReadMovementInput();
        UpdateAnimation();
        UpdateSpriteFlip();
    }

    private void ReadMovementInput()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        // These variables store the player's horizontal and vertical movement directions.
        float x = 0f;
        float y = 0f;

        // Check whether the player is holding W or the Up Arrow.
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            y = 1f;
        }

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            y = -1f;
        }

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            x = -1f;
        }

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            x = 1f;
        }



        // Combine the horizontal and vertical values into one movement direction.
        moveInput = new Vector2(x, y);

        // Stop diagonal movement from being faster.
        if (moveInput.sqrMagnitude > 1f)
        {
            moveInput = moveInput.normalized;
        }
    }

    private void FixedUpdate()
    {
        // Apply the movement direction and speed to the Rigidbody.
        rb.linearVelocity = moveInput * moveSpeed;
    }

    private void UpdateAnimation()
    {
        if (animator == null)
        {
            return;
        }

        // Check whether the player is currently moving.
        bool isMoving = moveInput.sqrMagnitude > 0f;

        // Update the movement animation.
        animator.SetBool(isMovingParameter, isMoving);
        acidBladeAnimator.SetBool(isMovingParameter, isMoving);
    }

    private void UpdateSpriteFlip()
    {
        if (playerSpriteRenderer == null)
        {
            return;
        }

        // Only flip when moving left or right.
        // This prevents the sprite from flipping when moving up or down.
        if (moveInput.x > 0f)
        {
            playerSpriteRenderer.flipX = !faceRightByDefault;
            acidBladeSprite.flipX = !faceRightByDefault;
        }
        else if (moveInput.x < 0f)
        {
            playerSpriteRenderer.flipX = faceRightByDefault;
            acidBladeSprite.flipX = faceRightByDefault;
        }
    }


    /// <summary>
    /// Checks if footsteps are already playing, 
    /// if it is, return.
    /// if not, plays a footstep sound effect
    /// </summary>
    public void PlayFootstepSound()
    {
        if (audioSource == null || footstepSounds == null)
        {
            return;
        }
        if (audioSource.isPlaying)
        {
            if (audioSource.clip.Equals(footstepSounds))
            {
                return;
            }
        }
        int randomFootstep = Random.Range(1, footstepSounds.Length);
        audioSource.PlayOneShot(footstepSounds[randomFootstep]);
    }
}