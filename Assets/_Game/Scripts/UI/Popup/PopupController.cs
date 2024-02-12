using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] private PopupBase[] arrPopupBase;
    public PopupBase GetPopupByType(PopupType popupType)
    {
        var popup = Array.Find(arrPopupBase, popup => popup.PopupType == popupType);
        if (popup != null)
        {
            popup.gameObject.SetActive(true);
        }
        return popup;
    }

    private void Start()
    {
        InitPosPopup();
    }

    private void InitPosPopup()
    {
        for (int i = 0; i < arrPopupBase.Length; i++)
        {
            var popup = arrPopupBase[i];
            popup.InitPopup();
        }
    }
}
