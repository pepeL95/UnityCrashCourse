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
            Destroy(hit.transform.parent.gameObject);
            originManager.RespawnOrigin();
        }

     }
}
