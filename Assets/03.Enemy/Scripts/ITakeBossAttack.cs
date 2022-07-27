using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ITakeBossAttack
{
    void TakeBossAttack(Transform hitTr, float damage, float knockbackSize);
}
