using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using IAP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPurchase : PopupBase
{
    [SerializeField] private IAPItem iAPItem;
    [SerializeField] private Button btnClose;
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
        // InitBtnBuy();
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

    public void ActionBuySuccess()
    {
        audioController.PlaySound(SoundName.Coin);
        userData.IncreaseResource(ResourceType.Star, amount);
        GameHelper.Instance.GamePlayController.UpdateStarText();
        Debug.Log("Success");
    }

    public void ActionBuyFail()
    {
        // gameHelper.GamePlayController.StateGame = StateGame.PlayGame;
        // HidePopup();
    }


    public async UniTask InitPopupPurchase()
    {
        // await UniTask.WaitUntil(() => iAPItem.TypeStatusIAP != TypeStatusIAP.None);
        // if (iAPItem.TypeStatusIAP == TypeStatusIAP.InitFailed) return;
        await UniTask.CompletedTask;
    }
}
