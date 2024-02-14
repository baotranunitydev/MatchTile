using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoosterBomb : BoosterBase
{
    private BoardController boardController;
    public override void InitBooster(UnityAction onActionButton)
    {
        boardController = GameHelper.Instance.BoardController;
        boosterButton.InitButton(onActionButton);
    }

    public override void SetAmountText(int amount)
    {
        boosterButton.SetAmountText(amount);
    }

    public override void UseBooster()
    {
        AddForceAllTile();
    }

    private void AddForceAllTile()
    {
        for (int i = 0; i < boardController.LstTile.Count; i++)
        {
            var tile = boardController.LstTile[i];
            tile.TileView.AddForce();
        }
    }
}
