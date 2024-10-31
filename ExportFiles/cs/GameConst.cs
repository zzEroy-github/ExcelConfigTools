using System;
using System.Collections.Generic;

[Serializable]
public class GameConst
{
    public Dictionary<int, GameConstConfigData> data;
}

[Serializable]
public class GameConstConfigData
{
    public int ID;
    public string name;
    public string descript;
    public string value;
}
