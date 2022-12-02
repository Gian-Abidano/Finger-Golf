using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMove : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    Vector3 moveDir;
    
    // Start is called before the first frame update
    void Start()
    {
        moveDir = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical")
        );
    }

    private void FixedUpdate()
    {
        if(moveDir != Vector3.zero)
            rb.AddForce(moveDir * force, ForceMode.Force);
    }
}
