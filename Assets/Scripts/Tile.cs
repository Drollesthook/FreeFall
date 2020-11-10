using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] int _tileLength = default;

    public int TileLength => _tileLength;
}
