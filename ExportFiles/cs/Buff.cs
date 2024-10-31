using System;
using System.Collections.Generic;

[Serializable]
public class Buff
{
    public Dictionary<int, BuffConfigData> data;
}

[Serializable]
public class BuffConfigData
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
}
