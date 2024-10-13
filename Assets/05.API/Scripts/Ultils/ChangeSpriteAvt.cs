using DevNgao.API;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteAvt : MonoBehaviour
{
    [SerializeField] private Image imgAvt;
    //[SerializeField] private string url;

    private void Start()
    {
        Debug.Log($"New url: https://neoko-images.s3.ap-southeast-2.amazonaws.com/games/66599b8a1ad4d94bad99fd1b/a-4.png");
        //ImageLoader.LoadSprite("https://neoko-images.s3.ap-southeast-2.amazonaws.com/games/66599b8a1ad4d94bad99fd1b/a-4.png").ThenSet(imgAvt).Forget();
        
    }
    [Button]
    private async void LoadImage(string path)
    {
        var itemSprite = await DevNgaoAPI.LoadImageFromPersistentDataPath(path);
        if(itemSprite.Item2)
        {
            imgAvt.sprite = itemSprite.Item1;
        }    
        else
        {
            Debug.Log("Fail");
        }    
    }    
}
