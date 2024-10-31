using System;
using System.Collections.Generic;

[Serializable]
public class Ideology
{
    public Dictionary<int, IdeologyConfigData> data;
}

[Serializable]
public class IdeologyConfigData
{
    public int ID;
    public string name;
    public int star;
    public string icon;
    public string item_illustration_pos;
    public string info_illustration_pos;
    public string descript;
    public int ideologyPos;
    public int ideologyClassId;
    public int attackValue;
    public int healthValue;
    public int defenseValue;
    public string ideologyEffect_Two;
    public string ideologyEffect_Two_Data;
    public string ideologyEffect_Four;
    public string ideologyEffect_Four_Data;
}
