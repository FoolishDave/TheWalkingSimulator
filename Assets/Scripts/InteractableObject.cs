using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public float LookDistance = 4f;

    private Color startColor;
    private Renderer rend;
    private Camera mainCam;
    private bool pickupAble;
    private bool isLooking;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rend = GetComponent<Renderer>();
    }

    private void OnMouseOver()
    {
        float distToCam = Vector3.Distance(transform.position, mainCam.transform.position);
        if (distToCam < LookDistance && !isLooking)
        {
            StartLooking();
        } else if (distToCam >= LookDistance && isLooking)
        {
            StopLooking();
        }
    }

    private void OnMouseExit()
    {
        if (isLooking)
        {
            StopLooking();
        }
    }

    private void StartLooking()
    {
        isLooking = true;
        startColor = rend.material.color;
        rend.material.color = lightenColor(startColor);
        mainCam.transform.parent.GetComponent<PlayerActions>().SetFocusedObject(gameObject);
    }

    private void StopLooking()
    {
        isLooking = false;
        rend.material.color = startColor;
        mainCam.transform.parent.GetComponent<PlayerActions>().LoseFocusedObject();
    }

    private Color lightenColor(Color color)
    {
        Color highlight = new Color(0.5f, 0.5f, 0.5f);
        return color + highlight;
    }
}
