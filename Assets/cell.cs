using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cell : MonoBehaviour
{
    SpriteRenderer sprite;
    int age = 0;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprite.enabled == true)
        {
            switch (age)
            {
                case (5):
                    sprite.color = Color.yellow;
                    break;
                case (10):
                    sprite.color = Color.red;
                    break;
                case (15):
                    sprite.color = Color.black;
                    break;
            }
            age++;

        }
        else
        {
            age = 0;
            sprite.color = Color.green;
        }
    }
}
