using Cysharp.Threading.Tasks;
using DevNgao.API;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Ultils;
using UnityEngine;
using Ultils.Encryption;
using System;

[CreateAssetMenu(fileName = "LoginDataAsset", menuName = "SO/LoginDataAsset")]
public class LoginDataAsset : ServerDataAsset<LoginData>
{
    [SerializeField] private UserInfo userInfo;

    public UserInfo UserInfo { get => userInfo; }

    public void SetIsNew()
    {
        data.isNew = false;
    }

    public void SetUserInfo(UserInfo usrInfo)
    {
        this.userInfo = usrInfo;
    }
    protected override async UniTask GetData()
    {
        var url = DevNgaoURL.GetUrlByTypePostRequest(TypePostRequest.PostLoginTelegram);
        var loginRequest = new LoginDataRequest
        {
            firstName = userInfo.firstName,
            lastName = userInfo.lastName,
            userName = userInfo.userName,
            telegramId = userInfo.id.ToString(),
        };
        var item = await DevNgaoAPI.PostAPI<LoginDataRequest, LoginData>(url, loginRequest);
        if (item.Item2)
        {
            data = item.Item1;
            type = LoadingType.Success;
        }
        else
        {
            data = new LoginData();
            type = LoadingType.Fail;
        }
        await base.GetData();
    }
}

[Serializable]
public struct LoginData
{
    public UserData userData;
    public bool isNew;
    public string accessToken;
    public string refreshToken;
}
[Serializable]
public struct LoginDataRequest
{
    [JsonProperty("firstName")]
    public string firstName;
    [JsonProperty("lastName")]
    public string lastName;
    [JsonProperty("userName")]
    public string userName;
    [JsonProperty("telegramId")]
    public string telegramId;
}
[Serializable]
public struct LoginRequest
{
    [JsonProperty("data")]
    public string data;
}