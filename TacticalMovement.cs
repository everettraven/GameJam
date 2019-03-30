using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

public class TacticalMovement : MonoBehaviour
{
    //Create a list to store the selectable tiles
    List<Tile> selectableTiles = new List<Tile>();

    //Store an array of tiles
    GameObject[] tiles;

    //Create a stack of tiles that will be used to create paths
    Stack<Tile> path = new Stack<Tile>();

    //Get the tile the player is currently on
    Tile currentTile;

    //logic bools
    public bool isMoving = false;
    public bool turn = false;
    public bool actionTaken = false;
    public bool isFarting = false;
    // Movement values
    public int move = 3;
    public float moveSpeed = 2f;

    //Player Turn value
    public int playerTurns = 0;


    public int turnsFarted = 0;

    //Movement Vectors
    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    //Initialization function
    public void Init()
    {
        //Gets all the tiles gameobjects
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        TurnManager.AddUnit(this);

    }

    //Function to get the current tile
    public void GetCurrentTile()
    {
        //Get the tile the current game object is on
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;

    }

    //Function to get the target tile
    public Tile GetTargetTile(GameObject target)
    {
        
        RaycastHit hit;
        Tile tile = null;

        Debug.DrawRay(target.transform.position, -Vector3.up, Color.blue);

        //Use a raycast to make sure you are hitting a tile object
        if(Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
         
            //get the tile that the ray hit
            tile = hit.collider.GetComponent<Tile>();

        }

        return tile;
    }

    public Tile getRandTile() {
        //Tile t = selectableTiles[r.Next(selectableTiles.Count)];
        Tile t = selectableTiles[Random.Range(0, selectableTiles.Count)];
        return t;
    }//##########################################################################

    //Function to find the available tiles
    public void ComputeAvailableTiles()
    {
        //loop through all the tile gameobjects in the tiles array
        foreach(GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();

            //Get the tiles that are available from each tile
            t.FindAvailableTiles();
        }
    }

    //Find the tiles that are selectable
    public void FindSelectableTiles()
    {
        //Run the Functions to get all necessary information
        ComputeAvailableTiles();
        GetCurrentTile();

        //Create a queue used to iterate through the tiles
        Queue<Tile> process = new Queue<Tile>();

        //add the current tile to the queue
        process.Enqueue(currentTile);
        //set it to visited
        currentTile.visited = true;

        //while there are still elements in the queue loop through
        while(process.Count > 0)
        {
            //take a tile out of the queue and set it to a variable
            Tile t = process.Dequeue();

            //add the tile to the selectable tiles list
            selectableTiles.Add(t);
            //set it to selectable
            t.selectable = true;

            //if the tiles distance is in the move radius
            if(t.distance < move)
            {
                //go through all the tiles available that have not been visited yet
                foreach(Tile tile in t.tiles)
                {
                    
                    if(!tile.visited)
                    {
                        //set the parent tile to the last tile
                        tile.parent = t;
                        //set it to visited
                        tile.visited = true;
                        //add the tile distance
                        tile.distance = 1 + t.distance;
                        //add to the queue the tiles in movement range from that tile
                        process.Enqueue(tile);
                    }
                }
            }

        }


    }

    //Function to move the player to the tile
    public void MoveToTile(Tile tile)
    {
        //Clear the path and set the chosen target tile to true, and actionTaken to true
        path.Clear();
        tile.target = true;
        actionTaken = true;
        isMoving = true;

        //Set the next tile when it reaches the target tile
        Tile next = tile;

        //loop through from the target tile until the path goes to the start tile. The stack is in the reverse direction so when it is used to move the player will follow the correct path
        while(next != null)
        {
            path.Push(next);
            next = next.parent;
        }

    }

    //Moves the character to the correct tile
    public void Move()
    {
        //if there is tiles to move
        if(path.Count > 0)
        {
            //look at the tile path
            Tile t = path.Peek();
            //set the target position to the next tile location
            Vector3 targetPos = t.transform.position;
            //set target position y vector to the height the character rests above the tiles so character move linearly
            targetPos.y = 0.6267f;

            //if the character is greater than .05 unit away from the target tile keep moving towards that tile
            if (Vector3.Distance(transform.position, targetPos) >= 0.05f)
            {
                CalculateHeading(targetPos);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = targetPos;
                path.Pop();
            }

        }
        else
        {
            //Remove all tiles from selectable tiles so it can be updated later
            RemoveSelectableTiles();
            //set is moving to false
            isMoving = false;
            //make actionTaken false
            actionTaken = false;
            //end the turn
            TurnManager.EndTurn();

        }

    }

    public void Fart()
    {
        turnsFarted = playerTurns;
        isFarting = false;
        actionTaken = false;
        TurnManager.EndTurn();
    }

    //Function to remove selectable tiles
    protected void RemoveSelectableTiles()
    {
        
        if(currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach(Tile tile in selectableTiles)
        {
            tile.Reset();

        }
        selectableTiles.Clear();
    }

    //Calculate the heading for the movement vectors
    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;

        heading.Normalize();
    }

    //Set the velocity to the moves speed
    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    //Begin turn
    public void BeginTurn()
    {
        if(gameObject.tag == "Player")
        {
            playerTurns++;
        }
        turn = true;
    }

    //End turn
    public void EndTurn()
    {
        turn = false;
    }


}
