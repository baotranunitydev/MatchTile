using Loading;
using Newtonsoft.Json;
using System;

namespace DevNgao.API
{
    public enum TypeGetRequest
    {
        None,
        //NEW ECCHO
        GetUserData,
    }

    public enum TypePostRequest
    {
        None,
        PostLoginTelegram,
        PostWin,
        PostBuyItem,
        PostUseItem
    }

    public enum TypePutRequest
    {
        None,
        PutUseItem
    }

    public static class DevNgaoURL
    {

        public static string GetUrlByTypeGetRequestGame(TypeGetRequest typeGetRequest)
        {
            string url = "";
            switch (typeGetRequest)
            {
                case TypeGetRequest.None:
                    break;
                case TypeGetRequest.GetUserData:
                    url = "/get-user-data";
                    break;
            }
            return LoadingDataController.URL_HOST_GAME + url;
        }

        public static string GetUrlByTypePostRequest(TypePostRequest typePostRequest)
        {
            string url = "";
            switch (typePostRequest)
            {
                case TypePostRequest.None:
                    break;
                case TypePostRequest.PostLoginTelegram:
                    url = "/login";
                    break;
                case TypePostRequest.PostWin:
                    url = "/win";
                    break;
                case TypePostRequest.PostBuyItem:
                    url = "/buy-item";
                    break;
                case TypePostRequest.PostUseItem:
                    url = "/use-item";
                    break;
                default:
                    break;
            }
            return LoadingDataController.URL_HOST_GAME + url;
        }

        public static string GetUrlByTypePutRequest(TypePutRequest typePutRequest)
        {
            string url = "";
            switch (typePutRequest)
            {
                case TypePutRequest.None:
                    break;
                case TypePutRequest.PutUseItem:
                    url = "/use-item";
                    break;
            }
            return LoadingDataController.URL_HOST_GAME + url;
        }
    }
}

