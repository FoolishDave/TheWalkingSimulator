using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float Speed;

    private Rigidbody playerRigid;

    private void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 vel = playerRigid.velocity;
        Vector3 velMod = transform.forward * Input.GetAxis("Vertical") * Speed + transform.right * Input.GetAxis("Horizontal") * Speed;
        vel.x = velMod.x;
        vel.z = velMod.z;
        playerRigid.velocity = vel;
        if (Input.GetKeyDown("q"))
            Application.Quit();
    }
}
