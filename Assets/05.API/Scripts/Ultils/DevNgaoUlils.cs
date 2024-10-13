using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace DevNgao.API
{
    public static class DevNgaoUlils
    {

        public static void MoveToLastSibling(this Transform transform)
        {
            int siblingCount = transform.parent.childCount;
            transform.SetSiblingIndex(siblingCount - 1);
        }
        public static T ConvertDataFormJson<T>(string data)
        {
            try
            {
                Debug.Log($"Json: {data}");
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new DefaultStructConverter<DateTime>());
                return JsonConvert.DeserializeObject<T>(data, settings);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public static void DebugResponse(DevNgaoResponse neokoResponse, bool isSuccess)
        {
            if (!isSuccess)
            {
                Debug.LogError($"Respone:\nStatusCode: {neokoResponse.success}\nData: {neokoResponse.data}\nMessage: {neokoResponse.message}");
            }
            else
            {
                Debug.Log($"Respone:\nStatusCode: {neokoResponse.success}\nData: {neokoResponse.data}\nMessage: {neokoResponse.message}");
            }
        }

        public static T ParseDataToGeneric<T>(string respone) where T : struct
        {
            var result = new T();
            try
            {
                var jsonObject = JObject.Parse(respone);
                var dataJson = jsonObject["data"].ToString();
                Debug.Log($"Data Json: {dataJson}");
                result = ConvertDataFormJson<T>(dataJson);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to parse JSON: {ex.Message}");
            }
            return result;
        }
    }

}

