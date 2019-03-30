using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : TacticalMovement
{
    public float turnCounter = 0;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!turn)
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
                RandomMove();
            }
            else
            {
                Move();
                anim.SetBool("IsWalking", true);
            }
        }

    }
    void RandomMove()
    {
        Tile t = getRandTile();
        if (t.selectable)
        {
            MoveToTile(t);
        }
    }
}