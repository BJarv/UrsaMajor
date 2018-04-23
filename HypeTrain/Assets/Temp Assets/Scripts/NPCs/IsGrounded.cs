using UnityEngine;
using System.Collections;

public class IsGrounded : LogController {

    bool isGrounded;

    public LayerMask groundMask = 302593;

    public void OnCollisionEnter2D(Collision2D collisionObject)
    {
        if (IsInLayerMask(collisionObject.gameObject, groundMask))
        {
            isGrounded = true;
        }
    }

    public void OnCollisionStay2D(Collision2D collisionObject)
    {
        if (IsInLayerMask(collisionObject.gameObject, groundMask))
        {
            isGrounded = true;
        }
    }


    public void OnCollisionExit2D(Collision2D collisionObject)
    {
        if (IsInLayerMask(collisionObject.gameObject, groundMask))
        {
            isGrounded = false;
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        // Convert the object's layer to a bitfield for comparison
        int objLayerMask = (1 << obj.layer);
        if ((layerMask.value & objLayerMask) > 0)  // Extra round brackets required!
            return true;
        else
            return false;
    }

    public bool check()
    {
        return isGrounded;
    }
}
