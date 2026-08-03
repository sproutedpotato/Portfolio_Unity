using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 moveOffset;
    [SerializeField] private float speed = 2.0f;

    private Vector2 startPos;
    private Vector2 targetPos;
    private bool movingToTarget = true;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveOffset;
        animator.SetTrigger("On");
    }

    void Update()
    {
        Vector3 currentTarget = movingToTarget ? startPos : targetPos;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            movingToTarget = !movingToTarget;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = Application.isPlaying ? startPos : transform.position;
        Gizmos.DrawLine(start, start + (Vector3)moveOffset);
        Gizmos.DrawSphere(start + (Vector3)moveOffset, 0.2f);
    }
}
