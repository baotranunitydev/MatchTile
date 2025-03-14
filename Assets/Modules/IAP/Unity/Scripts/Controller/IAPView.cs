using UnityEngine;
namespace IAP
{
    public class IAPView : MonoBehaviour
    {
        [SerializeField] private PopupIAPStatus popupIAPStatus;

        public void ShowPopupIAPStatus(TypeStatusPurchase typeStatusPurchase)
        {
            popupIAPStatus.ShowPopup(typeStatusPurchase);
        }

        public void Init()
        {
            popupIAPStatus.InitPopup();
        }
    }
}

