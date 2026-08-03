using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");

        if(x != 0)
        {
            animator.SetBool("isMove", true);
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        rb.velocity = new Vector2(speed * x, rb.velocity.y);
    }
}
