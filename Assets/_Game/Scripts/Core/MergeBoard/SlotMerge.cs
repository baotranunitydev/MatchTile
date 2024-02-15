using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotMerge
{
    [SerializeField] private Transform tfmPos;
    [SerializeField] private Tile currentTile;
    public Tile CurrentTile { get => currentTile; set => currentTile = value; }
    public Transform TfmPos { get => tfmPos; }
}
