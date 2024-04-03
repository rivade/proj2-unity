using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallBreakHandler : MonoBehaviour
{
    public void OnHit(Vector3Int position, int damage)
    {
        switch (damage)
        {
            case 50:
                GetComponent<Tilemap>().SetTile(position, null);
                break;

            case 100:
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        Vector3Int tempPosition = position + new Vector3Int(x, y, 0);
                        GetComponent<Tilemap>().SetTile(tempPosition, null);
                    }
                }
                break;
        }
    }
}
