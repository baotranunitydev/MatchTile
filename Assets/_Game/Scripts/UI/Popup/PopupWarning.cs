using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupWarning : PopupBase
{
    [SerializeField] private TextMeshProUGUI txtDesciption;
    [SerializeField] private Button btnHide;

    public override void InitPopup()
    {
        base.InitPopup();
        btnHide.onClick.AddListener(() =>
        {
            HidePopup();
        });
    }
    public void SetActionHide(Action onHide)
    {
        this.onHide = onHide;
    }

    public void SetDescription(string description)
    {
        txtDesciption.text = description;
    }
}
