using UnityEngine;

public class terrainTiles : MonoBehaviour
{
    [SerializeField] Vector2Int tilePosition;

   void Start() {
        GetComponentInParent<worldScorlling>().Add(gameObject, tilePosition);
    }
}
