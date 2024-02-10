using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuilder
{
    private Tile tile;

    public TileBuilder(Tile tile) => this.tile = tile;

    public TileBuilder SetTileID(int id)
    {
        tile.TileModel.SetTileId(id);
        return this;
    }

    public TileBuilder SetSpriteTile(Sprite sprTileIcon)
    {
        tile.TileView.InitTileIcon(sprTileIcon);
        return this;
    }

    public TileBuilder SetNameObject(string name)
    {
        tile.name = name;
        return this;
    }

    public Tile Build() => tile;
}


