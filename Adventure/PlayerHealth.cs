using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Image[] hearts;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Animator animator;
    private AudioSource audioSource;

    private float hp = 3f;
    private bool canHit = true;
    private AudioClip sound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sound = Resources.Load<AudioClip>("Sounds/Hit");
        InitHeart();
    }

    private void InitHeart()
    {
        hearts = new Image[3];
        for(int i = 0; i < 3; i++)
        {
            hearts[i] = GameObject.Find("Heart" + (i + 1)).GetComponent<Image>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!canHit)
        {
            return;
        }

        HitSound();
        animator.SetTrigger("Hit");
        canHit = false;
        hp -= damage;
        UpdateHeart();
        StartCoroutine(DamageCoroutuine());

        if (hp < 0)
        {
            Time.timeScale = 0f;
            Die();
            return;
        }
    }

    private void UpdateHeart()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].fillAmount = i < hp ? 1f : 0f;
        }
    }

    private void Die() => gameOverPanel.SetActive(true);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            if (hp < 3)
            {
                hp += 1;
                UpdateHeart();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            TakeDamage(1);
        }
        else if (collision.CompareTag("DeadZone"))
        {
            Time.timeScale = 0f;
            Die();
        }
    }
    private IEnumerator DamageCoroutuine()
    {
        yield return new WaitForSeconds(1);
        canHit = true;
    }

    private void HitSound()
    {
        audioSource.PlayOneShot(sound);
    }
}