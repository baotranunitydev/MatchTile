using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPurchase : PopupBase
{
    [SerializeField] private TextMeshProUGUI txtAmount;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnBuy;
    private AudioController audioController;
    private VibrateController vibrateController;
    private UserData userData;
    private GameHelper gameHelper;
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

    private void ActionBuySuccess()
    {
        audioController.PlaySound(SoundName.Coin);
        userData.InscreaseResource(ResourceType.Star, amount);
        GameHelper.Instance.GamePlayController.UpdateStarText();
    }


    private void InitBtnBuy()
    {
        btnBuy.onClick.RemoveAllListeners();
        btnBuy.onClick.AddListener(() =>
        {
            vibrateController.Vibrate();
            Debug.Log("Buyy");
        });
    }

    public void InitPopupPurchase(int amount, int price)
    {
        this.amount = amount;
        SetAmount(amount);
        SetPriceBuy(price);
    }

    private void SetAmount(int amount)
    {
        txtAmount.text = $"{amount}";
    }

    private void SetPriceBuy(int price)
    {
        txtPrice.text = $"{price} VND";
    }
}
