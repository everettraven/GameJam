using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public float radius = 0.0f;
    public bool firstTime;
    public GameObject gameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        firstTime = false;
        gameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SafeZoneRadius();

    }

    public void SafeZoneRadius()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius );

        foreach(Collider col in colliders)
        {
            if (col.tag == "Player")
            {
                if(!firstTime)
                {
                    PlayerMovement playerMove = gameObject.GetComponent<PlayerMovement>();
                    playerMove.ReleaseSafeFart();
                    firstTime = true;
                    
                }
            }
        }

    }
}
