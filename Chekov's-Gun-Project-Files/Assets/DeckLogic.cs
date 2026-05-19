using UnityEngine;

public class DeckLogic : MonoBehaviour
{
    //ON START: gun card is placed at a position in the deck 
    private int deck = 52;
    public int gunCard;
    public int currentDeckPos = 0; //itterate throughout the game
    public static DeckLogic instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        gunCard = Random.Range(1, 52);
    }

    public void howFar()
    {
        int distance = gunCard - currentDeckPos;
        Debug.Log("Gun is at " + gunCard);
        Debug.Log("Which is " + distance + " Cards Down");
    }

    public void takeCard()
    {
        if (TurnCounter.instance.player1 == false) return;

        currentDeckPos++;
        if (currentDeckPos == gunCard)
        {
            Debug.Log("Win");
        }

        TurnCounter.instance.player1 = false;
        OpponentLogic.instance.EnemyTurn();
    }
}
