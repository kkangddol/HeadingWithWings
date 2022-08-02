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

#region MonsterData
[Serializable]
public class Monster
{
    public string monsterName = "";
    public int monsterID = 0;
    public int monsterHp = 0;
    public float moveSpeed = 0.0f;

    public float meleeDamage = 0.0f;

    public float projectileDamage = 0.0f;
    public float projectileSpeed = 0.0f;
    public float projectileFireDelay = 0.0f;
    public float attackRange = 0.0f;

    public float skillCoolTime = 0.0f;
    public float skillDamage = 0.0f;
    public float skillKnockBackSize = 0.0f;
    public float skillProjectileDamage = 0.0f;
    public float skillProjectileSpeed = 0.0f;
}
[Serializable]
public class MonsterData: ILoader<int, Monster>
{
    public List<Monster> monsters = new List<Monster>();

    public Dictionary<int, Monster> MakeDict()
    {
        Dictionary<int, Monster> dict = new Dictionary<int, Monster>();

        foreach (Monster monster in monsters)
        {
            if (monster == null)
            {
                return null;
            }

            dict.Add(monster.monsterID, monster);
        }

        return dict;
    }
}
#endregion

#region AttackEquipData
[Serializable]
public class AttackEquipRaw
{
    public int equipID = 0;
    public string equipName = "";
    public int level = 0;
    public float damageMultiplier = 0.0f;
    public float delayMultiplier = 0.0f;
    public float attackRange = 0.0f;
    public float knockBackSize = 0.0f;
    public float bulletSpeed = 0.0f;
    public int pelletCount = 0;
    public float headShotChance = 0.0f;
    public float headShotDamageMultiplier = 0.0f;
    public float speedMultiplier = 0.0f;
    public float slowDuration = 0.0f;
    public float splashRange = 0.0f;
    public int collisionCount = 0;
    public string isPair = "";
    public float attackDuration = 0.0f;
    public int satelliteCount = 0;
    public int meteorCount = 0;
    public string isGTAEMeteor = "";
}

[Serializable]
public class AttackEquip
{
    public int equipID = 0;
    public string equipName = "";
    public int level = 0;
    public float damageMultiplier = 0.0f;
    public float delayMultiplier = 0.0f;
    public float attackRange = 0.0f;
    public float knockBackSize = 0.0f;
    public float bulletSpeed = 0.0f;
    public int pelletCount = 0;
    public float headShotChance = 0.0f;
    public float headShotDamageMultiplier = 0.0f;
    public float speedMultiplier = 0.0f;
    public float slowDuration = 0.0f;
    public float splashRange = 0.0f;
    public int collisionCount = 0;
    public bool isPair = false;
    public float attackDuration = 0.0f;
    public int satelliteCount = 0;
    public int meteorCount = 0;
    public bool isGTAEMeteor = false;
}

[Serializable]
public class AttackEquipData: ILoader<int, AttackEquip>
{
    public List<AttackEquipRaw> attackEquipData = new List<AttackEquipRaw>();

    #region SetData
    private AttackEquip SetData(AttackEquipRaw rawData)
    {
        AttackEquip temp = new AttackEquip();

        temp.equipID = rawData.equipID;
        temp.equipName = rawData.equipName;
        temp.level = rawData.level;
        temp.damageMultiplier = rawData.damageMultiplier;
        temp.delayMultiplier = rawData.delayMultiplier;
        temp.attackRange = rawData.attackRange;
        temp.knockBackSize = rawData.knockBackSize;
        temp.bulletSpeed = rawData.bulletSpeed;
        temp.pelletCount = rawData.pelletCount;
        temp.headShotChance = rawData.headShotChance;
        temp.headShotDamageMultiplier = rawData.headShotDamageMultiplier;
        temp.speedMultiplier = rawData.speedMultiplier;
        temp.slowDuration = rawData.slowDuration;
        temp.splashRange = rawData.splashRange;
        temp.collisionCount = rawData.collisionCount;
        temp.isPair = rawData.isPair == "TRUE" ? true : false;
        temp.attackDuration = rawData.attackDuration;
        temp.satelliteCount = rawData.satelliteCount;
        temp.meteorCount = rawData.meteorCount;
        temp.isGTAEMeteor = rawData.isGTAEMeteor == "TRUE" ? true : false;

        return temp;
    }
    #endregion

