using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.VisualScripting;
using System;

public class ComboController : MonoBehaviour
{
    [SerializeField] private ComboView comboView;
    private GameHelper gameHelper;
    private float timerCombo = 2f;
    private int combo;
    private CancellationTokenSource cancellationSource;
    public int Combo { get => combo; }

    private void Start()
    {
        gameHelper = GameHelper.Instance;
    }

    private void OnDestroy()
    {
        CancellUnitask();
    }

    public void StartCombo()
    {
        CancellUnitask();
        cancellationSource = new CancellationTokenSource();
        ComboCountDown(cancellationSource.Token).Forget();
    }

    private void CancellUnitask()
    {
        if (cancellationSource != null && !cancellationSource.IsCancellationRequested)
        {
            cancellationSource.Cancel();
            cancellationSource.Dispose();
        }
    }

    private async UniTaskVoid ComboCountDown(CancellationToken cancellationToken)
    {
        combo++;
        comboView.SetTextCombo(combo);
        float timer = timerCombo;
        while (timer >= 0)
        {
            await UniTask.Yield(cancellationToken);
            if (gameHelper.GamePlayController.StateGame != StateGame.PlayGame) continue;
            timer -= Time.deltaTime;
            float value = timer / timerCombo;
            comboView.SetFillAmountCombo(value);
        }
        combo = 0;
        comboView.SetTextCombo(combo);
    }

    //private async UnitaskVoid ComboCountDown(CancellationToken cancellationToken)
    //{
    //    combo++;
    //    comboView.SetTextCombo(combo);
    //    float timer = timerCombo;
    //    try
    //    {
    //        while (timer >= 0)
    //        {
    //            await UnitaskVoid.Yield(cancellationToken);
    //            cancellationSource.Token.ThrowIfCancellationRequested();
    //            if (gameHelper.GamePlayController.StateGame != StateGame.PlayGame) continue;
    //            timer -= Time.deltaTime;
    //            float value = timer / timerCombo;
    //            comboView.SetFillAmountCombo(value);
    //        }
    //        combo = 0;
    //    }
    //    catch (OperationCanceledException)
    //    {
    //        Debug.Log("Cancle");
    //    }
    //}
}
