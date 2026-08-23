using UnityEngine;

public class StalactiteTrigger : MonoBehaviour
{
    private Stalactite parentStalactite;

    void Awake()
    {
        parentStalactite = GetComponentInParent<Stalactite>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentStalactite != null)
        {
            parentStalactite.TriggerFall();
        }
    }
}
