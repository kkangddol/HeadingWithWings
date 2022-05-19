using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ProcessData
public static class ProcessData
{
    public static int[] CutStringToInt(string str)
    {
        return Array.ConvertAll(str.Split("|"), i => int.Parse(i));
    }
    public static float[] CutStringToFloat(string str)
    {
        return Array.ConvertAll(str.Split("|"), i => float.Parse(i));
    }
    public static bool CheckMatch(string str1, string str2, bool isBigwave = false)
    {
        if (isBigwave)
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
    public static bool CheckMatch(string str1, string str2, string str3)
    {
        if (str1.Split("|").Length == str2.Split("|").Length && str1.Split("|").Length == str3.Split("|").Length)
        {
            return true;
        }

        return false;
    }
}
#endregion


#region Stage
[Serializable]
public class Stage
{
    public int stage = 0;

    public int startHeight = 0;
    public int endHeight = 0;
    public float oxygenDecreaseSec = 0.0f;

    public float rareWingDrop = 0.0f;
    public float heroWingDrop = 0.0f;
    public float legendWingDrop = 0.0f;

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
[Serializable]
public class MonsterGenerateInfo
{
    public int[] id;
    public int[] amount;
}
[Serializable]
public class MonsterGroupGenerateInfo : MonsterGenerateInfo
{
    public int groupAmount = 0;
}
[Serializable]
public class SpecialGenerateInfo: MonsterGenerateInfo
{
    public float[] generateSec;
}
[Serializable]
public class BigwaveGenerateInfo
{
    public MonsterGenerateInfo monsterGenerateInfo = new MonsterGenerateInfo();
    public float generateSec = 0.0f;
    public int total = 0;
}
#endregion
[Serializable]
public class StageMonsterGenerateRaw
{
    public int stage = 0;
    public string monsterGenerateIDs = "";
    public int monsterGroupGenerateAmount = 0;
    public float monsterGenerateSec = 0.0f;
    public string monsterGenerateAmounts = "";
    public string specialGenerateIDs = "";
    public string specialGenerateSecs = "";
    public string specialGenerateAmounts = "";
    public int miniBossID = 0;
    public float miniBossGenerateSec = 0.0f;
    public int miniBossGenerateAmount = 0;
    public int miniBossGenerateTotal = 0;
    public int bossGenerateID = 0;
    public string bigwaveMonsterIDs = "";
    public string bigwaveMonsterAmounts = "";
    public float bigwaveGenerateSec = 0.0f;
    public int bigwaveGenerateTotal = 0;
}
[Serializable]
public class StageMonsterGenerate
{
    public int stage = 0;
    public float monsterGenerateSec = 0.0f;
    public MonsterGroupGenerateInfo monsterGroupGenerateInfo = new MonsterGroupGenerateInfo();
    public SpecialGenerateInfo specialGenerateInfo = new SpecialGenerateInfo();
    public BigwaveGenerateInfo miniBossGenerateInfo = new BigwaveGenerateInfo();
    public int bossGenerateID = 0;
    public BigwaveGenerateInfo bigwaveGenerateInfo = new BigwaveGenerateInfo();
}

[Serializable]
public class StageMonsterGenerateData : ILoader<int, StageMonsterGenerate>
{
    public List<StageMonsterGenerateRaw> stages = new List<StageMonsterGenerateRaw>();

