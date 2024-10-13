using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class LoadingDataAssetBase : ScriptableObject
{
    public abstract UniTask<LoadingType> LoadingData();
}

public class LoadingDataAsset<T> : LoadingDataAssetBase where T : struct
{
    [SerializeField] protected T data;
    [SerializeField] protected bool isCheckFail;
    protected LoadingType type;
    public T Data { get => data; }
    public override async UniTask<LoadingType> LoadingData()
    {
        switch (type)
        {
            case LoadingType.Fail:
                return await LoadingFail();
            case LoadingType.Success:
                return await LoadingSuccess();
            default:
                return await LoadingFail();
        }
    }

    protected virtual void ResetData()
    {
        type = LoadingType.None;
    }

    protected virtual UniTask<LoadingType> LoadingFail()
    {
        if (isCheckFail)
        {
            return UniTask.FromResult(LoadingType.Fail);
        }
        return UniTask.FromResult(LoadingType.Success);
    }

    protected virtual UniTask<LoadingType> LoadingSuccess()
    {
        return UniTask.FromResult(LoadingType.Success);
    }
}

public enum LoadingType
{
    None = 0,
    Fail = 1,
    Success = 2
}