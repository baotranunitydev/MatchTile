using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace IAP
{
    public enum TypeStatusPurchase
    {
        None,
        Success,
        Fail
    }
    public class PopupIAPStatus : MonoBehaviour
    {
        [SerializeField] private float timer;
        [SerializeField] private RectTransform rtfmPopup;
        [SerializeField] private Image imgFade;
        [SerializeField] private TextMeshProUGUI txtName;
        [SerializeField] private TextMeshProUGUI txtContent;
        [SerializeField] private Button btnOk;

        public void InitPopup()
        {
            imgFade.DOFade(0, 0);
            rtfmPopup.DOScale(0, 0);
            btnOk.onClick.RemoveAllListeners();
            btnOk.onClick.AddListener(() =>
            {
                VibrateController.Instance.Vibrate();
                HidePopup();
            }
            );
            SetStatusPopup(false);
        }

        private void SetStatusPopup(bool isStatus)
        {
            rtfmPopup.gameObject.SetActive(isStatus);
            imgFade.gameObject.SetActive(isStatus);
        }

        public void ShowPopup(TypeStatusPurchase typeStatusPurchase)
        {
            SetContentByType(typeStatusPurchase);
            SetStatusPopup(true);
            imgFade.DOFade(0.5f, timer);
            rtfmPopup.DOScale(1f, timer);
        }

        public void HidePopup()
        {
            imgFade.DOFade(0f, timer);
            rtfmPopup.DOScale(0f, timer).OnComplete(() =>
            {
                SetStatusPopup(false);
            });
        }

        private void SetContentByType(TypeStatusPurchase typeStatusIAP)
        {
            var name = "";
            var content = "";
            switch (typeStatusIAP)
            {
                case TypeStatusPurchase.None:
                    break;
                case TypeStatusPurchase.Success:
                    name = "Success";
                    content = "Purchase successful! Enjoy your game!";
                    break;
                case TypeStatusPurchase.Fail:
                    name = "Failed";
                    content = "Purchase failed! Please try again.";
                    break;
            }
            SetContentPopup(name, content);
        }

        private void SetContentPopup(string name, string content)
        {
            txtName.text = name;
            txtContent.text = content;
        }

    }
}
