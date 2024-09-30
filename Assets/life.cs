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
        for (int i = 1; i < window.x; i++)
        {
            for (int j = 1; j < window.y; j++)
            {
                grid[i, j] = Instantiate(cell, new Vector3Int(i, j), transform.rotation, gameObject.transform);
            }
        }

        for (int i = 0; grid.Length * (fillpursent * 0.01) >= i; i++)
        {
            int x = Random.Range(3,window.x);
            int y = Random.Range(3,window.y);
            if (!grid[x,y].activeSelf)
            {
                grid[x, y].SetActive (true);
            }
            else
            {
                i -= 1;
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
                for (int i = 1; i < window.x; i++)
                {
                    for (int j = 1; j < window.y; j++)
                    {
                        sgrid[i, j] = grid[i, j].activeSelf;
                    }
                }
           }
        }
        count++;
    }

    bool inspecter(GameObject[,] x, bool[,] y)
    {
        for (int i = 1; i < window.x; i++)
        {
            for (int j = 1; j < window.y; j++)
            {
                if (x[i,j].activeSelf != y[i,j])
                {
                    return (false);
                }
            }
        }
        return (true);
    }


    void growth(int i, int j)
    {
        if (!grid[i, j].activeSelf)
        {
            if (check(i, j) == 3)
            {
                grid[i, j].SetActive(true);
                grid[i, j].tag = "growing";
            }
        }  
    }
    void growing(int i, int j)
    {
        if (!grid[i, j].activeSelf)
        {

        }
        else if (grid[i, j].tag == "growing")
        {
            grid[i, j].tag = "live";
        }
    }

    void death(int i, int j)
    {
        if (!grid[i, j].activeSelf)
        {

        }
        else if (grid[i,j].tag == "live")
        {
            if (check(i, j)-1 < 2 || check(i,j)-1 > 3)
            {
                grid[i, j].tag = "Dead";
            }
        }
    }
    void decai(int i, int j)
    {
        if(!grid[i, j].activeSelf)
        {

        }
        else if (grid[i,j].tag == "Dead")
        {
            grid[i, j].SetActive(false);
        }
    }
    int check(int i, int j)
    {
        int live = 0;
        for (int y = j-1; y < j + 2; y++)
        {
            for (int x = i-1; x < i + 2; x++)
            {
                if (grid[x, y] == null || !grid[x, y].activeSelf)
                {

                }
                else if (grid[x, y].tag == "live" || grid[x, y].tag == "Dead")
                {
                    live++;
                }
            }
        }
        return (live);
    }
}
