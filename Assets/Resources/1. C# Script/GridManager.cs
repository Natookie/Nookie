using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject baseCellPrefab;
    [SerializeField] private GameObject oreCellPrefab;
    [SerializeField] private int gridSize = 8;
    [SerializeField] private float oreSpawnChance = .3f;

    private GridCell[,] grid;
    public GridCell[,] Grid => grid;

    public void GenerateGrid(){
        grid = new GridCell[gridSize, gridSize];

        for(int x = 0; x < gridSize; x++){
            for(int y = 0; y < gridSize; y++){
                GameObject cellPrefab;
                
                if(Random.value < oreSpawnChance && oreCellPrefab != null) cellPrefab = oreCellPrefab;
                else if(baseCellPrefab != null) cellPrefab = baseCellPrefab;
                else return;

                Vector3 position = new Vector3(x, y, 0);
                GameObject cellObj = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                
                GridCell cell = cellObj.GetComponent<GridCell>();
                cell.Initialize(x, y);
                
                grid[x, y] = cell;   
            }
        }
    }
}
