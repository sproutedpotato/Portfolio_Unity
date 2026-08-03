using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float forcedJumpHeight = 3.0f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private Animator animator;

    private bool isGrounded, isAboveWall;
    private float verticalVelocity, prevJumpHeight;
    private int jumpCount = 0;
    public int currentJumpCount => jumpCount;
    private AudioSource audioSource;
    private AudioClip jumpSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        jumpSound = Resources.Load<AudioClip>("Sounds/Jump");
    }
    void Update()
    {
        CheckGrounded();
        CheckAboveWall();
        Jump();
        ApplyGravity();
    }

    private void CheckGrounded()
    {
        Bounds bounds = playerCollider.bounds;
        isGrounded = Physics2D.BoxCast(bounds.center, new Vector2(bounds.size.x * 0.9f, 0.1f), 0f, Vector2.down, bounds.extents.y, groundLayer);
    }

    private void CheckAboveWall()
    {
        Bounds bounds = playerCollider.bounds;
        isAboveWall = Physics2D.BoxCast(bounds.center, new Vector2(bounds.size.x * 0.9f, 0.1f), 0f, Vector2.up, bounds.extents.y + 0.2f, groundLayer);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            audioSource.PlayOneShot(jumpSound);
            animator.SetBool("isGrounded", false);
            if(jumpCount == 0 && isGrounded)
            {
                jumpCount += 1;
                animator.SetTrigger("Jumping");
                animator.SetBool("isFalling", false);
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if(jumpCount == 1 && !isGrounded)
            {
                jumpCount += 1;
                animator.SetTrigger("DoubleJump");
                animator.SetBool("isFalling", false);
                verticalVelocity = Mathf.Sqrt(jumpHeight * 0.8f * -2f * gravity);
            }
        }
    }

    public void ForcedJump()
    {
        jumpCount = 1;
        animator.SetTrigger("Jumping");
        animator.SetBool("isFalling", false);
        verticalVelocity = Mathf.Sqrt(forcedJumpHeight * -2f * gravity);
    }

    public void SetVerticalVelocity(float newVerticalVelocity)
    {
        verticalVelocity = newVerticalVelocity;

        animator.SetBool("isFalling", false);
        if(newVerticalVelocity > 0)
        {
            animator.SetBool("isGrounded", false);
        }
    }

    private void ApplyGravity()
    {
        if (isAboveWall && verticalVelocity > 0)
        {
            verticalVelocity = -0.1f;
            animator.SetBool("isFalling", true);
        }

        if (isGrounded && verticalVelocity < 0.01f)
        {
            verticalVelocity = 0f;
            if (jumpCount > 0) jumpCount = 0;

            if (!animator.GetBool("isGrounded"))
            {
                animator.SetBool("isGrounded", true);
                animator.SetBool("isFalling", false);
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;

            if (animator.GetBool("isGrounded"))
            {
                animator.SetBool("isGrounded", false);
            }

            if (verticalVelocity < -0.1f && !isGrounded)
            {
                if (!animator.GetBool("isFalling"))
                    animator.SetBool("isFalling", true);
            }
        }

        verticalVelocity = Mathf.Max(verticalVelocity, -25f);
        rb.velocity = new Vector2(rb.velocity.x, verticalVelocity);
    }

    private void ButtonJump()
    {
        audioSource.PlayOneShot(jumpSound);
        if (jumpCount < 2)
        {
            animator.SetBool("isGrounded", false);
            if (jumpCount == 0 && isGrounded)
            {
                jumpCount += 1;
                animator.SetTrigger("Jumping");
                animator.SetBool("isFalling", false);
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (jumpCount == 1 && !isGrounded)
            {
                jumpCount += 1;
                animator.SetTrigger("DoubleJump");
                animator.SetBool("isFalling", false);
                verticalVelocity = Mathf.Sqrt(jumpHeight * 0.8f * -2f * gravity);
            }
        }
    }

    public void OnClickJumpButton()
    {
        ButtonJump();
    }
}
