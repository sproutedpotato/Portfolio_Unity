using UnityEngine;

public class Stalactite : MonoBehaviour
{
    public float fallDelay = 1.5f;       // 떨어지기 전 대기 시간
    public bool isFalling = false;       // 이미 떨어지고 있는지 여부

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    public void TriggerFall()
    {
        if (!isFalling)
        {
            isFalling = true;
            Invoke(nameof(Fall), fallDelay);
        }
    }

    private void Fall()
    {
        rb.isKinematic = false;
        rb.gravityScale = 2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling)
        {
            if (collision.collider.CompareTag("Floor"))
            {
                Destroy(gameObject);
            }
            else if (collision.collider.CompareTag("Player"))
            {
                collision.collider.GetComponent<PlayerInfo>().TakeDamage(0.5f);
                Destroy(gameObject);
            }
        }
    }
}
