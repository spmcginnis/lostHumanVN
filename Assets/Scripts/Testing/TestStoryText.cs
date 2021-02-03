using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStoryText : MonoBehaviour
{
    StoryTextManager storyTextManager;
    int storyIndex = 0;
    List<ScreenPlayLine> chapter0;

    void Start()
    {
        storyTextManager = StoryTextManager.instance;

        chapter0 = new List<ScreenPlayLine>()
        {
            new ScreenPlayLine("Test Speaker", "Test dialogue ... "),
            new ScreenPlayLine("Test Speaker 2", "Another dialogue string ...")
        };

        Debug.Log(chapter0[1].storyText);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!storyTextManager.isBusy || storyTextManager.isWaiting)
            {
                if (storyIndex >= chapter0.Count) {return;}

                storyTextManager.PrintStoryText(
                    chapter0[storyIndex].speaker,
                    chapter0[storyIndex].storyText
                    );
                storyIndex++;
            }
        }
    }
}
