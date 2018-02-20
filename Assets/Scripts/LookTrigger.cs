using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTrigger : MonoBehaviour {

    public float LookTime = 1f;
    public float LookTolerance = 0.9f;
    public bool OneTimeTrigger;
    public float LookDistance = 5f;
    public string AudioClip;
    private Camera mainCam;
    private float lookingTimer;
    private bool triggered;

	// Use this for initialization
	void Start () {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

    private void FixedUpdate()
    {
        Vector3 dirToCam = (transform.position - mainCam.transform.position).normalized;
        float dotProd = Vector3.Dot(dirToCam, mainCam.transform.forward);
        RaycastHit raycastInfo;
        Physics.Raycast(transform.position, -dirToCam, out raycastInfo, LookDistance);
        if (dotProd > LookTolerance && raycastInfo.collider != null && raycastInfo.collider.tag == "Player")
        {
            lookingTimer += Time.fixedDeltaTime;
            if (lookingTimer > LookTime && !triggered)
            {
                triggered = true;
                DialogueManager.Instance.PlayAudio(AudioClip);
            }
        } else
        {
            lookingTimer = 0;
            if (!OneTimeTrigger)
            {
                triggered = false;
            }
        }
    }
}
