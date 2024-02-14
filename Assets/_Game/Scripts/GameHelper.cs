using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelper : Singleton<GameHelper>
{
    [SerializeField] private ModelSO modelSO;
    [SerializeField] private InputHandle inputHandle;
    [SerializeField] private BoardController boardController;
    [SerializeField] private MergeBoard mergeBoard;
    [SerializeField] private LevelController levelController;
    [SerializeField] private PopupController popupController;
    [SerializeField] private GamePlayController gamePlayController;
    [SerializeField] private BoosterController featuresController;

    public ModelSO ModelSO { get => modelSO; }
    public BoardController BoardController { get => boardController; }
    public LevelController LevelController { get => levelController;}
    public InputHandle InputHandle { get => inputHandle;}
    public MergeBoard MergeBoard { get => mergeBoard;}
    public GamePlayController GamePlayController { get => gamePlayController;}
    public BoosterController BoosterController { get => featuresController;}
    public PopupController PopupController { get => popupController; }

    private void OnValidate()
    {
        if (modelSO == null || boardController == null || levelController == null || inputHandle == null)
        {
            Debug.LogError("Game Helper: Null Ref", gameObject);
        }
    }
}
