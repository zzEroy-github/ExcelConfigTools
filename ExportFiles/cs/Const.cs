using System;
using System.Collections.Generic;

[Serializable]
public class Const
{
    public Dictionary<int, ConstConfigData> data;
}

[Serializable]
public class ConstConfigData
{
    public int ID;
    public string name;
    public string descript;
    public string value;
}
