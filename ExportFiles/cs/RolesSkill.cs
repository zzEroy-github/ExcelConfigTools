using System;
using System.Collections.Generic;

[Serializable]
public class RolesSkill
{
    public Dictionary<int, RolesSkillConfigData> data;
}

[Serializable]
public class RolesSkillConfigData
{
    public int ID;
    public int roleId;
    public int type;
    public string notes;
    public string name;
    public string detailData;
    public int nextSkillId;
    public string effectList;
    public string audioName;
}
