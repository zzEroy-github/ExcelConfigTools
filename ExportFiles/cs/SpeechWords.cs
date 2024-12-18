using System;
using System.Collections.Generic;

[Serializable]
public class SpeechWords
{
    public Dictionary<int, SpeechWordsConfigData> data;
}

[Serializable]
public class SpeechWordsConfigData
{
    public int ID;
    public string name;
    public int belongId;
    public int type;
    public float attackValue;
    public float healthValue;
    public float defenseValue;
    public float speedValue;
    public float critValue;
    public float stayTime;
    public string buffIcon;
    public string buffEffectName;
    public string mainColor;
    public string performData;
}
