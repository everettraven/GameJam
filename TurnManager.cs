using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    //Create a dictionary to contain a tag and list of units as part of that tag
    static Dictionary<string, List<TacticalMovement>> units = new Dictionary<string, List<TacticalMovement>>();
    //Create a turnKey queue to keep track of what teams turn it is
    static Queue<string> turnKey = new Queue<string>();
    //Create a queue that contains units on each team
    static Queue<TacticalMovement> turnTeam = new Queue<TacticalMovement>();
    private void OnLevelWasLoaded(int level)
    {
        //Create a dictionary to contain a tag and list of units as part of that tag
      units = new Dictionary<string, List<TacticalMovement>>();
        //Create a turnKey queue to keep track of what teams turn it is
         turnKey = new Queue<string>();
        //Create a queue that contains units on each team
        turnTeam = new Queue<TacticalMovement>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    static   void InitTeamTurnQueue()
    {
        List<TacticalMovement> teamList = units[turnKey.Peek()];

        foreach(TacticalMovement unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }

        StartTurn();
    }


    public static void StartTurn()
    {
        if(turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        TacticalMovement unit = turnTeam.Dequeue();
        unit.EndTurn();

        if(turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(TacticalMovement unit)
    {
        List<TacticalMovement> list;


        if(!units.ContainsKey(unit.tag))
        {
            list = new List<TacticalMovement>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }

}
