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
    public int quality;
    public string icon;
    public string descript;
}
