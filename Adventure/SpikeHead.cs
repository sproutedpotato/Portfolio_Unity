using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    private enum State { Idle, Falling, Returning }
    [SerializeField] private State currentState = State.Idle;

    [SerializeField] private float fallSpeed;
    [SerializeField] private float returnSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float radius;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private bool isPlayerNearby;
    private bool hitPlayerThisFall = false;
    private Vector2 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        CheckNearby();
        switch (currentState)
        {
            case State.Idle:
                CheckBelow();
                break;  

            case State.Falling:
                Fall();
                break;

            case State.Returning:
                Return();
                break;
        }
    }

    private void CheckNearby()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, radius, playerMask);

        bool currentNearby = (player != null);
        
        if(currentNearby != isPlayerNearby)
        {
            isPlayerNearby = currentNearby;
            animator.SetBool("isNear", isPlayerNearby);
        }
    }

    private void CheckBelow()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.down, 10f, playerMask);

        if(hit.collider != null)
        {
            currentState = State.Falling;
        }
    }

    private void Fall()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        RaycastHit2D playerHit = Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.down, 0.1f, playerMask);
        if(playerHit.collider != null)
        {
            if(playerHit.collider.TryGetComponent<PlayerHealth>(out var playerHealth))
            {
                playerHealth.TakeDamage(3);
                hitPlayerThisFall = true;
            }
            currentState = State.Returning;
        }

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.down, 0.1f, groundMask);
        if (hit.collider !=  null)
        {
            audioSource.PlayOneShot(audioClip);
            currentState = State.Returning;
        }
    }

    private void Return()
    {
        hitPlayerThisFall = false;
        transform.position = Vector2.MoveTowards(transform.position, startPos, returnSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, startPos) < 0.01f)
        {
            currentState = State.Idle;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            if (!hitPlayerThisFall)
            {
                if (collision.gameObject.TryGetComponent<PlayerHealth>(out var health))
                {
                    health.TakeDamage(1);
                }
            }
        }
    }
}