    public Dictionary<int, AttackEquip> MakeDict()
    {
        Dictionary<int, AttackEquip> dict = new Dictionary<int, AttackEquip>();

        foreach (AttackEquipRaw attackEquip in attackEquipData)
        {
            AttackEquip temp = SetData(attackEquip);

            if (temp == null)
            {
                return null;
            }

            dict.Add(temp.equipID, temp);
        }

        return dict;
    }
}

#endregion

#region WingEquipData
[Serializable]
public class WingEquip
{
    public int equipID = 0;
    public string equipName = "";
    public int level = 0;
    public float damageMultiplier = 0.0f;
    public float delayMultiplier = 0.0f;
    public float knockBackSize = 0.0f;
    public float skillTime = 0.0f;
    public int maxDashCount = 0;
}

[Serializable]
public class WingEquipData: ILoader<int, WingEquip>
{
    public List<WingEquip> wingEquipData = new List<WingEquip>();

    public Dictionary<int, WingEquip> MakeDict()
    {
        Dictionary<int, WingEquip> dict = new Dictionary<int, WingEquip>();

        foreach (WingEquip wingEquip in wingEquipData)
        {
            if (wingEquip == null)
            {
                return null;
            }

            dict.Add(wingEquip.equipID, wingEquip);
        }
        return dict;
    }
}
#endregion

#region EquipDescriptionData
[Serializable]
public class EquipDescriptionRaw
{
    public int equipID = 0;
    public string equipName = "";
    public int level = 0;
    public string info = "";
    public string detailInfo = "";
    public string damage = "";
    public string delay = "";
    public float range = 0.0f;
    public string speed = "";
    public string pelletCount = "";
    public string headshotDamage = "";
    public string headshotChance = "";
    public string slow = "";
    public string slowTime = "";
    public string healAmount = "";
    public string skillTime = "";

}

[Serializable]
public class EquipDescription
{
    public int equipID = 0;
    public string equipName = "";
    public int level = 0;
    public string info = "";
    public string detailInfo = "";
    public string damage = "";
    public string delay = "";
    public float range = 0.0f;
    public string speed = "";
    public string pelletCount = "";
    public string headshotDamage = "";
    public string headshotChance = "";
    public string slow = "";
    public string slowTime = "";
    public string healAmount = "";
    public string skillTime = "";
    public List<string> infoList = new List<string>();
    public List<string> infoTitle = new List<string>();
}

[Serializable]
public class EquipDescriptionpData: ILoader<int, EquipDescription>
{
    public List<EquipDescriptionRaw> equipDescription = new List<EquipDescriptionRaw>();

