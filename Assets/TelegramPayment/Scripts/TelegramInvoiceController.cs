using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace TelegramPayment
{
    public class TelegramInvoiceController : MonoBehaviour
    {
        public static TelegramInvoiceController Instance { get; private set; }
        private void Start()
        {
            Instance = this;
            DontDestroyOnLoad(this);
            InitTelegramApp();
        }

        private void InitTelegramApp()
        {
            WebAppAPIBridge.LoadTelegramWebAppScriptWeb();
        }

        public void OpenInvoice(string invoiceUrl)
        {
            Debug.Log($"Invoice link: {invoiceUrl}");
            WebAppAPIBridge.OpenPurchaseInvoice(invoiceUrl);
        }

        [Button]
        public async void HandlePaymentStatus(string status)
        {
            Debug.Log($"Handle status Unity: {status}");
            await UniTask.Yield();
            var isSuccess = status == "paid";
            if (isSuccess)
            {
                Debug.Log("Payment Success");
            }
            else
            {
                Debug.Log("Payment Fail or Close");
            }
        }
    }
}