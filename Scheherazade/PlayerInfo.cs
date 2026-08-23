using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerInfo : MonoBehaviour
{
    private const int hittedIndex = 0, dieIndex = 1;

    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private GameObject gameOver;
    private float hp;
    private float minDamage, maxDamage;
    public Status status { get; set; }
    public Action<float> OnHealthChanged;

    private bool isDamaged;

    private GameManager manager;

    void Start()
    {
        manager = GameManager.Instance;
        Init();
        isDamaged = false;
        gameOver.SetActive(false);
        OnHealthChanged?.Invoke(hp);
    }

    private void Die()
    {
        audioSource.PlayOneShot(audioClips[dieIndex]);
        gameOver.SetActive(true);
        animator.SetTrigger("death");
        manager.canMove = false;
        //Destroy(gameObject);
    }

    public void TakeDamage(float damage){
        if (status != Status.Immune && !isDamaged && status != Status.Die)
        {
            this.hp -= damage;
            
            if(hp < 0f)
            { 
                status = Status.Die;
                Die();
                return;
            }
            StartCoroutine(DamageCoroutine());
            StartCoroutine(AnimiCoroutine());
            audioSource.PlayOneShot(audioClips[hittedIndex]);
        }
        OnHealthChanged?.Invoke(hp);
    }

    IEnumerator AnimiCoroutine()
    {
        if(status != Status.Die)
        {
            animator.SetBool("isHitted", true);
            manager.canMove = false;
            yield return new WaitForSeconds(0.3f);
            animator.SetBool("isHitted", false);
            manager.canMove = true;
        }
    }
    IEnumerator DamageCoroutine()
    {
        isDamaged = true;
        yield return new WaitForSeconds(1f);
        isDamaged = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            TakeDamage(1f);
        }
    }

    private void Init()
    {
        int itemNum = manager.itemNum;
                
        this.hp = 5f;
        this.minDamage = 0.7f;
        this.maxDamage = 1.3f;

        if(itemNum % 2 == 0 && itemNum != 0)
        {
            this.hp += itemNum / 2;
        }
        else if(itemNum % 2 == 1)
        {
            if(itemNum == 1)
            {
                this.minDamage += 0.5f;
                this.maxDamage += 0.5f;
            }
            else if(itemNum == 3)
            {
                this.minDamage += 1f;
                this.maxDamage += 1f;
            }
            else if(itemNum == 5)
            {
                this.minDamage += 1.5f;
                this.maxDamage += 1.5f;
            }
        }
    }

    public float ReturnDamage()
    {
        float damage = Random.Range(minDamage, maxDamage);
        damage = (float)Math.Round(damage, 1, MidpointRounding.AwayFromZero);

        return damage;
    }
    
}
