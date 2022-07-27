using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemyStopHandler
{
    public bool IsStop { get; set; }
    public void StopMove();
    public void ResumeMove();
}
