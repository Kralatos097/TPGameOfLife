using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayScript : MonoBehaviour
{
    [Header("Values")]
    public static int GridSize = 4;
    public float UpdateTimer;
    
    [Header("Drag'n Drop")]
    public Tilemap tilemap;
    public Tile BlackTile;
    public Tile WhiteTile;

    public Camera Camera;
    
    private float _updateTimer;
    
    public static int[,] Grid;
    private int[,] tempGrid;

    // Start is called before the first frame update
    void Start()
    {
        Camera.transform.position = new Vector3(GridSize/2, GridSize/2, -GridSize);

        _updateTimer = UpdateTimer;
        
        //Grid = new int[GridSize, GridSize];
        /*Grid = new int[,]{
            {0,1,0,0},
            {0,1,0,0},
            {0,1,0,0},
            {0,0,0,0}
        };*/
        
        tempGrid = new int[GridSize, GridSize];
        
        PrintGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if(_updateTimer <= 0)
        {
            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    StateRules(x,y);
                }
            }

            Grid = (int[,])tempGrid/*.Clone()*/;
            tempGrid = new int[GridSize,GridSize];
            PrintGrid();
            
            _updateTimer = UpdateTimer;
        }
        _updateTimer -= Time.deltaTime;
    }

    private int GetNbVoisin(int x, int y)
    {
        int nbVoisin = 0;


        for (int i = -1; i <= 1; i++)
        {
            if (x + i < GridSize && x + i >= 0)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        if (y + j < GridSize && y + j >= 0)
                        {
                            if (Grid[x + i, y + j] == 1)
                            {
                                nbVoisin++;
                            }
                        }
                    }
                }
            }
        }
        return nbVoisin;
    }

    private void StateRules(int x,int y)
    {
        int nbVoisin = GetNbVoisin(x, y);
        int state = Grid[x, y];
        
        if (nbVoisin == 3)
        {
            tempGrid[x, y] = 1;
        }
        else if (nbVoisin == 2)
        {
            tempGrid[x, y] = state;
        }
        else
        {
            tempGrid[x, y] = 0;
        }
    }

    private void PrintGrid()
    {
        for (int x = 0; x < GridSize; x++)
        {
            for (int y = 0; y < GridSize; y++)
            {
                PrintTile(x,y);
            }
        }
    }

    private void PrintTile(int x, int y)
    {
        Vector3Int pos = new Vector3Int(x, y, 0);

        switch (Grid[x,y])
        {
            case 1 :
                tilemap.SetTile(pos, BlackTile);
                break;
                
            case 0 :
                tilemap.SetTile(pos, WhiteTile);
                break;
        }
    }
}
