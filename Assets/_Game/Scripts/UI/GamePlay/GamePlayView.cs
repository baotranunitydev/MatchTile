using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePlayView : MonoBehaviour
{
    [SerializeField] private Button btnPause;
    [SerializeField] private Image imgCover;
    [SerializeField] private TextMeshProUGUI txtLevel;

    public void InitButtonBack(UnityAction actionSettings)
    {
        btnPause.onClick.RemoveAllListeners();
        btnPause.onClick.AddListener(actionSettings);
    }
    public void SetStatusImageCover(bool isStatus) => imgCover.gameObject.SetActive(isStatus);
    public void SetLevelText(int level) => txtLevel.text = $"LEVEL {level}";
}
