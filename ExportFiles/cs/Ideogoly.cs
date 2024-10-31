using System;
using System.Collections.Generic;

[Serializable]
public class Ideogoly
{
    public Dictionary<int, IdeogolyConfigData> data;
}

[Serializable]
public class IdeogolyConfigData
{
    public int ID;
    public string name;
    public int star;
    public string icon;
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
