using Cysharp.Threading.Tasks;
using UnityEngine;
using DevNgao.API;
using Ultils;
public class APIController : Singleton<APIController>
{
    [SerializeField] private UserDataAsset userDataAsset;

    public UserDataAsset UserDataAsset { get => userDataAsset;}



    public async UniTask PostWin(int star)
    {
        var url = DevNgaoURL.GetUrlByTypePostRequest(TypePostRequest.PostWin);
        var model = new PostWinRequest { rewardStar = star };
        var result = await DevNgaoAPI.PostAPI<PostWinRequest, UserInfo>(url, model);
        if (result.Item2)
        {

        }
        else
        {

        }
    }

    public async UniTask PostBuyItem(int typeItem)
    {
        var url = DevNgaoURL.GetUrlByTypePostRequest(TypePostRequest.PostBuyItem);
        var model = new PostBuyItem { itemType = typeItem };
        var result = await DevNgaoAPI.PostAPI<PostBuyItem, UserInfo>(url, model);
        if (result.Item2)
        {

        }
        else
        {

        }
    }

    public async UniTask PutUseItem(int typeItem)
    {
        var url = DevNgaoURL.GetUrlByTypePostRequest(TypePostRequest.PostBuyItem);
        var model = new PostBuyItem { itemType = typeItem };
        var result = await DevNgaoAPI.PostAPI<PostBuyItem, UserInfo>(url, model);
        if (result.Item2)
        {

        }
        else
        {

        }
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
