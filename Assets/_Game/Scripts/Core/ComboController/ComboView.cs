using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboView : MonoBehaviour
{
    [SerializeField] private Image imgFillCombo;
    [SerializeField] private TextMeshProUGUI txtCombo;

    public void SetTextCombo(int combo)
    {
        txtCombo.text = $"combo x{combo}";
    }

    public void SetFillAmountCombo(float value)
    {
        imgFillCombo.fillAmount = value;
    }
}
