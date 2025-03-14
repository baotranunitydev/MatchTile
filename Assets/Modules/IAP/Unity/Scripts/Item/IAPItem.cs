using System;
using IAP.Config;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace IAP
{
    public enum TypeStatusIAP
    {
        None,
        InitSuccess,
        InitFailed
    }
    public class IAPItem : MonoBehaviour
    {
        [SerializeField] private KEY_IAP keyIAP;
        [SerializeField] private UnityEvent onPurchaseSuccess;
        [SerializeField] private UnityEvent onPurchaseFail;
        [SerializeField] protected Button btnIAP;
        [SerializeField] protected TextMeshProUGUI txtBuy;
        [SerializeField] protected TextMeshProUGUI txtName;
        [SerializeField] protected TextMeshProUGUI txtPrice;
        [SerializeField] protected TextMeshProUGUI txtValue;

        private IAPController iAPController => IAPController.Instance;
        protected IAPItemModel model;
        protected Product product;
        private TypeStatusIAP typeStatusIAP;

        public IAPItemModel Model { get => model; }
        public TypeStatusIAP TypeStatusIAP { get => typeStatusIAP; }
        public Product Product { get => product; }

        private void Start()
        {
            InitIAPItem();
        }

        protected void InitIAPItem()
        {
            if (iAPController == null) return;
            var result = iAPController.GetIAPItemModelByProductId(keyIAP);
            model = result.Item1;
            product = result.Item2;
            var isSuccess = !string.IsNullOrEmpty(model.productId);
            typeStatusIAP = isSuccess ? TypeStatusIAP.InitSuccess : TypeStatusIAP.InitSuccess;
            if (isSuccess)
            {
                InitBtnBuy(model.productId);
                InitUiIAPItem(model, product);
                iAPController.onPurchaseSuccess += OnPurchaseSuccess;
                iAPController.onPurchaseFail += OnPurchaseFail;
            }
            btnIAP.interactable = isSuccess;
        }

        protected virtual void OnDestroy()
        {
            if (iAPController == null) return;
            iAPController.onPurchaseSuccess -= OnPurchaseSuccess;
            iAPController.onPurchaseFail -= OnPurchaseFail;
        }

        protected virtual void InitBtnBuy(string productId)
        {
            btnIAP.onClick.RemoveAllListeners();
            btnIAP.onClick.AddListener(() =>
            {
                VibrateController.Instance.Vibrate();
                iAPController.StoreController.InitiatePurchase(productId);
            });
        }

        protected virtual void InitUiIAPItem(IAPItemModel model, Product product)
        {
            txtName.text = model.name;
            txtPrice.text = product.metadata.localizedPriceString.Replace("₫", "VND");
            txtValue.text = model.value.ToString();
        }

        protected virtual void OnPurchaseSuccess(string productId)
        {
            if (productId == model.productId)
            {
                onPurchaseSuccess?.Invoke();
            }
        }

        protected virtual void OnPurchaseFail(string productId)
        {
            if (productId == model.productId)
            {
                onPurchaseFail?.Invoke();
            }
        }
    }
}
