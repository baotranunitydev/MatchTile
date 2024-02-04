using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo, 100f);
            if (hitInfo.collider == null) return;
            if (hitInfo.collider.TryGetComponent(out Tile tile))
            {
                BoardController.onSelectTile?.Invoke(tile);
                //Debug.Log($"GameObject Raycast: {hitInfo.collider.name}", hitInfo.collider.gameObject);
            }
        }
    }
}
