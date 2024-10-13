using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Loading;
public class PopupWin : PopupBase
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnNextLevel;
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtStar;
    private AudioController audioController;
    private VibrateController vibrateController;
    private int star;
    private int score;
    public override void InitPopup()
    {
        audioController = AudioController.Instance;
        vibrateController = VibrateController.Instance;
        InitBtnNextLevel();
        InitBtnHome();
        base.InitPopup();
    }

    public void InitInfoPopupWin(int score, int star)
    {
        this.score = score;
        this.star = star;
        txtScore.text = $"+{score}";
        txtStar.text = $"{star}<sprite=0>";
    }
    public void AnimationText()
    {
        float timerText = 1f;
        audioController.PlaySound(SoundName.Coin);
        DOVirtual.Int(star, star + score, timerText, (value) =>
        {
            txtScore.text = $"+{star + score - value}";
            txtStar.text = $"{value}<sprite=0>"; 
        });      
    }

    private void InitBtnNextLevel()
    {
        btnNextLevel.onClick.RemoveAllListeners();
        btnNextLevel.onClick.AddListener(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            LoadingSceneController.Instance.ChangeScene(SceneType.GamePlay);
        });
    }

    private void InitBtnHome()
    {
        btnHome.onClick.RemoveAllListeners();
        btnHome.onClick.AddListener(() =>
        {
            vibrateController.Vibrate();
            audioController.PlaySound(SoundName.ClickBtn);
            LoadingSceneController.Instance.ChangeScene(SceneType.MainScene);
        });
    }
}
