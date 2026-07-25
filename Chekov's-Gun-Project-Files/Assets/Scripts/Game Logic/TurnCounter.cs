using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TurnCounter : MonoBehaviour
{
    public static TurnCounter instance;

    [Header("Turn Order")]
    private GameObject[] players;
    private GameObject[] bots;
    public GameObject[] turnOrder;
    private int turn = 0; 

    private void Awake()
    {
        if (instance == null) instance = this;

        //Grab all players and bots and randomize in an array
        GetPlayers();

        //Assign each player a number
        //Assign each player a turn based on said number 
        //If their turn tick bool "your turn" in PlayerHand script
        checkTurn();
    }

    public void checkTurn()
    {
        int turnMemory = turn;
        turn++;
        if (turnOrder.Length == turn) turn = 0;
        if (turnOrder[turnMemory].CompareTag("Player")) turnOrder[turnMemory].GetComponent<PlayerHand>().NowsYourChance();
        else if (turnOrder[turnMemory].CompareTag("Bot")) turnOrder[turnMemory].GetComponent<OpponentLogic>().EnemyTake();
        else return;
    }

    private void GetPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        bots = GameObject.FindGameObjectsWithTag("Bot");

        turnOrder = new GameObject[players.Length + bots.Length];

        GameObject[] turnOrderHolder = new GameObject[players.Length + bots.Length];
        Array.Copy(players, 0, turnOrderHolder, 0, players.Length);
        Array.Copy(bots, 0, turnOrderHolder, players.Length, bots.Length);

        //Ensure no duplicate turns
        int[] duplicateCheck = new int[turnOrderHolder.Length];

        // Mark every slot as unused
        for (int x = 0; x < duplicateCheck.Length; x++) duplicateCheck[x] = -1;

        //Randomize turns
        for (int i = 0; i < turnOrderHolder.Length; i++)
        {
            int j = UnityEngine.Random.Range(0, turnOrderHolder.Length);
            bool dupe = false;

            for (int k = 0; k < i; k++)
            {
                if (duplicateCheck[k] == j)
                {
                    dupe = true;
                    break;
                }
            }

            if (dupe)
            {
                i--;
                continue;
            }

            duplicateCheck[i] = j;
            turnOrder[i] = turnOrderHolder[j];
        }
    }
}
