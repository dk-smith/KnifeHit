using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[Serializable]
public class PlayerData
{
    private int scoreRecord = 0;
    private int scoreStage = 0;
    private int apples = 0;

    public PlayerData() {}

    public PlayerData(int scoreRecord, int scoreStage, int apples)
    {
        this.scoreRecord = scoreRecord;
        this.scoreStage = scoreStage;
        this.apples = apples;
    }

    public int ScoreRecord { get => scoreRecord; set { scoreRecord = value; } }
    public int ScoreStage { get => scoreStage; set => scoreStage = value; }
    public int Apples { get => apples; set => apples = value; }

}
