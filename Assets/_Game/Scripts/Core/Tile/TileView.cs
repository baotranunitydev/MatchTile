using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprRendererTileIcon;
    public void InitTileIcon(Sprite sprIcon) => sprRendererTileIcon.sprite = sprIcon;
}
