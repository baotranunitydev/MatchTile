using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePlayView : MonoBehaviour
{
    [SerializeField] private Button btnSettings;

    public void InitButtonBack(UnityAction actionSettings)
    {
        btnSettings.onClick.RemoveAllListeners();
        btnSettings.onClick.AddListener(actionSettings);
    }
}
