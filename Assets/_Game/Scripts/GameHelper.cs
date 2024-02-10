using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelper : Singleton<GameHelper>
{
    [SerializeField] private ModelSO modelSO;
    [SerializeField] private InputHandle inputHandle;
    [SerializeField] private BoardController boardController;
    [SerializeField] private LevelController levelController;
    public ModelSO ModelSO { get => modelSO; }
    public BoardController BoardController { get => boardController; }
    public LevelController LevelController { get => levelController;}
    public InputHandle InputHandle { get => inputHandle;}

    private void OnValidate()
    {
        if (modelSO == null || boardController == null || levelController == null || inputHandle == null)
        {
            Debug.LogError("Game Helper: Null Ref", gameObject);
        }
    }
}