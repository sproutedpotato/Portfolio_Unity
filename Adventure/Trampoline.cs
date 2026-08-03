using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerJump>(out PlayerJump playerJump))
        {
            if(playerJump.currentJumpCount >= 1)
            {
                animator.SetTrigger("Jump");
                audioSource.PlayOneShot(clip);
                playerJump.ForcedJump();
            }
        }
    }
}
