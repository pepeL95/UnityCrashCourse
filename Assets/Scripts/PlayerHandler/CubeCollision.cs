using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    [SerializeField] private OriginManager originManager;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (hit.collider.gameObject.name == "Cube1")
        {
            //Debug.Log(hit.transform.parent.gameObject.name);
            //Destroy(hit.collider.transform.parent.gameObject);
            originManager.AtomicRespawn(hit.collider.transform.parent.gameObject);
            //originManager.AtomicRespawnOrigin(hit.collider.transform.parent)
            
        }

     }
}
