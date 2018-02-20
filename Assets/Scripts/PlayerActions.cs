using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    private GameObject focusedObject;
    private GameObject heldObject;
    private bool fireAxisHeld;
    public Transform CarryPoint;
    public float ThrowForce = 300f;


    void Update()
    {
        if (Input.GetAxisRaw("PrimaryFire") == 1)
        {
            if (!fireAxisHeld)
            {
                fireAxisHeld = true;
                FireActionDown();
            } else
            {
                FireAction();
            }
        } else
        {
            if (fireAxisHeld)
            {
                fireAxisHeld = false;
                FireActionUp();
            }
        }

        if (Input.GetKeyDown("q"))
        {
            Application.Quit();
        }
    }

    private void FireActionDown()
    {
        if (focusedObject != null && heldObject == null)
        {
            InteractWithObject();
        } else if (heldObject != null)
        {
            ThrowHeldObject();
        }
        
    }

    private void FireAction()
    {

    }

    private void FireActionUp()
    {

    }

    private void InteractWithObject()
    {
        if (focusedObject.tag == "Carry")
        {
            heldObject = focusedObject;
            heldObject.transform.SetParent(CarryPoint);
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.rotation = Quaternion.identity;
            heldObject.GetComponent<Rigidbody>().isKinematic = true;
            heldObject.GetComponent<Collider>().enabled = false;
        } else if (focusedObject.tag == "InteractAudio")
        {
            InteractAudioTrigger trig = focusedObject.GetComponent<InteractAudioTrigger>();
            DialogueManager.Instance.PlayAudio(trig.AudioClip);
            if (trig.DestroyOnInteract)
                Destroy(focusedObject);
        }
    }

    private void ThrowHeldObject()
    {
        heldObject.transform.SetParent(transform.parent);
        Rigidbody heldRigid = heldObject.GetComponent<Rigidbody>();
        heldRigid.isKinematic = false;
        heldObject.GetComponent<Collider>().enabled = true;
        heldRigid.AddForce(CarryPoint.forward.normalized * ThrowForce);
        heldObject = null;
    }

    public void SetFocusedObject(GameObject obj)
    {
        focusedObject = obj;
    }

    public void LoseFocusedObject()
    {
        focusedObject = null;
    }

}
