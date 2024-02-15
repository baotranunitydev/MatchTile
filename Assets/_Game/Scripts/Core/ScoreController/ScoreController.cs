using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private ScoreView scoreView;
    private int score;

    public int Score { get => score;}

    public void InitScore()
    {
        scoreView.InitScore(score);
    }

    public void AddScore(int scorePlus)
    {
        score += scorePlus;
        scoreView.AddScore(scorePlus);
        scoreView.AnimationScore(score - scorePlus, score);
    }
}
