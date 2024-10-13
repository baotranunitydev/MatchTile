using PopupLoading;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class BoosterController : MonoBehaviour
{
    [SerializeField] private BoosterSO boosterSO;
    [SerializeField] private List<BoosterBase> lstBooster;
    private BoosterHint boosterHint;
    private PopupBooster popupBooster;
    private GameHelper gameHelper;
    private UserData userData => APIController.Instance.UserDataAsset.Data;
    private BoosterBase GetBoosterByBoosterType(BoosterType boosterType) => lstBooster.Find(booster => booster.BoosterType == boosterType);
    private void Start()
    {
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
            booster.InitBooster(async () =>
            {
                var popupLoading = PopupLoading.Enum.PopupController.Instance;
                popupLoading.ShowLoading(new List<PopupLoading.Enum.TypeResponse> { PopupLoading.Enum.TypeResponse.GetUserData });
                var getUserData = await APIController.Instance.UserDataAsset.LoadingData();
                popupLoading.HideLoading(PopupLoading.Enum.TypeResponse.GetUserData);
                if (getUserData == LoadingType.Fail)
                {
                    APIController.Instance.PopupWarning.ShowPopup();
                    return;
                }
                GameHelper.Instance.GamePlayController.UpdateStarText();
                VibrateController.Instance.Vibrate();
                AudioController.Instance.PlaySound(SoundName.ClickBtn);
                var amount = GetAmountBooster(booster.BoosterType, userData);
                Debug.Log($"Amount - {amount}");
                if (amount > 0)
                {
                    var itemType = (int)booster.BoosterType;
                    var result = await APIController.Instance.PostUseItem(itemType);
                    if (result)
                    {
                        booster.UseBooster();
                        SetAmountTextByBoosterType(booster.BoosterType);
                    }
                }
                else
                {
                    var result = await APIController.Instance.GetItemConfig();
                    if (result)
                    {
                        var boosterModel = boosterSO.GetBoosterByType(booster.BoosterType);
                        if (popupBooster == null || booster == null) return;
                        popupBooster.InitPopupbooster(boosterModel.boosterType, boosterModel.sprBooster, boosterModel.amount, boosterModel.price);
                        gameHelper.GamePlayController.StateGame = StateGame.PauseGame;
                        popupBooster.gameObject.SetActive(true);
                        popupBooster.ShowPopup();
                    }
                }
            });
            SetAmountTextByBoosterType(booster.BoosterType);
        }
    }

    public void SetAmountTextByBoosterType(BoosterType boosterType)
    {
        var booster = GetBoosterByBoosterType(boosterType);
        if (booster == null) return;
        booster.SetAmountText(GetAmountBooster(booster.BoosterType, userData));
    }

    private int GetAmountBooster(BoosterType boosterType, UserData userData)
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
}
