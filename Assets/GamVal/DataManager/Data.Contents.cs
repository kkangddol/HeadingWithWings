using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Stage
[Serializable]
public class Stage
{
    public int stage = 0;
    public int startHeight = 0;
    public int endHeight = 0;
    public int rareWingDrop = 0;
    public int heroWingDrop = 0;
    public int legendWingDrop = 0;
    public float wingGenerateSec = 0.0f;
    public int wingGenerateAmount = 0;
    public int healItemDrop = 0;
    public int oxygenItemDrop = 0;
    public float itemGenerateSec = 0;
    public int itemGenerateAmount = 0;
}

[Serializable]
public class StageData: ILoader<int, Stage>
{
    public List<Stage> stages = new List<Stage>();

    public Dictionary<int, Stage> MakeDict()
    {
        Dictionary<int, Stage> dict = new Dictionary<int, Stage>();

        foreach(Stage stage in stages)
        {
            dict.Add(stage.stage, stage);
        }

        return dict;
    }
}
#endregion
#region Stage_MonsterGenerate
#region GenerateInfo
public class MonsterGenerateInfo
{
    public int[] id;
    public int[] amount;
}
public class SpecialGenerateInfo: MonsterGenerateInfo
{
    public float[] generateSec;
}
public class BigwaveGenerateInfo
{
    public MonsterGenerateInfo monsterGenerateInfo = new MonsterGenerateInfo();
    public float generateSec = 0.0f;
    public int amount = 0;
}
#endregion
[Serializable]
public class StageMonsterGenerateRaw
{
    public int stage = 0;
    public string monsterGenerateIDs = "";
    public float monsterGenerateSec = 0.0f;
    public string monsterGenerateAmounts = "";
    public string specialGenerateIDs = "";
    public string specialGenerateSecs = "";
    public string specialGenerateAmounts = "";
    public string bossGenerateIDs = "";
    public string bossGenerateAmounts = "";
    public string bigwaveMonsterIDs = "";
    public string bigwaveMonsterAmounts = "";
    public float bigwaveGenerateSec = 0.0f;
    public int bigwaveGenerateAmount = 0;
}
[Serializable]
public class StageMonsterGenerate
{
    public int stage = 0;
    public float monsterGenerateSec = 0.0f;
    public MonsterGenerateInfo monsterGenerateInfo = new MonsterGenerateInfo();
    public SpecialGenerateInfo specialGenerateInfo = new SpecialGenerateInfo();
    public SpecialGenerateInfo bossGenerateInfo = new SpecialGenerateInfo();
    public BigwaveGenerateInfo bigwaveGenerateInfo = new BigwaveGenerateInfo();
}

[Serializable]
public class StageMonsterGenerateData : ILoader<int, StageMonsterGenerate>
{
    public List<StageMonsterGenerateRaw> stages = new List<StageMonsterGenerateRaw>();

    #region SetData
    private int[] CutStringToInt(string str)
    {
        return Array.ConvertAll(str.Split("|"), i => int.Parse(i));
    }
    private float[] CutStringToFloat(string str)
    {
        return Array.ConvertAll(str.Split("|"), i => float.Parse(i));
    }
    private bool CheckMatch(string str1, string str2, bool isBigwave=false)
    {
        if(isBigwave)
        {
            if (str1.Split("|").Length == str2.Split("|").Length - 1 || str1.Split("|").Length - 1 == str2.Split("|").Length)
            {
                return true;
            }
        }
        else
        {
            if (str1.Split("|").Length == str2.Split("|").Length)
            {
                return true;
            }
        }
        

        return false;
    }
    private bool CheckMatch(string str1, string str2, string str3)
    {
        if (str1.Split("|").Length == str2.Split("|").Length && str1.Split("|").Length == str3.Split("|").Length)
        {
            return true;
        }

        return false;
    }


    private StageMonsterGenerate SetData(StageMonsterGenerateRaw raw)
    {
        StageMonsterGenerate temp = new StageMonsterGenerate();
        temp.stage = raw.stage;

        if(CheckMatch(raw.monsterGenerateIDs, raw.monsterGenerateAmounts) == false)
        {
            Debug.LogError("monsterGenerateData들이 매치되지 않습니다!");
            return null;
        }
        temp.monsterGenerateInfo.id = CutStringToInt(raw.monsterGenerateIDs);
        temp.monsterGenerateInfo.amount = CutStringToInt(raw.monsterGenerateAmounts);
        temp.monsterGenerateSec = raw.monsterGenerateSec;

        if (CheckMatch(raw.specialGenerateIDs, raw.specialGenerateSecs, raw.specialGenerateAmounts) == false)
        {
            Debug.LogError("specialGenerateData들이 매치되지 않습니다!");
            return null;
        }
        temp.specialGenerateInfo.id = CutStringToInt(raw.specialGenerateIDs);
        temp.specialGenerateInfo.amount = CutStringToInt(raw.specialGenerateAmounts);
        temp.specialGenerateInfo.generateSec = CutStringToFloat(raw.specialGenerateSecs);

        if(CheckMatch(raw.bossGenerateIDs, raw.bossGenerateAmounts) == false)
        {
            Debug.LogError("bossGenerateData들이 매치되지 않습니다!");
            return null;
        }
        temp.bossGenerateInfo.id = CutStringToInt(raw.bossGenerateIDs);
        temp.bossGenerateInfo.amount = CutStringToInt(raw.bossGenerateAmounts);

        if(CheckMatch(raw.bigwaveMonsterIDs, raw.bigwaveMonsterAmounts, true) == false)
        {
            Debug.LogError("bigwaveGenerateData들이 매치되지 않습니다!");
            return null;
        }
        temp.bigwaveGenerateInfo.monsterGenerateInfo.id = CutStringToInt(raw.bigwaveMonsterIDs);
        temp.bigwaveGenerateInfo.monsterGenerateInfo.amount = CutStringToInt(raw.bigwaveMonsterAmounts);
        temp.bigwaveGenerateInfo.generateSec = raw.bigwaveGenerateSec;
        temp.bigwaveGenerateInfo.amount = raw.bigwaveGenerateAmount;

        return temp;
    }
    #endregion

    public Dictionary<int, StageMonsterGenerate> MakeDict()
    {
        Dictionary<int, StageMonsterGenerate> dict = new Dictionary<int, StageMonsterGenerate>();

        foreach (StageMonsterGenerateRaw stage in stages)
        {
            StageMonsterGenerate temp = SetData(stage);

            if(temp == null)
            {
                return null;
            }

            dict.Add(stage.stage, temp);
        }

        return dict;
    }
}
#endregion
#region MonsterData
public class Monster
{
    public int monsterID = 0;
    public int monsterHp = 0;
    public float monsterSpeed = 0.0f;
    public float collisionDamage = 0.0f;
    public float projectileDamage = 0.0f;
    public float projectileSpeed = 0.0f;
    public float projectileFireDelay = 0.0f;
    public float skillCycleSec = 0.0f;
    public int dropItemType = 0;
}
public class MonsterData: ILoader<int, Monster>
{
    public List<Monster> monsters = new List<Monster>();

    public Dictionary<int, Monster> MakeDict()
    {
        Dictionary<int, Monster> dict = new Dictionary<int, Monster>();

        foreach (Monster monster in monsters)
        {
            dict.Add(monster.monsterID, monster);
        }

        return dict;
    }
}
#endregion