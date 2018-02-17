using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public static DialogueManager Instance { get; private set; }
    public float DefaultFade = 0.5f;
    public Canvas canvas;

    private SubtitleFade currentSubtitle;
    private AudioSource audioSource;
    private Queue<string> dialogueStrings;
    private string nextDialogue;
    private float fade;
    private float nextDialogueTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (audioSource.isPlaying && audioSource.time > nextDialogueTime && dialogueStrings.Count > 0)
        {
            GameObject newSubtitles = Instantiate(Resources.Load("Prefabs/Subtitles"), canvas.transform) as GameObject;
            //newSubtitles.transform.SetParent(canvas.transform);
            newSubtitles.GetComponent<Text>().text = nextDialogue;
            currentSubtitle = newSubtitles.GetComponent<SubtitleFade>();
            currentSubtitle.FadeIn(fade);
            GetNextDialogue(dialogueStrings.Dequeue());
        } else if (currentSubtitle != null && audioSource.time > nextDialogueTime - fade)
        {
            currentSubtitle.FadeOutAndDestroy(fade);
        }
    }

    public void PlayAudio(string clipName)
    {
        LoadDialogueStrings(clipName);
        audioSource.clip = Resources.Load("Dialogue/Audio/" + clipName) as AudioClip;
        audioSource.Play();
    }

    private void LoadDialogueStrings(string clipName)
    {
        TextAsset textFile = Resources.Load("Dialogue/Text/" + clipName) as TextAsset;
        Match fadeMatch = Regex.Match(textFile.text, @"fade:([0-9]*\.?[0-9]*)");
        if (fadeMatch.Success)
        {
            fade = float.Parse(fadeMatch.Groups[1].Value.Trim());
        }

        dialogueStrings = new Queue<string>(textFile.text.Split(new string[] { "[DIALOGUE_START]" }, StringSplitOptions.None)[1].Split('\n'));
        if (dialogueStrings.Peek().Trim().Length == 0)
            dialogueStrings.Dequeue();
        GetNextDialogue(dialogueStrings.Dequeue());
    }

    private void GetNextDialogue(string dialogueLine)
    {
        Match match = Regex.Match(dialogueLine, @"\[(\d+):(\d+):(\d+)\](.*)");
        nextDialogueTime = float.Parse(match.Groups[1].Value) * 60 + float.Parse(match.Groups[2].Value) + float.Parse(match.Groups[3].Value) * 0.01f;
        nextDialogue = match.Groups[4].Value;
    }
}
