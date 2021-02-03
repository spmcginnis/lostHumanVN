using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPlayLine
{
    public string speaker;
    public string storyText;
    public string stageDirection;
    public string specialConditions;

    public ScreenPlayLine(string speaker = "", string storyText = "", string stageDirection = "", string specialConditions = "")
    {
        this.speaker = speaker;
        this.storyText = storyText;
        this.stageDirection = stageDirection;
        this.specialConditions = specialConditions;
    }
}
