using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The target to follow
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        if(target.position.y < 0)
        {
            transform.position = new Vector3(target.position.x, 0, -10f);
        }
        else
        {
            transform.position = new Vector3(target.position.x, target.position.y, -10f);
        }
        
    }
}
