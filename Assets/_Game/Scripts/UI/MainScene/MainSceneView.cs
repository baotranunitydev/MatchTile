using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainSceneView : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnSettings;
    [SerializeField] private TextMeshProUGUI txtBtnPlay;
    public void InitButtonPlay(int level, UnityAction actionPlay)
    {
        txtBtnPlay.text = $"PLAY\nLEVEL {level:D2}";
        btnPlay.onClick.RemoveAllListeners();
        btnPlay.onClick.AddListener(actionPlay);
    }

    public void InitButtonSettings(UnityAction actionSettings)
    {
        btnSettings.onClick.RemoveAllListeners();
        btnSettings.onClick.AddListener(actionSettings);
    }
}
