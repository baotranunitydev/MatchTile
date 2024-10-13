using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace PopupLoading
{
    public enum PopupAlertSpecificTypeEnum
    {
        None = 0,
        Disconnect,
        SessionConflig
    }
    public class PopupAlertModel
    {
        public PopupAlertSpecificTypeEnum SpecificTypeEnum = PopupAlertSpecificTypeEnum.None;
        public string Title = "Alert";
        public string Content;
        public bool IsHideCloseBtn;
        public List<PopupAlertButtonModel> Buttons;
    }
    public class PopupAlertButtonModel
    {
        public string Content;
        public UnityAction OnClick;

    }
    public class PopupAlert : PopupBase
    {
        [SerializeField] TextMeshProUGUI txTitle;
        [SerializeField] TextMeshProUGUI txContent;
        [SerializeField] GameObject _btnClose;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Transform _buttonContainer;
        [SerializeField] private GameObject _buttonPrefab;
        private PopupAlertSpecificTypeEnum _specificTypeEnum;
        public PopupAlertSpecificTypeEnum SpecificTypeEnum { get => _specificTypeEnum; set => _specificTypeEnum = value; }

        public void SetAlert(PopupAlertModel model)
        {
            txTitle.text = model.Title;
            txContent.text = model.Content;
            _btnClose.SetActive(!model.IsHideCloseBtn);
            _specificTypeEnum = model.SpecificTypeEnum;
            foreach (Transform child in _buttonContainer.transform)
            {
                Destroy(child.gameObject);
            }
            if(model.Buttons != null)
            {
                foreach (var i in model.Buttons)
                {
                    var button = Instantiate(_buttonPrefab, _buttonContainer).GetComponent<Button>();
                    button.GetComponentInChildren<TextMeshProUGUI>().text = i.Content;
                    button.onClick.AddListener(i.OnClick);
                    button.onClick.AddListener(HidePopup);
                }
            }
        }
        public override void ShowPopup()
        {
            base.ShowPopup();
        }
        public override void HidePopup()
        {
            base.HidePopup();
        }
    }
}