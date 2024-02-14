using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoosterBomb : BoosterBase
{
    public override void InitBooster(UnityAction onActionButton)
    {
        boosterButton.InitButton(onActionButton);
    }

    public override void SetAmountText(int amount)
    {
        boosterButton.SetAmountText(amount);
    }

    public override void UseBooster()
    {

    }
}
