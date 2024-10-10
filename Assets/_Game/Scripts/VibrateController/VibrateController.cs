using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateController : Singleton<VibrateController>
{
    private UserSettings userSettings;
    private void Start()
    {
        userSettings = DBController.Instance.USER_SETTINGS;
    }

    public void Vibrate()
    {
        //if (SystemInfo.supportsVibration && userSettings.isVibrate)
        //{
        //    Handheld.Vibrate();
        //}
    }

    public void SetVibrate(bool isVibrate)
    {
        userSettings.SetVibrate(isVibrate);
    }
}
