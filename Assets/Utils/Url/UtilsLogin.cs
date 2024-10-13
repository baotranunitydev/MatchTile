using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace Ultils
{
    public static class UtilsLogin
    {
        public static UserInfo GetUserIdTelegram(string url)
        {
            var userInfo = new UserInfo();

            try
            {
                Uri uri = new Uri(url);
                var queryParamsStart = System.Web.HttpUtility.ParseQueryString(uri.Query);
                string startParam = queryParamsStart.Get("tgWebAppStartParam"); // Lấy start_param

                string[] urlParts = url.Split('#');
                if (urlParts.Length > 1)
                {
                    string queryString = urlParts[1];

                    string[] queryParams = queryString.Split('&');
                    string tgWebAppData = queryParams[0].Split('=')[1];

                    string decodedQuery = UnityWebRequest.UnEscapeURL(tgWebAppData);

                    int startIndex = decodedQuery.IndexOf("user=") + "user=".Length;
                    string userDataEncoded = decodedQuery.Substring(startIndex).Split('&')[0];

                    string userDataDecoded = UnityWebRequest.UnEscapeURL(userDataEncoded);

                    userInfo = JsonConvert.DeserializeObject<UserInfo>(userDataDecoded);

                    Debug.Log($"UserId: {userInfo.id} ----- {userDataDecoded}");
                    userInfo.refferalFrom = startParam;
                }
                else
                {
                    Debug.LogError("Không tìm thấy tgWebAppData trong URL");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Lỗi khi giải mã URL: " + ex.Message);
            }

            return userInfo;
        }

    }

    [Serializable]
    public struct UserInfo
    {
        [JsonProperty("id")]
        public long id;
        [JsonProperty("first_name")]
        public string firstName;
        [JsonProperty("last_name")]
        public string lastName;
        [JsonProperty("username")]
        public string userName;
        [JsonProperty("language_code")]
        public string languageCode;
        [JsonProperty("is_premium")]
        public bool isPremeium;
        [JsonProperty("allows_write_to_pm")]
        public bool isAllowWriteToPm;
        [JsonProperty("referral_from")]
        public string refferalFrom;
    }
}
