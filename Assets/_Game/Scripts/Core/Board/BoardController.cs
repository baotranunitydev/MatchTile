using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class BoardController : MonoBehaviour
{
    private MergeBoard mergeBoard;
    [SerializeField] private Transform tfmSpawnTile;
    public static UnityAction<Tile> onSelectTile;
    private List<Tile> lstTile = new List<Tile>();
    private ModelSO modelSO;

    public List<Tile> LstTile { get => lstTile;}

    private void OnValidate()
    {
        if (tfmSpawnTile == null)
        {
            Debug.LogError("Board: Null ref", gameObject);
        }
    }

    public void InitBoard()
    {
        modelSO = GameHelper.Instance.ModelSO;
        mergeBoard = GameHelper.Instance.MergeBoard;
        onSelectTile += OnSelectTile;
    }

    private void OnSelectTile(Tile tile)
    {
        mergeBoard.MoveTileToMergeBoardAndCheck(tile);
    }

    public void UseHint(Tile tile)
    {
        mergeBoard.MoveTileToMergeBoardAndCheckHint(tile);
    }

    private void OnDestroy()
    {
        onSelectTile -= OnSelectTile;
    }

    public async UniTask SpawnTile(List<int> lstTileID)
    {
        if (modelSO == null) return;
        for (int i = 0; i < lstTileID.Count; i++)
        {
            var tileID = lstTileID[i];
            //tileID = 0;
            var tile = Instantiate(modelSO.tileSO.tilePrefab, tfmSpawnTile);
            var tileModelSO = modelSO.tileSO.GetTileModelSO(tileID);
            var tileBuilder = new TileBuilder(tile);
            tileBuilder.SetTileID(tileModelSO.id)
                        .SetSpriteTile(tileModelSO.sprTileIcon)
                        .SetNameObject($"Tile Id: {tileModelSO.id} - {i:D2}")
                        .Build();
            var posInsideCicle = RandomPosInCircle(0.15f);
            var pos = tfmSpawnTile.transform.position;
            tile.transform.position = pos + new Vector3(posInsideCicle.x, 2f, posInsideCicle.y);
            lstTile.Add(tile);
            await UniTask.WaitForSeconds(0.2f);
        }
    }

    private Vector2 RandomPosInCircle(float radius)
    {
        var randomInCircle = Random.insideUnitCircle * radius;
        return randomInCircle;
    }


}
