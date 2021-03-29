using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class GameStats : ScriptableObject
{
    public int JumpCount = 0;
    public int AttemptCount = 0;
    public float RunTime = 0;
    public int EnemiesDefeated = 0;
    public int MoneyCount = 0;
}
