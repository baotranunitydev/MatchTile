using Cysharp.Threading.Tasks;
using DevNgao.API;
using UnityEngine;

[CreateAssetMenu(fileName = "UserDataAsset", menuName = "Data/UserDataAsset")]
public class UserDataAsset : ServerDataAsset<UserData>
{
    protected override async UniTask GetData()
    {
        var url = DevNgaoURL.GetUrlByTypeGetRequestGame(TypeGetRequest.GetUserData);
        var result = await DevNgaoAPI.GetAPI<UserData>(url);
        if (result.Item2)
        {
            data = result.Item1;
            type = LoadingType.Success;
        }
        else
        {
            type = LoadingType.Fail;
        }
        await base.GetData();
    }

    public void SetData(UserData data)
    {
        this.data = data;
    }
}
