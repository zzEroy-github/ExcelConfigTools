using System;
using System.Collections.Generic;

[Serializable]
public class Skill
{
    public Dictionary<int, SkillConfigData> data;
}

[Serializable]
public class SkillConfigData
{
    public int ID;
    public int belongId;
    public int type;
    public int buffId;
    public string notes;
    public string name;
    public string detailData;
    public int nextSkillId;
    public float flySpeed;
    public string effectList;
    public string audioName;
    public string hitAudio;
}
