using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Box"))
        {
            audioSource.PlayOneShot(clip);
            animator.SetTrigger("Collect");
        }
    }

    public void DestroyFruit()
    {
        Destroy(gameObject);
    }
}
