using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField]int dir;

    bool isPressed = false;

    private void Update()
    {
        if (isPressed)
        {
            Move(dir);
        }
    }

    private void Move(int dir)
    {
        if (dir != 0)
        {
            animator.SetBool("isMove", true);
            rb.transform.localScale = new Vector3(dir, 1, 1);
        }
        else
        {
            animator.SetBool("isMove", false);
        }   

        rb.velocity = new Vector2(speed * dir, rb.velocity.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        rb.velocity = new Vector2(0, rb.velocity.y);

        animator.SetBool("isMove", false);
    }
}
