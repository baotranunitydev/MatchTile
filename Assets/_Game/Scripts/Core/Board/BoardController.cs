using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class BoardController : MonoBehaviour
{
    [SerializeField] private Transform tfmSpawnTile;
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
        SpawnTile().Forget();
    }

    private async UnitaskVoid SpawnTile()
    {
        if (modelSO == null) return;
        for (int i = 0; i < 30; i++)
        {
            var tile = Instantiate(modelSO.tilePrefab, tfmSpawnTile);
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