    #region SetData
    private EquipDescription SetData(EquipDescriptionRaw rawData)
    {
        EquipDescription temp = new EquipDescription();

        temp.equipID = rawData.equipID;
        temp.equipName = rawData.equipName;
        temp.level = rawData.level;
        temp.info = rawData.info == "null" ? null : rawData.info;
        temp.detailInfo = rawData.detailInfo == "null" ? null : rawData.detailInfo;
        temp.damage = rawData.damage == "null" ? null : rawData.damage;
        temp.delay = rawData.delay == "null" ? null : rawData.delay;
        temp.range = rawData.range;
        temp.speed = rawData.speed == "null" ? null : rawData.speed;
        temp.pelletCount = rawData.pelletCount == "null" ? null : rawData.pelletCount;
        temp.headshotDamage = rawData.headshotDamage == "null" ? null : rawData.headshotDamage;
        temp.headshotChance = rawData.headshotChance == "null" ? null : rawData.headshotChance;
        temp.slow = rawData.slow == "null" ? null : rawData.slow;
        temp.slowTime = rawData.slowTime == "null" ? null : rawData.slowTime;
        temp.healAmount = rawData.healAmount == "null" ? null : rawData.healAmount;
        temp.skillTime = rawData.skillTime == "null" ? null : rawData.skillTime;

        if(temp.damage != null) {temp.infoList.Add(temp.damage); temp.infoTitle.Add("damage");} 
        if(temp.delay != null) {temp.infoList.Add(temp.delay); temp.infoTitle.Add("delay");}
        if(temp.speed != null) {temp.infoList.Add(temp.speed); temp.infoTitle.Add("speed");}
        if(temp.pelletCount != null) {temp.infoList.Add(temp.pelletCount); temp.infoTitle.Add("pelletCount");}
        if(temp.headshotDamage != null) {temp.infoList.Add(temp.headshotDamage); temp.infoTitle.Add("headshotDamage");}
        if(temp.headshotChance != null) {temp.infoList.Add(temp.headshotChance); temp.infoTitle.Add("headshotChance");}
        if(temp.slow != null) {temp.infoList.Add(temp.slow); temp.infoTitle.Add("slow");}
        if(temp.slowTime != null) {temp.infoList.Add(temp.slowTime); temp.infoTitle.Add("slowTime");}
        if(temp.healAmount != null) {temp.infoList.Add(temp.healAmount); temp.infoTitle.Add("healAmount");}
        if(temp.skillTime != null) {temp.infoList.Add(temp.skillTime); temp.infoTitle.Add("skillTime");}

        return temp;
    }
    #endregion

    public Dictionary<int, EquipDescription> MakeDict()
    {
        Dictionary<int, EquipDescription> dict = new Dictionary<int, EquipDescription>();

        foreach (EquipDescriptionRaw equipDesc in equipDescription)
        {
            EquipDescription temp = SetData(equipDesc);

            if (temp == null)
            {
                return null;
            }

            dict.Add(temp.equipID, temp);
        }
        return dict;
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
            Debug.LogError("monsterGenerateData���� ��ġ���� �ʽ��ϴ�!");
            return null;
        }
        temp.monsterGroupGenerateInfo.id = ProcessData.CutStringToInt(rawData.monsterGenerateIDs);
        temp.monsterGroupGenerateInfo.groupAmount = rawData.monsterGroupGenerateAmount;
        temp.monsterGenerateSec = rawData.monsterGenerateSec;
        temp.monsterGroupGenerateInfo.amount = ProcessData.CutStringToInt(rawData.monsterGenerateAmounts);

