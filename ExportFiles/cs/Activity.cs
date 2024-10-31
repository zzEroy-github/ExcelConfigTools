using System;
using System.Collections.Generic;

[Serializable]
public class Activity
{
    public Dictionary<int, ActivityConfigData> data;
}

[Serializable]
public class ActivityConfigData
{
    public int ID;
    public string descript;
    public string rewardData;
}
