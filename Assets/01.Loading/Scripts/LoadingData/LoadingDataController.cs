using Cysharp.Threading.Tasks;
using DevNgao.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Loading
{
    public sealed class LoadingDataController : MonoBehaviour
    {
        [SerializeField] private bool isLocal;
        [SerializeField] private LoginDataAsset _loginDataAsset;
        [SerializeField] private List<LoadingDataAssetBase> loadingDataAssets;
        [SerializeField] private string token;
        public static Action<int, int> onDoneIndexLoad;
        public static string URL_HOST_GAME = "";

        private async UniTask AutoRefreshToken()
        {
            return;
            //while (true)
            //{
            //    await UniTask.WaitForSeconds(3600, cancellationToken: this.destroyCancellationToken);
            //    var url = DevNgaoURL.GetUrlByTypeGetRequestGame(TypeGetRequest.GetRefreshToken);
            //    var itemAcess = await DevNgaoAPI.GetAPI<RequestAccessToken>(url, _loginDataAsset.Data.refreshToken);
            //    if (itemAcess.Item2)
            //    {
            //        DevNgaoAPI.token = itemAcess.Item1.accessToken;
            //    }
            //}
        }

        private async void Start()
        {
            URL_HOST_GAME = isLocal ? "http://localhost:8080/api/matchtile3d" : "https://devngao.com/api/matchtile3d";
#if UNITY_WEBGL && !UNITY_EDITOR
            var url = Application.absoluteURL;
           var isLogin = await Login(url);
            if (isLogin)
            {
                await LoadingData();
                Debug.Log("Login Success");
            }
            else
            {
                Debug.Log("Login Error");
            }
#else
            await UniTask.Yield();
#endif
        }

        //private async void Start()
        //{
        //    URL_HOST_GAME = isLocal ? "http://localhost:8080/api/matchtile3d" : "https://devngao.com/api/matchtile3d";
        //    var url = Application.absoluteURL;
        //    url = "https://games.neoko.com/Game_Ecchi_Payment_v54/index.html#tgWebAppData=query_id%3DAAFAGaAtAAAAAEAZoC1mXakQ%26user%3D%257B%2522id%2522%253A765466944%252C%2522first_name%2522%253A%2522Paul%2522%252C%2522last_name%2522%253A%2522%2522%252C%2522username%2522%253A%2522PaulUnityDev%2522%252C%2522language_code%2522%253A%2522en%2522%252C%2522is_premium%2522%253Atrue%252C%2522allows_write_to_pm%2522%253Atrue%257D%26auth_date%3D1726890201%26hash%3D4e7b6711d1f965c488c105491dbad067af7be527fdff154341b4a3173c43db16&tgWebAppVersion=7.8&tgWebAppPlatform=weba&tgWebAppBotInline=1&tgWebAppThemeParams=%7B%22bg_color%22%3A%22%23212121%22%2C%22text_color%22%3A%22%23ffffff%22%2C%22hint_color%22%3A%22%23aaaaaa%22%2C%22link_color%22%3A%22%238774e1%22%2C%22button_color%22%3A%22%238774e1%22%2C%22button_text_color%22%3A%22%23ffffff%22%2C%22secondary_bg_color%22%3A%22%230f0f0f%22%2C%22header_bg_color%22%3A%22%23212121%22%2C%22accent_text_color%22%3A%22%238774e1%22%2C%22section_bg_color%22%3A%22%23212121%22%2C%22section_header_text_color%22%3A%22%23aaaaaa%22%2C%22subtitle_text_color%22%3A%22%23aaaaaa%22%2C%22destructive_text_color%22%3A%22%23e53935%22%7D";
        //    var isLogin = await Login(url);
        //    if (isLogin)
        //    {
        //        await LoadingData();
        //        Debug.Log("Login Success");
        //    }
        //    else
        //    {
        //        Debug.Log("Login Error");
        //    }
        //}

        private async UniTask<bool> Login(string url)
        {
            Debug.Log($"Url web: {url}");
            var isSuccess = false;
            var userInfo = Ultils.UtilsLogin.GetUserIdTelegram(url);
            _loginDataAsset.SetUserInfo(userInfo);
            await UniTask.Yield();
            var item = await _loginDataAsset.LoadingData();
            if (item == LoadingType.Success)
            {
                DevNgaoAPI.token = _loginDataAsset.Data.accessToken;
                isSuccess = true;
                Debug.Log("Login Success");
            }
            else
            {
                APIController.Instance.PopupWarning.SetActionHide(() =>
                {
                    LoadingSceneController.Instance.ChangeScene(SceneType.Loading);
                });
                Debug.Log("Login Fail");
            }
            Debug.Log("Done");
            return isSuccess;
        }


        public async UniTask<bool> LoadingData()
        {
            var index = 0;
            var isSucess = true;
            foreach (var dataAsset in loadingDataAssets)
            {
                var result = await dataAsset.LoadingData();
                index++;
                onDoneIndexLoad?.Invoke(index, loadingDataAssets.Count);
                if (result == LoadingType.Fail)
                {
                    Debug.LogError($"{dataAsset.name} - Fail");
                    isSucess = false;
                    break;
                }
            }
            return isSucess;
        }
    }
}
