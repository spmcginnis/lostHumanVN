using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryTextManager : MonoBehaviour
{
    public static StoryTextManager instance;
    void Awake() { instance = this; }  

    [System.Serializable]
    public struct Wiring
    {
        public GameObject textArea;
        public TextMeshProUGUI speakerNameTextBox;
        public TextMeshProUGUI storyTextBox;
    }
    public Wiring wiring;
    public GameObject textArea {get {return wiring.textArea;}}
    public TextMeshProUGUI speakerNameTextBox {get {return wiring.speakerNameTextBox;}}
    public TextMeshProUGUI storyTextBox {get {return wiring.storyTextBox;}}
    
    // Properties for the printing coroutine
    [HideInInspector] public bool isWaiting = false;
    [HideInInspector] public bool isBusy {get {return printingCR != null; }} 
    Coroutine printingCR = null;
    string toPrint = "";

    IEnumerator StoryText(string speaker = "", string inputText = "", bool newLine = true)
    {
        storyTextBox.gameObject.SetActive(true); // no SetActive() method available on a T...UGUI object.
        toPrint = inputText;
        isWaiting = false;
        speakerNameTextBox.text = GetSpeakerName(speaker);
        
        if (newLine)
        {
            storyTextBox.text = "";
        }
        else
        {
            toPrint = storyTextBox.text + toPrint;
        }

        while (storyTextBox.text != toPrint)
        {
            storyTextBox.text += toPrint[storyTextBox.text.Length];
            yield return new WaitForEndOfFrame();
        }

        isWaiting = true;
        
        while (isWaiting)
        {
            yield return new WaitForEndOfFrame();
        }
        StopStoryText();
    }

    public void PrintStoryText(string speaker = "", string inputText = "",  bool newLine = true)
    {
        StopStoryText();
        storyTextBox.text = toPrint;
        printingCR = StartCoroutine(StoryText(speaker, inputText, newLine));
    }

    void StopStoryText()
    {
        if (isBusy)
        {
            StopCoroutine(printingCR);
        }
        printingCR = null;
    }

    string GetSpeakerName(string input)
    {
        string speakerName = speakerNameTextBox.text; 
        if (input != speakerName && input != "")
        {
            speakerName = (input.ToLower().Contains("narrator")) ? "" : input;
        }
        return speakerName;
    }
}
