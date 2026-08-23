using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ChakraController : MonoBehaviour
{
    [SerializeField] private Collider2D cd;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    private float damage;
    private float moveSpeed;
    private float direction;
    private float maxDistance = 10f;
    private bool returning;

    private GameObject player;
    private PlayerController playerController;
    private Position chakraGeneratePos;

    Vector2 playerPos;
    Vector3 moveDirection;

    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        moveSpeed = 30f;
        playerPos = player.transform.position;
        moveDirection = playerController.DIRECTION == 1 ? Vector3.right : Vector3.left;
        returning = false;
    }

    void Update()
    {
        float distance = Vector2.Distance(playerPos, transform.position);
        bool stopChakra = playerController.STOPCHAKRA;
        if (distance >= maxDistance || stopChakra)
        {
            returning = true;
        }
        else
        {
            stopChakra = false;
        }

        if (returning)
        {
            if (stopChakra)
            {
                cd.isTrigger = false;
                rb.isKinematic = true;

                return;
            }
            else
            {
                cd.isTrigger = true;
                rb.isKinematic = false;

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            }

            if (Vector2.Distance(transform.position, player.transform.position) < 0.1f)
            {
                playerController.OnChakraReturn();
                Destroy(gameObject);
            }

            return;
        }

        if (!returning)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var monster))
        {
            damage = Random.Range(0.1f, 0.3f);
            damage = (float)Math.Round(damage, 1, MidpointRounding.AwayFromZero);
            monster.TakeDamage(this.damage);
        }
        //else if (collision.TryGetComponent<IObstacle>(out var obstacle))
        //{
        //    obstacle.TakeDamage();
        //}
    }

    public void DestroyChakra()
    {
        Destroy(gameObject);
    }
}
