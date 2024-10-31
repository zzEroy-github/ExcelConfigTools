using System;
using System.Collections.Generic;

[Serializable]
public class DungeonInfo
{
    public Dictionary<int, DungeonInfoConfigData> data;
}

[Serializable]
public class DungeonInfoConfigData
{
    public int ID;
    public string name;
    public int level;
    public string story;
    public string reward;
    public int prefabId;
    public string playerInitXY;
    public string monsterData;
    public string audio;
}
