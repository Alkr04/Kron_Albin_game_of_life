using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class life : MonoBehaviour
{
    public Vector3Int window = new Vector3Int(20, 12);
    GameObject[,] grid = new GameObject[200, 200];
    //public GameObject[] test = new GameObject[2];
    //public GameObject t;
    public GameObject cell;
    public int cellm;
    public int frames = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        window = new Vector3Int(grid.GetLength(0), grid.GetLength(1));
        Application.targetFrameRate = frames;
        //test[0] = t;
        //grid[1, 1] = 69;
        //grid[1, 0] = 96;
        //grid[cordinat.x,cordinat.y] = Instantiate(cell, cordinat, transform.rotation);
        //cellm = Random.Range(600,700);
        for (int i = 0; cellm >= i; i++)
        {
            int x = Random.Range(3,window.x -3);
            int y = Random.Range(3,window.y -3);
            if(grid[x,y] == null)
            {
                grid[x, y] = Instantiate(cell, new Vector3Int(x,y), transform.rotation);
            }
            else
            {
                Debug.Log(grid[x,y]);
                i -= 1;
            }
        }
        /*grid[3, 3] = Instantiate(cell, new Vector3Int(3, 3), transform.rotation);
        grid[3, 4] = Instantiate(cell, new Vector3Int(3, 4), transform.rotation);
        grid[3, 5] = Instantiate(cell, new Vector3Int(3, 5), transform.rotation);*/
    }

    // Update is called once per frame
    void Update()
    {
        //print(grid.GetLength(0));
        //print(test[1]);
        
        
        
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        for (int b = 0; b < 4; b++)
        {
            for (int i = 1; i < window.x - 3; i++)
            {
                for (int j = 1; j < window.y - 3; j++)
                {
                    switch (b)
                    {
                        case (0):
                            growth(i,j);
                            break;
                        case (1):
                            death(i,j);
                            break;
                        case (2):
                            decai(i,j);
                            break;
                        case (3):
                            growing(i,j);
                            break;
                    }
                }
            }
        }
    }

    

    void growth(int i, int j)
    {
        if (grid[i,j] == null)
        {
            if (check(i, j) + 1 == 4)
            {
                grid[i, j] = Instantiate(cell, new Vector3Int(i,j), transform.rotation);
            }
        }  
    }
    void growing(int i, int j)
    {
        if (grid[i, j] == null)
        {

        }
        else if (grid[i, j].tag == "growing")
        {
            grid[i, j].tag = "live";
        }
    }

    void death(int i, int j)
    {
        if (grid[i,j] == null)
        {

        }
        else if (grid[i,j].tag == "live" || grid[i,j].tag == "Dead")
        {
            Debug.Log(grid[i,j].tag);
            if (check(i, j) < 3 || check(i,j) > 4)
            {
                grid[i, j].tag = "Dead";
                Debug.Log(i + " " + j);
            }
        }
    }
    void decai(int i, int j)
    {
        if(grid[i,j] == null)
        {

        }
        else if (grid[i,j].tag == "Dead")
        {
            Destroy(grid[i, j]);
            grid[i, j] = null;
        }
    }
    int check(int i, int j)
    {
        int live = 0;
        for (int y = j-1; y < j + 2; y++)
        {
            for (int x = i-1; x < i + 2; x++)
            {
                //Debug.Log(x + " " + y);
                if (grid[x, y] == null)
                {

                }
                else
                {
                    if (grid[x,y].tag == "live" || grid[x,y].tag == "Dead")
                    {
                        live++;
                    }
                }
            }
        }
        return (live);
    }
}
