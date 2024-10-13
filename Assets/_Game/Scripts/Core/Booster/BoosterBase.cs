using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BoosterType
{
    Hint = 1,
    Bomb = 2,
}


public abstract class BoosterBase : MonoBehaviour
{
    [SerializeField] private BoosterType boosterType;
    [SerializeField] protected BoosterButton boosterButton;

    public BoosterType BoosterType { get => boosterType; }

    public abstract void InitBooster(UnityAction onActionButton);
    public abstract void SetAmountText(int amount);
    public abstract void UseBooster();
}
