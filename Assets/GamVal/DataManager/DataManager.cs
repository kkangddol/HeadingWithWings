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
    public Dictionary<int, Monster> MonsterDict { get; private set; } = new Dictionary<int, Monster>();

    public void Init()
    {
        StageDict = LoadJson<StageData, int, Stage>("StageData").MakeDict();
        StageMonsterGenerateDict = LoadJson<StageMonsterGenerateData, int, StageMonsterGenerate>("StageMonsterGenerateData").MakeDict();
        MonsterDict = LoadJson<MonsterData, int, Monster>("MonsterData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader: ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
