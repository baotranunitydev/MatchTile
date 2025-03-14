using System;
using IAP.Config;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace IAP
{
    public class IAPController : MonoBehaviour, IDetailedStoreListener
    {
        [SerializeField] private IAPView iAPView;
        [SerializeField] private IAPStoreConfigSO iAPStoreConfigSO;
        public Action<string> onPurchaseSuccess;
        public Action<string> onPurchaseFail;
        private IStoreController storeController;
        private IExtensionProvider storeExtensionProvider;
        public static IAPController Instance;

        public IStoreController StoreController { get => storeController; }

        private bool IsInitialized()
        {
            return storeController != null && storeExtensionProvider != null;
        }
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            InitIAP();
            iAPView.Init();
        }

        private void InitIAP()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            foreach (var item in iAPStoreConfigSO.dicIAPItemModel)
            {
                var key = item.Key;
                var value = item.Value;
                builder.AddProduct(value.productId, value.productType);
            }
            UnityPurchasing.Initialize(this, builder);
        }

        [Button]
        public (IAPItemModel, Product) GetIAPItemModelByProductId(KEY_IAP keyIAP)
        {
            try
            {
                if (!IsInitialized())
                {
                    var IAPItemModel = new IAPItemModel();
                    Debug.LogError("IsInitialized -- false");
                    return (IAPItemModel, null);
                }
                else
                {
                    var model = iAPStoreConfigSO.dicIAPItemModel[keyIAP];
                    var product = storeController.products.WithID(model.productId);
                    var price = product.metadata.localizedPriceString;
                    var result = new IAPItemModel()
                    {
                        productId = model.productId,
                        productType = model.productType,
                        name = model.name,
                        description = model.description,
                        value = model.value,
                        price = price,
                    };
                    return (result, product);
                }
            }
            catch (Exception error)
            {
                Debug.LogError($"Exception - {error}");
                var IAPItemModel = new IAPItemModel();
                return (IAPItemModel, null);
            }
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            var productId = product.definition.id;
            onPurchaseFail?.Invoke(productId);
            iAPView.ShowPopupIAPStatus(TypeStatusPurchase.Fail);
            // Debug.LogError($"OnPurchaseFailed - {JsonConvert.SerializeObject(product)} || Reason - {JsonConvert.SerializeObject(failureDescription)}");
            Debug.LogError($"OnPurchaseFailed Reason - {failureDescription.reason} - mess: {failureDescription.message} - id: {failureDescription.productId}");
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"OnInitializeFailed Reason - {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"OnInitializeFailed - {message}\nReason - {error}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var product = purchaseEvent.purchasedProduct;
            var productId = product.definition.id;
            onPurchaseSuccess?.Invoke(productId);
            iAPView.ShowPopupIAPStatus(TypeStatusPurchase.Success);
            Debug.Log($"Purchase Success: - {productId}");
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogError($"OnPurchaseFailed - {product} || Reason - {failureReason}");
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;
            storeExtensionProvider = extensions;
        }
    }
}
