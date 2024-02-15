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
    private UserData userData;
    private GameHelper gameHelper;
    private BoosterType boosterType;
    private int price;
    private int amount;
    public override void InitPopup()
    {
        userData = DBController.Instance.USER_DATA;
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

    private ResourceType GetResourceTypeByBoosterType(BoosterType boosterType)
    {
        ResourceType resourceType = ResourceType.Hint;
        switch (boosterType)
        {
            case BoosterType.Hint:
                resourceType = ResourceType.Hint;
                break;
            case BoosterType.Bomb:
                resourceType = ResourceType.Bomb;
                break;
        }
        return resourceType;
    }

    private void InitBtnBuy()
    {
        btnBuy.onClick.RemoveAllListeners();
        btnBuy.onClick.AddListener(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.Coin);
            HidePopup(() => gameHelper.GamePlayController.StateGame = StateGame.PlayGame);
            if (isCanBuy())
            {
                var resourceType = GetResourceTypeByBoosterType(boosterType);
                userData.InscreaseResource(resourceType, amount);
                userData.DescreaseResource(ResourceType.Star, price);
                GameHelper.Instance.GamePlayController.UpdateStarText();
                GameHelper.Instance.BoosterController.SetAmountTextByBoosterType(boosterType);
            }
        });
    }

    public void InitPopupbooster(BoosterType boosterType, Sprite sprIconBooster, int amount, int price)
    {
        btnBuy.interactable = isCanBuy();
        SetIconBooster(sprIconBooster);
        SetNameBooster(boosterType.ToString());
        SetAmountBoosterBuy(amount);
        SetSpriceBuy(price);
        this.boosterType = boosterType;
        this.amount = amount;
        this.price = price;
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
