using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Variables required to check for nearby tiles
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    //List to store available tiles
    public List<Tile> tiles = new List<Tile>();

    //BFS (breadth first search variables
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

          
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(current)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        else if(target)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if(selectable)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        
    }

    //Reset all the components for the next call
    public void Reset()
    {
        tiles.Clear();

        current = false;
        target = false;
        selectable = false;

        visited = false;
        parent = null;
        distance = 0;

    }

    //Find the available tiles
    public void FindAvailableTiles()
    {
        Reset();

        //Find the tiles nearby in these directions
        CheckTiles(Vector3.forward);
        CheckTiles(-Vector3.forward);
        CheckTiles(Vector3.right);
        CheckTiles(-Vector3.right);

    }


    //Function to check for the tiles
    public void CheckTiles(Vector3 direction)
    {
        Vector3 halfExtents = new Vector3(0.25f,0.25f,0.25f);

        //Gets array of colliders collided by the Physics overlap box in the direction passed in
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        //Goes through each collider in the collider array
        foreach(Collider col in colliders)
        {
            //Get the tile component
            Tile tile = col.GetComponent<Tile>();
            
            if(tile != null && tile.walkable)
            {
                //Check if there is anything already on the tile
                RaycastHit hit;
                
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                {
                    //add the tile to the list if there isnt
                    tiles.Add(tile);
                } 

            }
        }


    }


}
