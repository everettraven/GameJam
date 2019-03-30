using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : TacticalMovement
{
    public float turnCounter = 0;
    public float fartMeterFill = 0f;
    public float baseFart = 1.5f;
    public Animator anim;
    public GameOver gameOver;
    public float arrowUiMovement = 400 / 5;
    public Image image;
    float imagePos;


    Queue<GameObject> objs = new Queue<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

        Init();
        anim = GetComponent<Animator>();
        float imagePos = image.rectTransform.position.x;




    }

    // Update is called once per frame
    void Update()
    {
        ShitCheck();
        if(!turn)
        {
            anim.SetBool("IsWalking", false);
            return;
 
        }
        else
        {
            anim.SetBool("IsWalking", false);
            if (!actionTaken)
            {
                FindSelectableTiles();
                CheckMouse();
                CheckFart();
            }
            else
            {
                if (!isFarting)
                {
                    Move();
                    anim.SetBool("IsWalking", true);
                    if (gameObject.tag == "Player")
                    {

                        turnCounter = playerTurns;
                        fartMeterFill = turnCounter;

                    }

                    if ((playerTurns - turnsFarted) % 2 == 0)
                    {
                        GameObject delete = objs.Dequeue();
                        Destroy(delete);
                    }



                }
                else
                {
                    anim.SetBool("IsWalking", false);
                    Fart();
                    if (gameObject.tag == "Player")
                    {
                        turnCounter = 0;
                        playerTurns = 0;
                        turnsFarted = 0;
                        fartMeterFill = 0;


                    }

                }




            }
        }





    }




    void CheckMouse()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    if(t.selectable)
                    {
                        MoveToTile(t);
                        if(fartMeterFill < 5)
                        {
                                image.rectTransform.position = new Vector3(image.rectTransform.position.x + arrowUiMovement, image.rectTransform.position.y, 0);

                        }
                    }
                }
            }
        }else if(Input.GetKeyUp(KeyCode.Escape))
        {
            MainMenu menu = new MainMenu();
            menu.QuitGame();
        }
    }

    public void CheckFart()
    {



        if (Input.GetKeyUp(KeyCode.Space))
        {
            CreateFart();


            isFarting = true;
            actionTaken = true;

            image.rectTransform.position = new Vector3(image.rectTransform.position.x  - (turnCounter*arrowUiMovement), image.rectTransform.position.y, 0);
            

        }


    }

    public void CreateFart()
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load("FartObject"), transform.position, transform.rotation);
        obj.GetComponent<ParticleSystem>().Stop();
        obj.GetComponent<ParticleSystem>().Play();

        objs.Enqueue(obj);
    }

    public void ReleaseSafeFart()
    {
        image.rectTransform.position = new Vector3(image.rectTransform.position.x - (turnCounter * arrowUiMovement), image.rectTransform.position.y, 0);
        turnCounter = 0;
        playerTurns = 0;
        fartMeterFill = 0;

    }

    public void ShitCheck()
    {
        if (fartMeterFill > 5)
        {
            CreateFart();
            gameOver.gameOver = true;
            gameOver.won = false;
        }

    }
}