        if (ProcessData.CheckMatch(rawData.specialGenerateIDs, rawData.specialGenerateSecs, rawData.specialGenerateAmounts) == false && rawData.specialGenerateIDs != "-1")
        {
            Debug.LogError("specialGenerateData���� ��ġ���� �ʽ��ϴ�!");
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
            Debug.LogError("bigwaveGenerateData���� ��ġ���� �ʽ��ϴ�!");
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

#region WingData
[Serializable]
public class WingRaw
{
    public int wingID = 0;
    public int wingTier = 0;
    public int hasRange = 0;

    public string damagePerLevels = "";
    public string attackSpeedPerLevels = "";

    public int hasPassive = 0;
    public string passiveSkillDamagePerLevels = "";
    public string passiveAttackSpeedIncreasePerLevels = "";
    public string passiveMoveSpeedIncreasePerLevels = "";
    public float passiveSkillCooltime = 0.0f;

    public int hasActive = 0;
    public string activeSkillDamagePerLevels = "";
    public string activeSkillLifetimePerLevels = "";
    public float activeSkillCooltime = 0.0f;
}
[Serializable]
public class Wing
{
    public int wingID = 0;
    public int wingTier = 0;
    public bool hasRange = false;

    public int[] damagePerLevels;
    public float[] attackSpeedPerLevels;

    public bool hasPassive = false;
    public int[] passiveSkillDamagePerLevels;
    public int[] passiveAttackSpeedIncreasePerLevels;
    public int[] passiveMoveSpeedIncreasePerLevels;
    public float passiveSkillCooltime = 0.0f;

    public bool hasActive = false;
    public int[] activeSkillDamagePerLevels;
    public float[] activeSkillLifetimePerLevels;
    public float activeSkillCooltime = 0.0f;
}
[Serializable]
public class WingData : ILoader<int, Wing>
{
    public List<WingRaw> wings = new List<WingRaw>();

    #region SetData
    public bool CheckLevels(string str1, bool isActive=false)
    {
        string[] split = str1.Split("|");
        
        if(isActive)
        {
            if (str1.Split("|").Length == 2 && split[0] != "" && split[1] != "")
            {
                return true;
            }
        }
        else
        {
            if (str1.Split("|").Length == 3 && split[0] != "" && split[1] != "" && split[2] != "")
            {
                return true;
            }
        }

        return false;
    }
    private Wing SetData(WingRaw rawData)
    {
        Wing temp = new Wing();

        temp.wingID = rawData.wingID;
        temp.wingTier = rawData.wingTier;
        temp.hasRange = rawData.hasRange == 1 ? true : false;

        if (CheckLevels(rawData.damagePerLevels) == false)
        {
            Debug.LogError("damageDat�� �����մϴ�!");
            return null;
        }
        temp.damagePerLevels = ProcessData.CutStringToInt(rawData.damagePerLevels);

        if (CheckLevels(rawData.attackSpeedPerLevels) == false)
        {
            Debug.LogError("attackSpeedData�� �����մϴ�!");
            return null;
        }
        temp.attackSpeedPerLevels = ProcessData.CutStringToFloat(rawData.attackSpeedPerLevels);

        temp.hasPassive = rawData.hasPassive == 1 ? true : false;
        if (temp.hasPassive)
        {
            if(CheckLevels(rawData.passiveSkillDamagePerLevels) == false)
            {
                Debug.LogError("passiveSkillDamageData�� �����մϴ�!");
                return null;
            }
            temp.passiveSkillDamagePerLevels = ProcessData.CutStringToInt(rawData.passiveSkillDamagePerLevels);

            if (CheckLevels(rawData.passiveAttackSpeedIncreasePerLevels) == false)
            {
                Debug.LogError($"passiveAttackSpeedIncreaseData�� �����մϴ�!");
                return null;
            }
            temp.passiveAttackSpeedIncreasePerLevels = ProcessData.CutStringToInt(rawData.passiveAttackSpeedIncreasePerLevels);

            if (CheckLevels(rawData.passiveMoveSpeedIncreasePerLevels) == false)
            {
                Debug.LogError("passiveMoveSpeedIncreaseData�� �����մϴ�!");
                return null;
            }
            temp.passiveMoveSpeedIncreasePerLevels = ProcessData.CutStringToInt(rawData.passiveMoveSpeedIncreasePerLevels);

            temp.passiveSkillCooltime = rawData.passiveSkillCooltime;
        }
        
        temp.hasActive = rawData.hasActive == 1 ? true : false;
        if(temp.hasActive)
        {
            if (CheckLevels(rawData.activeSkillDamagePerLevels, true) == false)
            {
                Debug.LogError("activeSkillDamageData�� �����մϴ�!");
                return null;
            }
            temp.activeSkillDamagePerLevels = ProcessData.CutStringToInt(rawData.activeSkillDamagePerLevels);

            temp.activeSkillLifetimePerLevels = ProcessData.CutStringToFloat(rawData.activeSkillLifetimePerLevels);
            temp.activeSkillCooltime = rawData.activeSkillCooltime;
        }

        return temp;
    }
    #endregion

    public Dictionary<int, Wing> MakeDict()
    {
        Dictionary<int, Wing> dict = new Dictionary<int, Wing>();

        foreach (WingRaw wing in wings)
        {
            Wing temp = SetData(wing);

            if (temp == null)
            {
                return null;
            }

            dict.Add(wing.wingID, temp);
        }

        return dict;
    }
}
#endregion