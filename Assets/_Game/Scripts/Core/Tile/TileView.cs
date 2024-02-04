using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprRendererTileIcon;
    [SerializeField] private Rigidbody rbTile;

    public Rigidbody RbTile { get => rbTile; }

    public void InitTileIcon(Sprite sprIcon) => sprRendererTileIcon.sprite = sprIcon;
}
