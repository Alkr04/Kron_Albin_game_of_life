using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class life : MonoBehaviour
{
    public Vector3Int window = new Vector3Int(20, 12);
    GameObject[,] grid = new GameObject[500, 500];
    bool[,] sgrid = new bool[500, 500];
    public GameObject cell;
    public float fillpursent;
    public int frames = 1;
    int children;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        window = new Vector3Int(grid.GetLength(0)-3, grid.GetLength(1)-3);
        Application.targetFrameRate = frames;
        for (int x = 1; x < window.x; x++)
        {
            for (int y = 1; y < window.y; y++)
            {
                grid[x, y] = Instantiate(cell, new Vector3Int(x, y), transform.rotation, gameObject.transform);
            }
        }

        for (int maxCells = 0; grid.Length * (fillpursent * 0.01) >= maxCells; maxCells++)
        {
            int x = Random.Range(3,window.x);
            int y = Random.Range(3,window.y);
            if (!grid[x,y].activeSelf)
            {
                grid[x, y].SetActive (true);
            }
            else
            {
                maxCells -= 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        for (int stage = 0; stage < 4; stage++)
        {
            for (int x = 1; x < window.x; x++)
            {
                for (int y = 1; y < window.y; y++)
                {
                    switch (stage)
                    {
                        case (0):
                            growth(x, y);
                            break;
                        case (1):
                            death(x, y);
                            break;
                        case (2):
                            decai(x, y);
                            break;
                        case (3):
                            growing(x, y);
                            break;
                    }
                }
            }
        }
        stable();
    }

    private void stable()
    {
        if (count % 16 == 0)
        {
           if (inspecter(grid, sgrid))
           {
                SceneManager.LoadScene(0);
           }
           else
           {
                for (int x = 1; x < window.x; x++)
                {
                    for (int y = 1; y < window.y; y++)
                    {
                        sgrid[x, y] = grid[x, y].activeSelf;
                    }
                }
           }
        }
        count++;
    }

    bool inspecter(GameObject[,] gameObjectGrid, bool[,] activeObjectGrid)
    {
        for (int x = 1; x < window.x; x++)
        {
            for (int y = 1; y < window.y; y++)
            {
                if (gameObjectGrid[x,y].activeSelf != activeObjectGrid[x,y])
                {
                    return (false);
                }
            }
        }
        return (true);
    }


    void growth(int x, int y)
    {
        if (!grid[x, y].activeSelf)
        {
            if (check(x, y) == 3)
            {
                grid[x, y].SetActive(true);
                grid[x, y].tag = "growing";
            }
        }  
    }
    void growing(int x, int y)
    {
        if (!grid[x, y].activeSelf)
        {

        }
        else if (grid[x, y].tag == "growing")
        {
            grid[x, y].tag = "live";
        }
    }

    void death(int x, int y)
    {
        if (!grid[x, y].activeSelf)
        {

        }
        else if (grid[x,y].tag == "live")
        {
            if (check(x, y)-1 < 2 || check(x,y)-1 > 3)
            {
                grid[x, y].tag = "Dead";
            }
        }
    }
    void decai(int x, int y)
    {
        if(!grid[x, y].activeSelf)
        {

        }
        else if (grid[x,y].tag == "Dead")
        {
            grid[x, y].SetActive(false);
        }
    }
    int check(int locationX, int locationy)
    {
        int living = 0;
        for (int y = locationy-1; y < locationy + 2; y++)
        {
            for (int x = locationX-1; x < locationX + 2; x++)
            {
                if (grid[x, y] == null || !grid[x, y].activeSelf)
                {

                }
                else if (grid[x, y].tag == "live" || grid[x, y].tag == "Dead")
                {
                    living++;
                }
            }
        }
        return (living);
    }
}
