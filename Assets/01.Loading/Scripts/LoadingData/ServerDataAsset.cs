using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

public class ServerDataAsset<T> : LoadingDataAsset<T> where T : struct
{
    [Button]
    public override async UniTask<LoadingType> LoadingData()
    {
        isCheckFail = true;
        type = LoadingType.None;
        await GetData();
        return await base.LoadingData();
    }

    protected async virtual UniTask GetData()
    {

        await UniTask.CompletedTask;
    }
}