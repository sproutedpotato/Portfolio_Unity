using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    private bool isOn = true;
    public event Action<bool> OnFireStateChanged;


    void Start()
    {
        animator.SetTrigger("On");
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        { 
            yield return new WaitForSeconds(2f);
            isOn = !isOn;
            animator.SetBool("isOn", isOn);
            OnFireStateChanged?.Invoke(isOn);
            if (isOn)
            {
                audioSource.clip = clip;
                audioSource.loop = true;
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
            
        }
    }
}
