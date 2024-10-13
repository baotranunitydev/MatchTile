using Cysharp.Threading.Tasks;
using UnityEngine;
using DevNgao.API;
using Ultils;
using System;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using Loading;
public class APIController : Singleton<APIController>
{
    [SerializeField] private UserDataAsset userDataAsset;
    [SerializeField] private LoginDataAsset loginDataAsset;
    [SerializeField] private BoosterSO boosterSO;
    [SerializeField] private PopupWarning popupWarning;
    [SerializeField] private PopupWarning popupWelcome;
    public static Action<UserData> OnLoadUserData;
    public UserDataAsset UserDataAsset { get => userDataAsset; }
    private PopupLoading.Enum.PopupController popupLoading => PopupLoading.Enum.PopupController.Instance;

    public PopupWarning PopupWarning { get => popupWarning; }
    public PopupWarning PopupWelcome { get => popupWelcome; }
    public LoginDataAsset LoginDataAsset { get => loginDataAsset; }

    private void Start()
    {
        popupWarning.InitPopup();
        popupWelcome.InitPopup();
    }
    public async UniTask<bool> PostWin(int star)
    {
        popupLoading.ShowLoading(new List<PopupLoading.Enum.TypeResponse> { PopupLoading.Enum.TypeResponse.PostWin });
        var url = DevNgaoURL.GetUrlByTypePostRequest(TypePostRequest.PostWin);
        var model = new PostWinRequest { rewardStar = star };
        var result = await DevNgaoAPI.PostAPI<PostWinRequest, UserData>(url, model);
        if (result.Item2)
        {
            userDataAsset.SetData(result.Item1);
            OnLoadUserData?.Invoke(userDataAsset.Data);
        }
        else
        {
            popupWarning.ShowPopup();
            popupWarning.SetActionHide(() =>
            {
                LoadingSceneController.Instance.ChangeScene(SceneType.GamePlay);
            });
        }
        popupLoading.HideLoading(PopupLoading.Enum.TypeResponse.PostWin);
        return result.Item2;
    }

    public async UniTask<bool> PostBuyItem(int typeItem)
    {
        popupLoading.ShowLoading(new List<PopupLoading.Enum.TypeResponse> { PopupLoading.Enum.TypeResponse.PostBuyItem });
        var url = DevNgaoURL.GetUrlByTypePostRequest(TypePostRequest.PostBuyItem);
        var model = new PostBuyItem { itemType = typeItem };
        var result = await DevNgaoAPI.PostAPI<PostBuyItem, UserData>(url, model);
        if (result.Item2)
        {
            userDataAsset.SetData(result.Item1);
            OnLoadUserData?.Invoke(userDataAsset.Data);
        }
        else
        {
            Debug.Log("Faill");
            popupWarning.ShowPopup();
        }
        popupLoading.HideLoading(PopupLoading.Enum.TypeResponse.PostBuyItem);
        return result.Item2;
    }

    public async UniTask<bool> PostUseItem(int typeItem)
    {
        popupLoading.ShowLoading(new List<PopupLoading.Enum.TypeResponse> { PopupLoading.Enum.TypeResponse.PostUseItem });
        var url = DevNgaoURL.GetUrlByTypePostRequest(TypePostRequest.PostUseItem);
        var model = new PostBuyItem { itemType = typeItem };
        var result = await DevNgaoAPI.PostAPI<PostBuyItem, UserData>(url, model);
        if (result.Item2)
        {
            userDataAsset.SetData(result.Item1);
            OnLoadUserData?.Invoke(userDataAsset.Data);
        }
        else
        {
            popupWarning.ShowPopup();
        }
        popupLoading.HideLoading(PopupLoading.Enum.TypeResponse.PostUseItem);
        return result.Item2;
    }

    public async UniTask<bool> GetItemConfig()
    {
        popupLoading.ShowLoading(new List<PopupLoading.Enum.TypeResponse> { PopupLoading.Enum.TypeResponse.GetItemConfig });
        var url = DevNgaoURL.GetUrlByTypeGetRequestGame(TypeGetRequest.GetConfig);
        var result = await DevNgaoAPI.GetAPI<List<GetItemConfig>>(url);
        if (result.Item2)
        {
            for (int i = 0; i < result.Item1.Count; i++)
            {
                var current = result.Item1[i];
                var booster = boosterSO.GetBoosterByType(current.boosterType);
                if (booster == null) continue;
                booster.price = current.price;
                booster.amount = current.amount;
            }
        }
        else
        {
            popupWarning.ShowPopup();
        }
        popupLoading.HideLoading(PopupLoading.Enum.TypeResponse.GetItemConfig);
        return result.Item2;
    }
}

public struct PostWinRequest
{
    public int rewardStar;
}
public struct PostBuyItem
{
    public int itemType;
}

[Serializable]
public struct GetItemConfig
{
    [JsonProperty("typeItem")]
    public BoosterType boosterType;
    [JsonProperty("amount")]
    public int amount;
    [JsonProperty("price")]
    public int price;
}
