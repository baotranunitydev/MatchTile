using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Purchasing;
using Utils;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace IAP.Config
{
    public enum KEY_IAP
    {
        KEY_300_START,
        KEY_1000_START,
        KEY_SUB,
    }

    [Serializable]
    public struct IAPItemModel
    {
        public string productId;
        public ProductType productType;
        public string name;
        public string description;
        public int value;
        [HideInInspector]
        public string price;
    }

    [CreateAssetMenu(fileName = "IAPStoreConfigSO", menuName = "IAP/IAPConfig")]
    public class IAPStoreConfigSO : ScriptableObject
    {
        public SerializableDictionary<KEY_IAP, IAPItemModel> dicIAPItemModel;
    }
}