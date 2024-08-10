using System;
using System.Collections;
using UnityEngine;

public class worldScorlling : MonoBehaviour
{

    [SerializeField] Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0, 0);
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    [SerializeField] float tileSize = 20f;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    [SerializeField] int fieldOfVisionHeight = 5;  // เพิ่มขนาดฟิลด์การมองเห็น
    [SerializeField] int fieldOfVisionWidth = 5;   // เพิ่มขนาดฟิลด์การมองเห็น
    

   

    private void Awake() {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Start() {
        StartCoroutine(UpdateTilesScreenCoroutine());
    }

    private void Update() {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;

        if (currentTilePosition != playerTilePosition) {
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatePositionOnAxis(playerTilePosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxis(playerTilePosition.y, false);
        }
    }

    private IEnumerator UpdateTilesScreenCoroutine() {
        while (true) {
            UpdateTilesScreen();
            yield return null; // รอจนกว่าจะถึงเฟรมถัดไปเพื่อไม่ให้เกิดการกระตุก
        }
    }

    private void UpdateTilesScreen() {
        for (int pov_x = -(fieldOfVisionWidth / 2); pov_x <= fieldOfVisionWidth / 2; pov_x++) {
            for (int pov_y = -(fieldOfVisionHeight / 2); pov_y <= fieldOfVisionHeight / 2; pov_y++) {
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + pov_y, false);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];


                if (tile != null) {
                    tile.transform.position = CalculateTilePosition(
                        playerTilePosition.x + pov_x,
                        playerTilePosition.y + pov_y);
                }
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y) {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal) {
        int maxCount = horizontal ? terrainTileHorizontalCount : terrainTileVerticalCount;
        int position = ((int)currentValue % maxCount + maxCount) % maxCount;
        return position;
    }

   

    public void Add(GameObject tileGameObject, Vector2Int tilePosition) {
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }

}


