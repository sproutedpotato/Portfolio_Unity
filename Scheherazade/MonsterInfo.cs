using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour, IDamageable
{
    [SerializeField] private float damage;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInfo player;

    private float hp;
    private bool hasDamaged;

    // Start is called before the first frame update
    void Start()
    {
        hasDamaged = false;
        hp = 50f;
    }

    public void TakeDamage(float damage)
    {
        this.hp -= damage;
        animator.SetTrigger("Hit");
    }

    #region Trigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInfo>(out var player) && !hasDamaged)
        {
            hasDamaged = true;
            StartCoroutine(ApplyDamageAfterAnimation(player));
        }
    }

    private IEnumerator ApplyDamageAfterAnimation(PlayerInfo player)
    { 
        player.TakeDamage(this.damage);

        yield return new WaitForSeconds(1.3f);

        hasDamaged = false;
    }
    #endregion
}
