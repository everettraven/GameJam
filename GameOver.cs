using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public bool gameOver;
    public bool won;
    public bool detected;
    public Image image;
    public Canvas canvas;
    public Sprite winSprite;
    public Sprite lose1Sprite;
    public Sprite lose2Sprite;



    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        won = false;
        detected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver == true)
        {
            if(won)
            {
                image.sprite = winSprite;
            }
            else
            {
                if(detected)
                {
                    image.sprite = lose1Sprite;
                }
                else
                {
                    image.sprite = lose2Sprite;
                }


            }
            canvas.enabled = true;

            Time.timeScale = 0.1f;



        }
        else
        {
            Time.timeScale = 1;
            

            canvas.enabled = false;

        }
    }
}
