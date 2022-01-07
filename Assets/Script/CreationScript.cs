using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreationScript : MonoBehaviour
{
    //[Range(4, 20)]
    public int GridSize;

    public GridLayoutGroup UIGrid;

    public GameObject ButtonPrefab;

    public Slider Slider;
    
    // Start is called before the first frame update
    public void CreateGrid()
    {
        UIGrid.cellSize = new Vector2(750/GridSize, 750/GridSize);
        UIGrid.spacing = new Vector2(50 / GridSize, 50 / GridSize);
        UIGrid.constraintCount = GridSize;

        for (int i = 0; i < Mathf.Pow(GridSize,2); i++)
        {
            Instantiate(ButtonPrefab, UIGrid.transform);
        }
    }

    public void StartGame()
    {
        PlayScript.GridSize = GridSize;
        PlayScript.Grid = new int[GridSize, GridSize];

        int childInd = 0;
        for (int x = 0; x < GridSize; x++)
        {
            for (int y = 0; y < GridSize; y++)
            {
                int state = UIGrid.transform.GetChild(childInd).GetComponent<ButtonScript>().state;
                PlayScript.Grid[y, x] = state;

                childInd++;
            }
        }
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void AddGridSize()
    {
        GridSize = (int)Slider.value;
        CreateGrid();
    }
}
