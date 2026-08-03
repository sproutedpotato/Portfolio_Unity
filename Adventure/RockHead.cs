using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
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
    [SerializeField] private AudioClip thudClip;

    private bool isPlayerNearby;
    private Vector3 startPos;

    private void Start()
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

        if(currentNearby  != isPlayerNearby)
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
                playerHealth.TakeDamage(2);
            }
            currentState = State.Returning;
        }

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.down, 0.1f, groundMask);
        if(hit.collider != null)
        {
            audioSource.PlayOneShot(thudClip);        
            currentState = State.Returning;
        }
    }

    private void Return()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPos, returnSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, startPos) < 0.01f)
        {
            currentState = State.Idle;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        Vector2 boxSize = new Vector2(1f, 10f);
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * 5f;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
