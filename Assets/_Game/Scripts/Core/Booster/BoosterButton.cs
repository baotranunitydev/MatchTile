using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterButton : MonoBehaviour
{
    [SerializeField] private Button btnBooster;
    [SerializeField] private TextMeshProUGUI txtAmount;

    public void InitButton(UnityAction actionButton)
    {
        btnBooster.onClick.RemoveAllListeners();
        btnBooster.onClick.AddListener(actionButton);
    }

    public void SetAmountText(int amount)
    {
        txtAmount.text = amount > 0 ? $"{amount}" : "+";
    }

    public void SetInteracbleButton(bool isInteracable)
    {
        btnBooster.interactable = isInteracable;
    }
}
