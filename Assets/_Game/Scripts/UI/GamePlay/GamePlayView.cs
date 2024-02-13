using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePlayView : MonoBehaviour
{
    [SerializeField] private Button btnPause;

    public void InitButtonBack(UnityAction actionSettings)
    {
        btnPause.onClick.RemoveAllListeners();
        btnPause.onClick.AddListener(actionSettings);
    }
}
