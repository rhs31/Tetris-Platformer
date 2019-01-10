//lines 30-105 were written using a tutorial
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    public static int gridWidth = 60;
    public static int gridHeight = 60;


    int wins;

    public static Transform[,] grid = new Transform[120, 120];
    // Use this for initialization
	void Start () {

        GameObject dontdestroyobject = GameObject.Find("BGMSource");
        dontDestroyOnLoad dontdestroyscript = dontdestroyobject.GetComponent<dontDestroyOnLoad>();
        wins = dontdestroyscript.wins;
        SpawnNextTetromino();
        SpawnDoor();
        SpawnBuzzsaws();
	}
	
	// Update is called once per frame
	void Update () {

    }
    public void UpdateGrid(tetroscript tetromino)
    {
        for (int y = 0; y < 120; ++y)
        {
            for (int x = 0; x < 120; ++x)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }
        foreach (Transform mino in tetromino.transform)
        {
            Vector2 pos = Round(mino.position);
            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }
    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        if (pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }
    public bool isInGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < 120 && (int)pos.y >= 0);
    }
    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }
    public void SpawnNextTetromino()
    {
        GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetomino(), typeof(GameObject)), new Vector2(60.0f, 60.0f), Quaternion.identity);
    }
    string GetRandomTetomino()
    {
        int randomTetromino = Random.Range(1, 7);
        string randomTetrominoName = "Prefabs/Tetromino_T";
        switch (randomTetromino)
        {
            case 1:
                randomTetrominoName = "Prefabs/Tetromino_T";
                break;
            case 2:
                randomTetrominoName = "Prefabs/Tetromino_Long";
                break;
            case 3:
                randomTetrominoName = "Prefabs/Tetromino_Square";
                break;
            case 4:
                randomTetrominoName = "Prefabs/Tetromino_J";
                break;
            case 5:
                randomTetrominoName = "Prefabs/Tetromino_L";
                break;
            case 6:
                randomTetrominoName = "Prefabs/Tetromino_S";
                break;
            case 7:
                randomTetrominoName = "Prefabs/Tetromino_Z";
                break;
        }
        return randomTetrominoName;

     }
    public void SpawnDoor()
    {
        int randomX = Random.Range(15, 120);
        int randomY = Random.Range(30, 55);
        GameObject Door = (GameObject)Instantiate(Resources.Load("Prefabs/Door", typeof(GameObject)), new Vector2(randomX, randomY), Quaternion.identity);
    }
    public void SpawnBuzzsaws()
    {
        int buzzsawnum = 10 + 2 * (wins);
        for (int i = 0; i < buzzsawnum; i++)
        {
            int randomX = Random.Range(0, 120);
            int randomY = Random.Range(10, 60);
            GameObject Buzzsaw = (GameObject)Instantiate(Resources.Load("Prefabs/BuzzSaw", typeof(GameObject)), new Vector2(randomX, randomY), Quaternion.identity);
        }
    }
}

