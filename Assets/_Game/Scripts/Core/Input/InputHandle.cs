using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    [SerializeField] private LayerMask tileLayer;
    public void UpdateInputHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo, 100f, tileLayer);
            if (hitInfo.collider == null) return;
            if (hitInfo.collider.TryGetComponent(out Tile tile))
            {
                BoardController.onSelectTile?.Invoke(tile);
                //Debug.Log($"GameObject Raycast: {hitInfo.collider.name}", hitInfo.collider.gameObject);
            }
        }
    }
}
