using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class BoosterController : MonoBehaviour
{
    [SerializeField] private BoosterSO boosterSO;
    [SerializeField] private List<BoosterBase> lstBooster;
    private UserData userData;
    private BoosterHint boosterHint;
    private PopupBooster popupBooster;
    private GameHelper gameHelper;
    private BoosterBase GetBoosterByBoosterType(BoosterType boosterType) => lstBooster.Find(booster => booster.BoosterType == boosterType);
    private void Start()
    {
        userData = DBController.Instance.USER_DATA;
        gameHelper = GameHelper.Instance;
        SetBoosterHint();
        SetPopupBooster();
        InitBooster();
    }

    private void SetBoosterHint()
    {
        boosterHint = lstBooster.Find(booster => booster.BoosterType == BoosterType.Hint) as BoosterHint;
    }

    private void SetPopupBooster()
    {
        var popupBooster = GameHelper.Instance.PopupController.GetPopupByType(PopupType.PopupBooster, false) as PopupBooster;
        this.popupBooster = popupBooster;
    }

    public void CheckIsCanUseHint()
    {
        if (boosterHint == null) return;
        boosterHint.CheckIsCanUseHint();
    }

    private void InitBooster()
    {
        for (int i = 0; i < lstBooster.Count; i++)
        {
            var booster = lstBooster[i];
            booster.InitBooster(() =>
            {
                VibrateController.Instance.Vibrate();
                AudioController.Instance.PlaySound(SoundName.ClickBtn);
                var amount = GetAmountBooster(booster.BoosterType);
                if (amount > 0)
                {
                    booster.UseBooster();
                    DescreaseBooster(booster.BoosterType, 1);
                    SetAmountTextByBoosterType(booster.BoosterType);
                }
                else
                {
                    var boosterModel = boosterSO.GetBoosterByType(booster.BoosterType);
                    if (popupBooster == null || booster == null) return;
                    popupBooster.InitPopupbooster(boosterModel.boosterType, boosterModel.sprBooster, boosterModel.amount, boosterModel.price);
                    gameHelper.GamePlayController.StateGame = StateGame.PauseGame;
                    popupBooster.gameObject.SetActive(true);
                    popupBooster.ShowPopup();
                }
            });
            SetAmountTextByBoosterType(booster.BoosterType);
        }
    }

    public void SetAmountTextByBoosterType(BoosterType boosterType)
    {
        var booster = GetBoosterByBoosterType(boosterType);
        if (booster == null) return;
        booster.SetAmountText(GetAmountBooster(booster.BoosterType));
    }

    private int GetAmountBooster(BoosterType boosterType)
    {
        var amount = 0;
        switch (boosterType)
        {
            case BoosterType.Hint:
                amount = userData.amountHint;
                break;
            case BoosterType.Bomb:
                amount = userData.amountBomb;
                break;
        }
        return amount;
    }

    private void InscreaseBooster(BoosterType boosterType, int amount)
    {
        switch (boosterType)
        {
            case BoosterType.Hint:
                userData.InscreaseResource(ResourceType.Hint, amount);
                break;
            case BoosterType.Bomb:
                userData.DescreaseResource(ResourceType.Bomb, amount);
                break;
        }
    }

    private void DescreaseBooster(BoosterType boosterType, int amount)
    {
        switch (boosterType)
        {
            case BoosterType.Hint:
                userData.DescreaseResource(ResourceType.Hint, amount);
                break;
            case BoosterType.Bomb:
                userData.DescreaseResource(ResourceType.Bomb, amount);
                break;
        }
    }
}
