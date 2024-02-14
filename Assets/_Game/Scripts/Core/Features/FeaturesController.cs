using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FeaturesController : MonoBehaviour
{
    [SerializeField] private HintController hintController;
    private GameHelper gameHelper;
    private BoardController boardController;
    private MergeBoard mergeBoard;
    public static UnityAction<bool> onCheckCanUseHint;

    private void Start()
    {
        gameHelper = GameHelper.Instance;
        boardController = gameHelper.BoardController;
        mergeBoard = gameHelper.MergeBoard;
    }
    public void UseHint()
    {
        hintController.UseHint(mergeBoard, boardController);
    }

    public void CheckIsCanUseHint()
    {
        onCheckCanUseHint?.Invoke(hintController.IsCanUseHint(mergeBoard));
    }
}
