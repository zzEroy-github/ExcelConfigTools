using System;
using System.Collections.Generic;

[Serializable]
public class Item
{
    public Dictionary<int, ItemConfigData> data;
}

[Serializable]
public class ItemConfigData
{
    public int ID;
    public string name;
    public int star;
    public string icon;
    public string descript;
    public int ideologyPos;
    public int ideologyClassId;
    public string ideologyEffect_Two;
    public string ideologyEffect_Two_Data;
    public string ideologyEffect_Four;
    public string ideologyEffect_Four_Data;
}
