using UnityEngine;

[CreateAssetMenu(fileName = "UserSettingsAsset",menuName = "Data/UserSettingsAsset")]
public class UserSettingsAsset : LocalDataAsset<UserSettings>
{
    public void SetMusic(bool isMusic)
    {
        data.SetMusic(isMusic);
        SaveData();
    }

    public void SetSound(bool isSound)
    {
        data.SetSound(isSound);
        SaveData();
    }

    public void SetVibrate(bool isVibrate)
    {
        data.SetVibrate(isVibrate);
        SaveData();
    }
}
