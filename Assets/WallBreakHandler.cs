using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallBreakHandler : MonoBehaviour
{
    public void OnHit(Vector3Int position)
    {
        GetComponent<Tilemap>().SetTile(position, null);
    }
}
