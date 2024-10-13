using Cysharp.Threading.Tasks;
using UnityEngine;
using DevNgao.API;
using Ultils;
using System;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
public class APIController : Singleton<APIController>
{
    [SerializeField] private UserDataAsset userDataAsset;
    [SerializeField] private BoosterSO boosterSO;
    public static Action<UserData> OnLoadUserData;
    public UserDataAsset UserDataAsset { get => userDataAsset; }

    public async UniTask<bool> PostWin(int star)
    {
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

        }
        return result.Item2;
    }

    public async UniTask<bool> PostBuyItem(int typeItem)
    {
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

        }
        return result.Item2;
    }

    public async UniTask<bool> PostUseItem(int typeItem)
    {
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

        }
        return result.Item2;
    }

    public async UniTask<bool> GetItemConfig()
    {
        var url = DevNgaoURL.GetUrlByTypeGetRequestGame(TypeGetRequest.GetConfig);
        var result = await DevNgaoAPI.GetAPI<GetListItemConfig>(url);
        if (result.Item2)
        {
            for (int i = 0; i < result.Item1.list.Count; i++)
            {
                var current = result.Item1.list[i];
                var booster = boosterSO.GetBoosterByType(current.boosterType);
                if (booster == null) continue;
                booster.price = current.price;
                booster.amount = current.amount;
            }
        }
        else
        {

        }
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
    [JsonProperty("itemType")]
    public BoosterType boosterType;
    [JsonProperty("amount")]
    public int amount;
    [JsonProperty("price")]
    public int price;
}

[Serializable]
public struct GetListItemConfig
{
    [JsonProperty("data")]
    public List<GetItemConfig> list;
}