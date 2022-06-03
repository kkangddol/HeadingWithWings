using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WingEquipment : MonoBehaviour
{
    public int level;
    public abstract void SetLevel(int newLevel);
}
