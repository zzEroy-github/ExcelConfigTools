using System;
using System.Collections.Generic;

[Serializable]
public class Weapon
{
    public Dictionary<int, WeaponConfigData> data;
}

[Serializable]
public class WeaponConfigData
{
    public int ID;
    public string name;
    public int star;
    public string type;
    public string icon;
    public string item_illustration_pos;
    public string info_illustration_pos;
    public string descript;
    public string weaponType;
    public int attackValue;
    public int critValue;
    public string speedType;
    public int speedValue;
    public string weaponEffect;
    public string weaponEffect_Data;
}
