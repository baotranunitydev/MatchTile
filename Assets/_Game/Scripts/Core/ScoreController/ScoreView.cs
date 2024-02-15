using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtScorePlus;
    public void InitScore(int score)
    {
        txtScore.text = $"{score}";
    }

    public void AddScore(int score, float timerMove = 0.25f)
    {
        txtScorePlus.text = $"+{score}";
        txtScorePlus.gameObject.SetActive(true);
        txtScorePlus.DOKill();
        txtScorePlus.rectTransform.DOLocalMoveY(50f, timerMove).OnComplete(() =>
        {
            txtScorePlus.gameObject.SetActive(false);
            txtScorePlus.rectTransform.anchoredPosition = Vector3.zero;
        });
    }

    public void AnimationScore(int form, int to, float timer = 0.5f)
    {
        DOVirtual.Int(form, to, timer, (value) =>
        {
            txtScore.text = $"{value}";
        });
    }
}
