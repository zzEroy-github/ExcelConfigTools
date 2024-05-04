using System;
using System.Collections.Generic;

[Serializable]
public class 示例表_RolesInfo
{
    public List<示例表_RolesInfoConfigData> data;
}

[Serializable]
public class 示例表_RolesInfoConfigData
{
    public int ID;
    public string name;
    public string modelName;
    public bool optional;
    public string iconName;
}
