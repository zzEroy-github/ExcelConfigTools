using System;
using System.Collections.Generic;

[Serializable]
public class RolesInfo
{
    public Dictionary<int, RolesInfoConfigData> data;
}

[Serializable]
public class RolesInfoConfigData
{
    public int ID;
    public string name;
    public string name_en;
    public string modelName;
    public bool optional;
    public string iconName;
    public string iconTransform;
    public string roleDescribe;
    public int attackValue;
    public float attackGrow;
    public int healthValue;
    public float healthGrow;
    public int defenseValue;
    public float defenseGrow;
    public int speedValue;
    public int critValue;
}
