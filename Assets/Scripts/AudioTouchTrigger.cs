using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTouchTrigger : MonoBehaviour {

    public string AudioClip;
    public bool OneTimeTrigger;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !(triggered && OneTimeTrigger))
        {
            triggered = true;
            DialogueManager.Instance.PlayAudio(AudioClip);
        }
    }
}
