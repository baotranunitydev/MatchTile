using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoosterSO", menuName = "SO/BoosterSO")]
public class BoosterSO : ScriptableObject
{
    public List<Booster> lstBooster;

    public Booster GetBoosterByType(BoosterType type) => lstBooster.Find(booster => booster.boosterType == type);
}

[Serializable]
public class Booster
{
    public BoosterType boosterType;
    public Sprite sprBooster;
    public int amount;
    public int price;
}
