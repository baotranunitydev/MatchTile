using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelper : Singleton<GameHelper>
{
    [SerializeField] private ModelSO modelSO;
    [SerializeField] private BoardController boardController;
    public ModelSO ModelSO { get => modelSO; }
    public BoardController BoardController { get => boardController; }

    private void OnValidate()
    {
        if (modelSO == null || boardController == null)
        {
            Debug.LogError("Game Helper: Null Ref", gameObject);
        }
    }
}
