using System;
using System.Collections.Generic;

[Serializable]
public class MonsterInfo
{
    public Dictionary<int, MonsterInfoConfigData> data;
}

[Serializable]
public class MonsterInfoConfigData
{
    public int ID;
    public string name;
    public string name_en;
    public string type;
    public int level;
    public int hp;
    public int attackValue;
    public int defenseValue;
    public int speedValue;
    public string aiComponent;
    public string rewardDrop;
    public string colliderBoxOnStand;
    public string colliderBoxOnLie;
    public string effectsData;
}
