using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // Complete each section marked TODO.

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float dashSpeed = 2f;

    [Header("Animation")]
    public Animator animator;
    public string isMovingParameter = "isMoving";

    [Header("Weapons")]
    public bool[] hasWeapon;
    public WeaponVisuals[] weaponVisuals;

    [Header("Sprite")]
    public SpriteRenderer playerSpriteRenderer;
    public bool faceRightByDefault = true;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip footstepSounds;

    [Header("Cooldown")]
    public float dashCooldown = 4f;
    public float dashTimer = 0f;
    public float dashLength = 0.5f;
    public float dashLengthTime = 0f;
    public bool dashing = false;
    public bool fireDash = false;

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
        for (int i = 0; i < weaponVisuals.Length; i++)
        {
            CheckWeapon(i);
        }
    }

    private void Update()
    {
        ReadMovementInput();
        UpdateAnimation();
        UpdateSpriteFlip();

        // countdown the dash timer, unless currently dashing, in which count down the dash lenght timer instead
        if (!dashing)
        {
            dashTimer -= Time.deltaTime;
        }
        else
        {
            dashLengthTime -= Time.deltaTime;
        }

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
        if (Keyboard.current.shiftKey.isPressed && dashTimer <= 0)
        {
            dashTimer = dashCooldown;
            dashLengthTime = dashLength;
            dashing = true;
            fireDash = true;
        }
    }

    private void FixedUpdate()
    {
        // Apply the movement direction and speed to the Rigidbody.
        // if dashing and dash length is positive, apply dash force.
        if (dashing && dashLengthTime > 0)
        {
            if (fireDash)
            {
                rb.AddForce(moveInput * dashSpeed, ForceMode2D.Impulse);
                fireDash = false;
            }
        }
        // else if dashing and dash length is negative, reset dashing to false
        else if (dashing && dashLengthTime <= 0)
        {
            dashing = false;
        }
        // if false, apply typical force
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
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
        for (int i = 0; i < weaponVisuals.Length; i++)
        {
            weaponVisuals[i].weaponAnimator.SetBool(isMovingParameter, isMoving);
        }
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
            for (int i = 0; i < weaponVisuals.Length; i++)
            {
                weaponVisuals[i].weaponSprite.flipX = !faceRightByDefault;
            }
        }
        else if (moveInput.x < 0f)
        {
            playerSpriteRenderer.flipX = faceRightByDefault;
            for (int i = 0; i < weaponVisuals.Length; i++)
            {
                weaponVisuals[i].weaponSprite.flipX = faceRightByDefault;
            }
        }
    }

    // Check the weapon script and enable the weapons visuals
    public void CheckWeapon(int weapon)
    {
        if (weaponVisuals[weapon] != null)
        {
            weaponVisuals[weapon].weaponSprite.enabled = hasWeapon[weapon];
            weaponVisuals[weapon].weaponAnimator.enabled = hasWeapon[weapon];
        }
        else { Debug.LogWarning("Weapon " + weapon + " visuals do not exist"); }
    }


    /// <summary>
    /// Checks if footsteps are already playing, 
    /// if it is, return.
    /// if not, plays a footstep sound effect
    /// </summary>
    public void PlayFootstepSound()
    {
        if (audioSource.isPlaying)
        {
            if (audioSource.clip.Equals(footstepSounds))
            {
                return;
            }
        }
        else if (audioSource != null && footstepSounds != null)
        {
            audioSource.PlayOneShot(footstepSounds);
        }
        else return;
    }
}