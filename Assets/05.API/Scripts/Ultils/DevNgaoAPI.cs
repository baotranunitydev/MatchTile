
using Best.HTTP;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace DevNgao.API
{
    public static class DevNgaoAPI
    {
        public static string token;

        public async static UniTask<(T, bool)> GetAPI<T>(string url, bool isNoti = true, Action<string> OnJson = null)
        {
            //Debug.Log($"Token: {token}");
            Debug.Log($"Url: {url}");
            Debug.Log($"Token: {token}");
            var tcs = new TaskCompletionSource<(T, bool)>();
            HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Get, (request, respone) =>
            {
                Debug.Log("Gettttt");
                if (!respone.IsSuccess)
                {
                    var convertResult = default(T);
                    var valueTuple = (convertResult, respone.IsSuccess);
                    tcs.SetResult(valueTuple);
                }
                Debug.Log(url);
                var neokoResponse = DevNgaoUlils.ConvertDataFormJson<DevNgaoResponse>(respone.DataAsText);
                OnJson?.Invoke(JsonConvert.SerializeObject(neokoResponse.data));
                DevNgaoUlils.DebugResponse(neokoResponse, respone.IsSuccess);
                if (neokoResponse.data != null)
                {
                    var convertResult = DevNgaoUlils.ConvertDataFormJson<T>(neokoResponse.data.ToString());
                    var valueTuple = (convertResult, respone.IsSuccess);
                    tcs.SetResult(valueTuple);
                }
            });
            request.AddHeader("Authorization", "Bearer " + token);
            request.Send();
            return await tcs.Task.AsUniTask();
        }

        public async static UniTask<(T, bool)> GetAPI<T>(string url, string refreshToken)
        {
            var tcs = new TaskCompletionSource<(T, bool)>();
            HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Get, (request, respone) =>
            {
                if (!respone.IsSuccess)
                {
                    var convertResult = default(T);
                    var valueTuple = (convertResult, respone.IsSuccess);
                    tcs.SetResult(valueTuple);
                }
                var devNgaoResponse = DevNgaoUlils.ConvertDataFormJson<DevNgaoResponse>(respone.DataAsText);
                DevNgaoUlils.DebugResponse(devNgaoResponse, respone.IsSuccess);
                if (devNgaoResponse.data != null)
                {
                    var convertResult = DevNgaoUlils.ConvertDataFormJson<T>(devNgaoResponse.data.ToString());
                    var valueTuple = (convertResult, respone.IsSuccess);
                    tcs.SetResult(valueTuple);
                }
            });
            request.AddHeader("Authorization", "Bearer " + refreshToken);
            request.Send();
            return await tcs.Task.AsUniTask();
        }

        public async static UniTask<(T1, bool)> PostAPI<T, T1>(string url, T data) where T : struct
        {
            var tcs = new TaskCompletionSource<(T1, bool)>();
            HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Post, (request, respone) =>
            {
                var devNgaoResponse = DevNgaoUlils.ConvertDataFormJson<DevNgaoResponse>(respone.DataAsText);
                if (!respone.IsSuccess)
                {
                    var convertResult = default(T1);
                    var valueTuple = (convertResult, respone.IsSuccess);
                    tcs.SetResult(valueTuple);
                }
                if (devNgaoResponse.data != null)
                {
                    var convertResult = DevNgaoUlils.ConvertDataFormJson<T1>(devNgaoResponse.data.ToString());
                    var valueTuple = (convertResult, respone.IsSuccess);
                    tcs.SetResult(valueTuple);
                }

            });
            request.AddHeader("Authorization", "Bearer " + token);
            string jsonData = JsonConvert.SerializeObject(data);
            var bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.SetHeader("Content-Type", "application/json");
            request.UploadSettings.UploadStream = new MemoryStream(bodyRaw);
            request.Send();
            return await tcs.Task.AsUniTask();
        }

        public async static UniTask<(T1, bool)> PutAPI<T, T1>(string url, T data) where T : struct
        {
            var tcs = new TaskCompletionSource<(T1, bool)>();
            HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Put, (request, respone) =>
            {
                var devNgaoResponse = DevNgaoUlils.ConvertDataFormJson<DevNgaoResponse>(respone.DataAsText);
                if (!respone.IsSuccess)
                {
                    var convertResult = default(T1);
                    var valueTuple = (convertResult, respone.IsSuccess);
                    tcs.SetResult(valueTuple);
                }
                if (devNgaoResponse.data != null)
                {
                    var convertResult = DevNgaoUlils.ConvertDataFormJson<T1>(devNgaoResponse.data.ToString());
                    var valueTuple = (convertResult, respone.IsSuccess);
                    tcs.SetResult(valueTuple);
                }

            });
            request.AddHeader("Authorization", "Bearer " + token);
            string jsonData = JsonConvert.SerializeObject(data);
            var bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.SetHeader("Content-Type", "application/json");
            request.UploadSettings.UploadStream = new MemoryStream(bodyRaw);
            request.Send();
            return await tcs.Task.AsUniTask();
        }

        public async static UniTask<(string, bool)> PostAPIGetString<T>(string url, T data, bool isNoti = true) where T : struct
        {
            var tcs = new TaskCompletionSource<(string, bool)>();
            HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Post, (request, respone) =>
            {
                Debug.Log($"Post: {respone.DataAsText}");
                var valueTuple = (respone.DataAsText, respone.IsSuccess);
                tcs.SetResult(valueTuple);
            });
            request.AddHeader("Authorization", "Bearer " + token);
            string jsonData = JsonConvert.SerializeObject(data);
            var bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.SetHeader("Content-Type", "application/json");
            request.UploadSettings.UploadStream = new MemoryStream(bodyRaw);
            request.Send();
            Debug.Log($"Post Url: {url} - {jsonData}");
            return await tcs.Task.AsUniTask();
        }

        public async static UniTask<(string, bool)> PutAPIGetString<T>(string url, T data, bool isNoti = true) where T : struct
        {
            var tcs = new TaskCompletionSource<(string, bool)>();
            HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Put, (request, respone) =>
            {
                Debug.Log($"Url: {respone.DataAsText}");
                var valueTuple = (respone.DataAsText, respone.IsSuccess);
                tcs.SetResult(valueTuple);
            });
            request.AddHeader("Authorization", "Bearer " + token);
            string jsonData = JsonConvert.SerializeObject(data);
            var bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.SetHeader("Content-Type", "application/json");
            request.UploadSettings.UploadStream = new MemoryStream(bodyRaw);
            request.Send();
            Debug.Log($"Url: {url} - {jsonData}");
            return await tcs.Task.AsUniTask();
        }

        public async static UniTask<bool> PutAPIGetStringParams(string url)
        {
            var tcs = new TaskCompletionSource<bool>();
            HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Put, (request, respone) =>
            {
                tcs.SetResult(respone.IsSuccess);
            });
            request.AddHeader("Authorization", "Bearer " + token);
            request.Send();
            return await tcs.Task.AsUniTask();
        }

        public async static UniTask<(string, bool)> GetAPIString(string url, bool isNoti = true)
        {
            var tcs = new TaskCompletionSource<(string, bool)>();
            HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Get, (request, respone) =>
            {
                var valueTuple = (respone.DataAsText, respone.IsSuccess);
                tcs.SetResult(valueTuple);
            });
            request.AddHeader("Authorization", "Bearer " + token);
            request.Send();
            return await tcs.Task.AsUniTask();
        }

        public async static UniTask<(string, bool)> GetAPIStringIp(string url, bool isNoti = true)
        {
            var tcs = new TaskCompletionSource<(string, bool)>();
            HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Get, (request, respone) =>
            {
                var valueTuple = (respone.DataAsText, respone.IsSuccess);
                tcs.SetResult(valueTuple);
            });
            request.Send();
            return await tcs.Task.AsUniTask();
        }

        private static string GetUrlAvt(string url)
        {
            var newUrl = url;
            Uri uri = new Uri(newUrl);
            string path = uri.AbsolutePath;
            string newPath = uri.AbsolutePath.TrimStart('/');
            return newPath;
        }

        public async static UniTask<(Sprite, bool)> GetSpriteByUrl(string avatar)
        {
            var tcs = new TaskCompletionSource<(Sprite, bool)>();
            var url = avatar;
            var name = GetFileNameWithoutExtensionFromUrl(url);
            HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Get, (request, respone) =>
            {
                var neokoResponse = respone.DataAsTexture2D;
                var neokoResponseByte = respone.Data;
                SaveFileToPersistentDataPath(neokoResponseByte, name);
                var valueTuple = (ConvertTextureToSprite(neokoResponse), respone.IsSuccess);
                tcs.SetResult(valueTuple);
            });
            request.DownloadSettings.ContentStreamMaxBuffered = long.MaxValue;
            request.Send();
            return await tcs.Task.AsUniTask();
        }

        private static Sprite ConvertTextureToSprite(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }


        private static void SaveFileToPersistentDataPath(byte[] data, string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            try
            {
                File.WriteAllBytes(path, data);
                Debug.Log("File saved to " + path);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to save file: " + e.Message);
            }
        }
        public static async UniTask<(Sprite, bool)> LoadImageFromPersistentDataPath(string avatar)
        {
            var tcs = new TaskCompletionSource<(Sprite, bool)>();
            if (string.IsNullOrEmpty(avatar))
            {
                Texture2D texture = new Texture2D(2, 2);
                Sprite sprite = ConvertTextureToSprite(texture);
                var valueTuple = (sprite, false);
                tcs.SetResult(valueTuple);
            }
            else
            {
                var name = GetFileNameWithoutExtensionFromUrl(avatar);
                string path = Path.Combine(Application.persistentDataPath, name);
                if (File.Exists(path))
                {
                    byte[] fileData = File.ReadAllBytes(path);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    Sprite sprite = ConvertTextureToSprite(texture);
                    var result = (sprite, true);
                    tcs.SetResult(result);
                }
                else
                {
                    Debug.LogError("File not found at " + path);
                    var result = await GetSpriteByUrl(avatar);
                    tcs.SetResult(result);
                }
            }
            return await tcs.Task.AsUniTask();
        }

        public static async UniTask<(Sprite, bool)> LoadImageFromPersistentDataPathUrl(string url)
        {
            var tcs = new TaskCompletionSource<(Sprite, bool)>();
            if (string.IsNullOrEmpty(url))
            {
                Texture2D texture = new Texture2D(2, 2);
                Sprite sprite = ConvertTextureToSprite(texture);
                var valueTuple = (sprite, false);
                tcs.SetResult(valueTuple);
            }
            else
            {
                var name = GetFileNameWithoutExtensionFromUrl(url);
                string path = Path.Combine(Application.persistentDataPath, name);
                if (File.Exists(path))
                {
                    byte[] fileData = File.ReadAllBytes(path);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    Sprite sprite = ConvertTextureToSprite(texture);
                    var result = (sprite, true);
                    tcs.SetResult(result);
                }
                else
                {
                    Debug.LogError($"Time: {Time.time} - File not found at: {path}");
                    var result = await GetSpriteByUrl(url);
                    Debug.Log($"Time: {Time.time}");
                    tcs.SetResult(result);
                }
            }
            return await tcs.Task.AsUniTask();
        }

        public static string GetFileNameWithoutExtensionFromUrl(string url)
        {
            int lastSlashIndex = url.LastIndexOf('/');

            int lastDotIndex = url.LastIndexOf('.');

            if (lastSlashIndex != -1 && lastDotIndex != -1 && lastSlashIndex < lastDotIndex)
            {
                return url.Substring(lastSlashIndex + 1, lastDotIndex - lastSlashIndex - 1);
            }

            return string.Empty;
        }
    }
}

