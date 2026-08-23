using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Mimic: MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private Canvas worldCanvas;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    Animator animator;
    private GameManager manager;
    public ParticleSystem hitEffect;
    public float speed;

    public float currentHp = 3;
    public float maxHp = 3;

    bool isLeft = true;
    bool isHit = false;
    bool isAttack = false;
    void Start()
    {
        manager = GameManager.Instance;
        animator = GetComponent<Animator>();
        GetComponent<BoxCollider2D>().enabled = true; // 콜라이더 on
        currentHp = maxHp;
    }

    void Update()
    {
        if (!isHit && !isAttack)
        {
            EnemyMove();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EndPoint")
        {
            if (isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isLeft = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInfo>().TakeDamage(1f);
        }
    }

    private void EnemyMove()
    {
        if (currentHp > 0)
        {
            animator.SetBool("isWalking", true);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;


        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1f);
        GameObject text = Instantiate(damageTextPrefab, screenPos, Quaternion.identity, worldCanvas.transform);
        text.GetComponent<DamageText>().SetDamage(damage);

        CreateHitEffect();
        if (currentHp <= 0)
        {
            audioSource.PlayOneShot(audioClips[1]);
            Die();
        }
        else
        {
            audioSource.PlayOneShot(audioClips[0]);
            StartCoroutine(HitReaction());
        }
    }

    private IEnumerator HitReaction()
    {
        isHit = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(1f);
        isHit = false;
        animator.SetBool("isHit", false);
        animator.SetBool("isWalking", true);
    }

    private void CreateHitEffect()
    {
        hitEffect.Play();
    }
    private void Die()
    {
        animator.SetBool("isWalking", false);
        animator.SetTrigger("die");
        manager.isHaveKey = true;
        GetComponent<BoxCollider2D>().enabled = false; // 콜라이더 off
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