    #region SetData
    private StageMonsterGenerate SetData(StageMonsterGenerateRaw rawData)
    {
        StageMonsterGenerate temp = new StageMonsterGenerate();
        temp.stage = rawData.stage;

        if(ProcessData.CheckMatch(rawData.monsterGenerateIDs, rawData.monsterGenerateAmounts) == false && rawData.monsterGenerateIDs != "-1")
        {
            Debug.LogError("monsterGenerateData들이 매치되지 않습니다!");
            return null;
        }
        temp.monsterGroupGenerateInfo.id = ProcessData.CutStringToInt(rawData.monsterGenerateIDs);
        temp.monsterGroupGenerateInfo.groupAmount = rawData.monsterGroupGenerateAmount;
        temp.monsterGenerateSec = rawData.monsterGenerateSec;
        temp.monsterGroupGenerateInfo.amount = ProcessData.CutStringToInt(rawData.monsterGenerateAmounts);

        if (ProcessData.CheckMatch(rawData.specialGenerateIDs, rawData.specialGenerateSecs, rawData.specialGenerateAmounts) == false && rawData.specialGenerateIDs != "-1")
        {
            Debug.LogError("specialGenerateData들이 매치되지 않습니다!");
            return null;
        }
        temp.specialGenerateInfo.id = ProcessData.CutStringToInt(rawData.specialGenerateIDs);
        temp.specialGenerateInfo.amount = ProcessData.CutStringToInt(rawData.specialGenerateAmounts);
        temp.specialGenerateInfo.generateSec = ProcessData.CutStringToFloat(rawData.specialGenerateSecs);

        temp.miniBossGenerateInfo.monsterGenerateInfo.id = new int[1] { rawData.miniBossID };
        temp.miniBossGenerateInfo.generateSec = rawData.miniBossGenerateSec;
        temp.miniBossGenerateInfo.monsterGenerateInfo.amount = new int[1] { rawData.miniBossGenerateAmount };
        temp.miniBossGenerateInfo.total = rawData.miniBossGenerateTotal;

        temp.bossGenerateID = rawData.bossGenerateID;

        if(ProcessData.CheckMatch(rawData.bigwaveMonsterIDs, rawData.bigwaveMonsterAmounts, true) == false && rawData.bigwaveMonsterIDs != "-1")
        {
            Debug.LogError("bigwaveGenerateData들이 매치되지 않습니다!");
            return null;
        }
        temp.bigwaveGenerateInfo.monsterGenerateInfo.id = ProcessData.CutStringToInt(rawData.bigwaveMonsterIDs);
        temp.bigwaveGenerateInfo.monsterGenerateInfo.amount = ProcessData.CutStringToInt(rawData.bigwaveMonsterAmounts);
        temp.bigwaveGenerateInfo.generateSec = rawData.bigwaveGenerateSec;
        temp.bigwaveGenerateInfo.total = rawData.bigwaveGenerateTotal;

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
[Serializable]
public class MonsterRaw
{
    public int monsterID = 0;
    public int monsterHp = 0;
    public float monsterSpeed = 0.0f;

    public float collisionDamage = 0.0f;

    public float projectileDamage = 0.0f;
    public float projectileSpeed = 0.0f;
    public float projectileFireDelay = 0.0f;

    public string skillProjectileSpeeds ="";
    public string skillCycleSecs = "";

    public string dropItemTypes = "";
    public string dropItemDrops = "";
}
[Serializable]
public class Monster
{
    public int monsterID = 0;
    public int monsterHp = 0;
    public float monsterSpeed = 0.0f;

    public float collisionDamage = 0.0f;

    public float projectileDamage = 0.0f;
    public float projectileSpeed = 0.0f;
    public float projectileFireDelay = 0.0f;

    public float[] skillProjectileSpeeds;
    public float[] skillCycleSecs;

    public int[] dropItemTypes;
    public float[] dropItemDrops;
}
[Serializable]
public class MonsterData: ILoader<int, Monster>
{
    public List<MonsterRaw> monsters = new List<MonsterRaw>();

    #region SetData
    private Monster SetData(MonsterRaw rawData)
    {
        Monster temp = new Monster();

        temp.monsterID = rawData.monsterID;
        temp.monsterHp = rawData.monsterHp;
        temp.monsterSpeed = rawData.monsterSpeed;

        temp.collisionDamage = rawData.collisionDamage;

        temp.projectileDamage = rawData.projectileDamage;
        temp.projectileSpeed = rawData.projectileSpeed;
        temp.projectileFireDelay = rawData.projectileFireDelay;
        
        if(ProcessData.CheckMatch(rawData.skillProjectileSpeeds, rawData.skillCycleSecs) == false && rawData.skillProjectileSpeeds != "-1")
        {
            Debug.LogError("skillData들이 매치되지 않습니다!");
            return null;
        }
        temp.skillProjectileSpeeds = ProcessData.CutStringToFloat(rawData.skillProjectileSpeeds);
        temp.skillCycleSecs = ProcessData.CutStringToFloat(rawData.skillCycleSecs);

        if (ProcessData.CheckMatch(rawData.dropItemTypes, rawData.dropItemDrops) == false && rawData.dropItemTypes != "-1")
        {
            Debug.LogError("dropitemdata들이 매치되지 않습니다!");
            return null;
        }
        temp.dropItemTypes = ProcessData.CutStringToInt(rawData.dropItemTypes);
        temp.dropItemDrops = ProcessData.CutStringToFloat(rawData.dropItemDrops);

        return temp;
    }
    #endregion

    public Dictionary<int, Monster> MakeDict()
    {
        Dictionary<int, Monster> dict = new Dictionary<int, Monster>();

        foreach (MonsterRaw monster in monsters)
        {
            Monster temp = SetData(monster);

            if (temp == null)
            {
                return null;
            }

            dict.Add(monster.monsterID, temp);
        }

        return dict;
    }
}
#endregion