using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SettingCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private CinemachineConfiner2D confiner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (confiner != null && confiner.m_BoundingShape2D == null)
        {
            GameObject boundary = GameObject.FindWithTag("Boundary");

            if (boundary != null)
            {
                Collider2D boundaryCollider = boundary.GetComponent<Collider2D>();
                if (boundaryCollider != null)
                {
                    confiner.m_BoundingShape2D = boundaryCollider;
                    confiner.InvalidateCache();
                }
            }
        }

        if (cam != null && cam.Follow == null && confiner.m_BoundingShape2D != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            
            if(player != null)
            {
                cam.Follow = player.transform;
            }
        }

        if (confiner.m_BoundingShape2D == null)
        {
            this.enabled = false;

            return;
        }
    }
}
