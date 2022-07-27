using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Stage> StageDict { get; private set; } = new Dictionary<int, Stage>();
    public Dictionary<int, StageMonsterGenerate> StageMonsterGenerateDict { get; private set; } = new Dictionary<int, StageMonsterGenerate>();
    public Dictionary<int, Wing> WingDict { get; private set; } = new Dictionary<int, Wing>();
    public Dictionary<int, Monster> MonsterDict { get; private set; } = new Dictionary<int, Monster>();
    public Dictionary<int, AttackEquip> AttackEquipDict { get; private set; } = new Dictionary<int, AttackEquip>();

    public void Init()
    {
        // StageDict = LoadJson<StageData, int, Stage>("StageData").MakeDict();
        // StageMonsterGenerateDict = LoadJson<StageMonsterGenerateData, int, StageMonsterGenerate>("StageMonsterGenerateData").MakeDict();
        // WingDict = LoadJson<WingData, int, Wing>("WingData").MakeDict();
        MonsterDict = LoadJson<MonsterData, int, Monster>("MonsterData/MonsterData").MakeDict();
        AttackEquipDict = LoadJson<AttackEquipData, int, AttackEquip>("AttackEquipData/attackEquipData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader: ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

}
