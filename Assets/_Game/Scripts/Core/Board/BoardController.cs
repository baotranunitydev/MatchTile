using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class BoardController : MonoBehaviour
{
    [SerializeField] private MergeBoard mergeBoard;
    [SerializeField] private Transform tfmSpawnTile;
    public static UnityAction<Tile> onSelectTile;
    private List<Tile> lstTile = new List<Tile>();
    private ModelSO modelSO;

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
        onSelectTile += OnSelectTile;
    }

    private void OnSelectTile(Tile tile)
    {
        mergeBoard.MoveTileToMergeBoardAndCheck(tile);
    }

    private void OnDestroy()
    {
        onSelectTile -= OnSelectTile;
    }

    public async UnitaskVoid SpawnTile(List<int> lstTileID)
    {
        if (modelSO == null) return;
        for (int i = 0; i < lstTileID.Count; i++)
        {
            var tileID = lstTileID[i];
            var tile = Instantiate(modelSO.tileSO.tilePrefab, tfmSpawnTile);
            var tileModelSO = modelSO.tileSO.GetTileModelSO(tileID);
            tile.InitTile(tileModelSO.id, tileModelSO.sprTileIcon);
            tile.name = $"Tile Id: {tileModelSO.id} - {i:D2}";
            var posInsideCicle = RandomPosInCircle(0.5f);
            tile.transform.position = new Vector3(posInsideCicle.x, 2f, posInsideCicle.y);
            lstTile.Add(tile);
            await UnitaskVoid.WaitForSeconds(0.2f);
        }
    }

    private Vector2 RandomPosInCircle(float radius)
    {
        var randomInCircle = Random.insideUnitCircle * radius;
        return randomInCircle;
    }
}
