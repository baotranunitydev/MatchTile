using System;
using Sirenix.OdinInspector;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

/// <summary>
/// Setup in Unity Cloud
/// </summary>
public enum TypeEnviroment
{
    Production,
    Dev
}

public class UnityGameServiceController : MonoBehaviour
{
    [SerializeField] private TypeEnviroment typeEnviroment;

    private void Start()
    {
        Initialize(OnSuccess, OnError);
    }

    void Initialize(Action onSuccess, Action<string> onError)
    {
        try
        {
            var options = new InitializationOptions().SetEnvironmentName(typeEnviroment.ToString());

            UnityServices.InitializeAsync(options).ContinueWith(task => onSuccess());
        }
        catch (Exception exception)
        {
            onError(exception.Message);
        }
    }

    void OnSuccess()
    {
        var text = "Congratulations!\nUnity Gaming Services has been successfully initialized.";
        Debug.Log(text);
    }

    void OnError(string message)
    {
        var text = $"Unity Gaming Services failed to initialize with error: {message}.";
        Debug.LogError(text);
    }
}
