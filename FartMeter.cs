using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartMeter : TacticalMovement
{
    public int turnCounter = 0;
    public float fartMeterFill = 0f;
    public float baseFart = 2f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turn)
        {
            turnCounter++;
            fartMeterFill += turnCounter * baseFart;
            Debug.Log(string.Format("(Turn)FartMeter : {0}", fartMeterFill));
        }

        if(fartMeterFill >= 100)
        {
            //Game over
            Debug.Log("You shid your pants");        
        }
        else
        {
            if(isFarting)
            {
                turnCounter--;
                fartMeterFill -= 5;
                Debug.Log(string.Format("(Farted)FartMeter : {0}", fartMeterFill));

            }
        }
    }
}
