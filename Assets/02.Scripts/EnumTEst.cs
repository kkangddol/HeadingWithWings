using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum enumtest
{
    one,
    two,
    three,
    four,
    five,
    six
}

public class EnumTEst : MonoBehaviour
{
    int[] babo = new int[System.Enum.GetValues(typeof(enumtest)).Length];
    private void Start() {
        Debug.Log(System.Enum.GetValues(typeof(enumtest)).Length);
        Debug.Log(babo[0]);
        Debug.Log(babo[1]);
        Debug.Log(babo[2]);
        Debug.Log(babo[3]);
        Debug.Log(babo[4]);
        Debug.Log(babo[5]);
    }
}
