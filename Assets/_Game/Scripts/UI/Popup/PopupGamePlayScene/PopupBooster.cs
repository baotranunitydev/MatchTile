using PopupLoading;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBooster : PopupBase
{
    [SerializeField] private Image imgIconBooster;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtAmount;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnBuy;
    private AudioController audioController;
    private VibrateController vibrateController;
    private UserData userData => APIController.Instance.UserDataAsset.Data;
    private GameHelper gameHelper;
    private BoosterType boosterType;
    private int price;
    private int amount;
    private APIController apiController => APIController.Instance;
    public override void InitPopup()
    {
        audioController = AudioController.Instance;
        vibrateController = VibrateController.Instance;
        gameHelper = GameHelper.Instance;
        InitBtnClose();
        InitBtnBuy();
        base.InitPopup();
    }

    private void InitBtnClose()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            gameHelper.GamePlayController.StateGame = StateGame.PlayGame;
            HidePopup();
        });
    }

    private bool isCanBuy()
    {
        bool isCanBuy = false;
        if (userData.star >= price)
        {
            isCanBuy = true;
        }
        return isCanBuy;
    }


    private void InitBtnBuy()
    {
        btnBuy.onClick.RemoveAllListeners();
        btnBuy.onClick.AddListener(async () =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.Coin);
            if (isCanBuy())
            {
                var itemType = (int)boosterType;
                var result = await APIController.Instance.PostBuyItem(itemType);
                if (result)
                {
                    GameHelper.Instance.GamePlayController.UpdateStarText();
                    GameHelper.Instance.BoosterController.SetAmountTextByBoosterType(boosterType);
                }
            }
            else
            {
                var popupLoading = PopupLoading.Enum.PopupController.Instance;
                popupLoading.ShowLoading(new List<PopupLoading.Enum.TypeResponse> { PopupLoading.Enum.TypeResponse.GetUserData });
                var reloadData = await APIController.Instance.UserDataAsset.LoadingData();
                if (reloadData == LoadingType.Fail)
                {
                    APIController.Instance.PopupWarning.ShowPopup();
                    return;
                }
                GameHelper.Instance.GamePlayController.UpdateStarText();
                popupLoading.HideLoading(PopupLoading.Enum.TypeResponse.GetUserData);
            }
            HidePopup(() => gameHelper.GamePlayController.StateGame = StateGame.PlayGame);
        });
    }

    public void InitPopupbooster(BoosterType boosterType, Sprite sprIconBooster, int amount, int price)
    {
        this.boosterType = boosterType;
        this.amount = amount;
        this.price = price;
        btnBuy.interactable = isCanBuy();
        SetIconBooster(sprIconBooster);
        SetNameBooster(boosterType.ToString());
        SetAmountBoosterBuy(amount);
        SetSpriceBuy(price);
    }

    private void SetIconBooster(Sprite sprIconBooster)
    {
        imgIconBooster.sprite = sprIconBooster;
    }

    private void SetAmountBoosterBuy(int amount)
    {
        txtAmount.text = $"{amount}";
    }

    private void SetNameBooster(string name)
    {
        txtName.text = name;
    }

    private void SetSpriceBuy(int price)
    {
        txtPrice.text = $"BUY {price}<sprite=0>";
    }
}
