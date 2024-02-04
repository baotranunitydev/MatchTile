using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeBoard : MonoBehaviour
{
    [SerializeField] private SlotMerge[] arrSlotMerge;
    [SerializeField] private List<Tile> lstTileMergeBoard = new List<Tile>();
    private Tile currentTileCheck;

    public void MoveTileToMergeBoard(Tile tile)
    {
        if (lstTileMergeBoard.Count >= 7) return;
        //var index = lstTileMergeBoard.Count - 1 >= 1 ? lstTileMergeBoard.Count - 1 : 0;
        var currentSlotEmpty = arrSlotMerge[lstTileMergeBoard.Count];
        currentTileCheck = tile;
        lstTileMergeBoard.Add(tile);
        tile.MoveTileToMergeBoard(currentSlotEmpty.transform.position);
        CheckMerge();
    }

    private void CheckMerge()
    {
        List<Tile> lstTileMerge = new List<Tile>();
        for (int i = 0; i < lstTileMergeBoard.Count; i++)
        {
            var tileInMergeBoard = lstTileMergeBoard[i];
            if (tileInMergeBoard.TileID == currentTileCheck.TileID)
            {
                lstTileMerge.Add(tileInMergeBoard);
                if (lstTileMerge.Count >= 3) break;
            }
        }
        if (lstTileMerge.Count >= 3)
        {
            for (int i = 0; i < lstTileMerge.Count; i++)
            {
                var tile = lstTileMerge[i];
                lstTileMergeBoard.Remove(tile);
                tile.AnimationMerge();
            }
            RearrangePosTile();
        }
    }

    private void RearrangePosTile()
    {
        for (int i = 0; i < lstTileMergeBoard.Count; i++)
        {
            var tileInMergeBoard = lstTileMergeBoard[i];
            //Debug.Log("Move");
            tileInMergeBoard.MoveTileToMergeBoard(arrSlotMerge[i].transform.position);
            //tileInMergeBoard.transform.position = arrSlotMerge[i].transform.position;
        }
    }

    #region Old

    private int indexSlot = 0;


    //public void MoveTileToMergeBoard(Tile tile)
    //{
    //    if (indexSlot >= arrSlotMerge.Length) return;
    //    var currentSlotEmpty = arrSlotMerge[indexSlot];
    //    currentSlotEmpty.CurrentTile = tile;
    //    currentTileCheck = tile;
    //    tile.MoveTileToMergeBoard(currentSlotEmpty.transform.position);
    //    indexSlot++;
    //    CheckMerge();
    //}

    //private void CheckMerge()
    //{
    //    List<SlotMerge> slotMerges = new List<SlotMerge>();
    //    for (int i = 0; i < arrSlotMerge.Length; i++)
    //    {
    //        var slotMerge = arrSlotMerge[i];
    //        var tile = slotMerge.CurrentTile;
    //        if (tile == null) break;
    //        if (tile.TileID == currentTileCheck.TileID)
    //        {
    //            slotMerges.Add(slotMerge);
    //            if (slotMerges.Count >= 3)
    //            {
    //                break;
    //            }
    //        }
    //    }
    //    if (slotMerges.Count >= 3)
    //    {
    //        for (int i = 0; i < slotMerges.Count; i++)
    //        {
    //            var slotMerge = slotMerges[i];
    //            slotMerge.CurrentTile.gameObject.SetActive(false);
    //            slotMerge.CurrentTile = null;
    //        }
    //        RearrangePosTile();
    //    }
    //}

    //private void RearrangePosTile()
    //{
    //    List<SlotMerge> slotMerges = new List<SlotMerge>();
    //    for (int i = 0; i < arrSlotMerge.Length; i++)
    //    {
    //        var slotMerge = arrSlotMerge[i];
    //        if (slotMerge.CurrentTile != null)
    //        {
    //            slotMerges.Add(slotMerge);
    //        }
    //    }
    //    if (slotMerges.Count <= 0)
    //    {
    //        indexSlot = 0;
    //    }
    //    else
    //    {
    //        indexSlot = slotMerges.Count;
    //    }
    //    for (int i = 0; i < slotMerges.Count; i++)
    //    {
    //        var slotMerge = slotMerges[i];
    //        slotMerge.CurrentTile.MoveTileToMergeBoard(arrSlotMerge[i].transform.position);
    //    }
    //}
    #endregion
}
