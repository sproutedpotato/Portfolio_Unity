using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float riseSpeed;
    [SerializeField] private float exitBoost;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        animator.SetTrigger("On");
        audioSource.PlayOneShot(clip);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.TryGetComponent<PlayerJump>(out var playerJump))
            {                playerJump.SetVerticalVelocity(riseSpeed);
            }
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.TryGetComponent<PlayerJump>(out var playerJump))
            {
                playerJump.SetVerticalVelocity(exitBoost);
            }
        }
    }
}
