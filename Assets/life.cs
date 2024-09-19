using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class life : MonoBehaviour
{
    public Vector3Int window = new Vector3Int(20, 12);
    GameObject[,] grid = new GameObject[500, 500];
    bool[,] sgrid = new bool[500, 500];
    //public GameObject[] test = new GameObject[2];
    //public GameObject t;
    public GameObject cell;
    public int cellm;
    public int frames = 1;
    int children;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        window = new Vector3Int(grid.GetLength(0)-3, grid.GetLength(1)-3);
        Application.targetFrameRate = frames;
        //test[0] = t;
        //grid[1, 1] = 69;
        //grid[1, 0] = 96;
        //grid[cordinat.x,cordinat.y] = Instantiate(cell, cordinat, transform.rotation);
        //cellm = Random.Range(600,700);
        for (int i = 1; i < window.x; i++)
        {
            for (int j = 1; j < window.y; j++)
            {
                grid[i, j] = Instantiate(cell, new Vector3Int(i, j), transform.rotation, gameObject.transform);
            }
        }

        for (int i = 0; cellm >= i; i++)
        {
            int x = Random.Range(3,window.x);
            int y = Random.Range(3,window.y);
            if (!grid[x,y].activeSelf)
            {
                grid[x, y].SetActive (true);
            }
            else
            {
                //Debug.Log(grid[x,y]);
                //i -= 1;
            }
        }
        //grid[3, 3].GetComponent<SpriteRenderer>().enabled = true;
        /*grid[2, 4].SetActive(true);
        grid[4, 2].SetActive(true);
        grid[3, 3].SetActive(true);*/
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
            for (int i = 1; i < window.x; i++)
            {
                for (int j = 1; j < window.y; j++)
                {
                    switch (b)
                    {
                        case (0):
                            growth(i, j);
                            break;
                        case (1):
                            death(i, j);
                            break;
                        case (2):
                            decai(i, j);
                            break;
                        case (3):
                            growing(i, j);
                            break;
                    }
                }
            }
        }
        //Debug.Log(Time.deltaTime);
        stable();
    }

    private void stable()
    {
        if (count % 8 == 0)
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

                //Debug.Log(x[i,j].activeSelf + " " + y[i,j].activeSelf);
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
                //Debug.Log(i + " " + j);
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
            //Debug.Log(grid[i,j].tag);
            if (check(i, j)-1 < 2 || check(i,j)-1 > 3)
            {
                grid[i, j].tag = "Dead";
                //Debug.Log(i + " " + j);
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
                //Debug.Log(x + " " + y);
                if (grid[x, y] == null || !grid[x, y].activeSelf)
                {

                }
                else if (grid[x, y].tag == "live" || grid[x, y].tag == "Dead")
                {
                    //Debug.Log(grid[x,y] + " " + x + " " + y);
                    live++;
                    
                }
            }
        }
        //Debug.Log(live);
        return (live);
    }
}
