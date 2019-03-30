using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartRadius : MonoBehaviour
{
    public float radius;
    // public GameObject player;
    public GameOver gameOver;


    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
       
    }

    // Update is called once per frame
    void Update()
    {
       // float fillMeter = player.GetComponent<PlayerMovement>().fartMeterFill;
        

        FartZone();
    }


    public void FartZone()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        bool playerInRadius = false;

       

        foreach(Collider col in colliders)
        {
            if(col.tag == "Player")
            {
                playerInRadius = true;
            }

            if(col.tag == "Fart" && playerInRadius == true)
            {
                gameOver.gameOver = true;
                gameOver.won = false;
                gameOver.detected = true;

            }

        }

        

    }

}
