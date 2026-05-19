using UnityEngine;

public class OpponentLogic : MonoBehaviour
{
    public static OpponentLogic instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void EnemyTurn()
    {
        if (TurnCounter.instance.player1) return;

        DeckLogic.instance.currentDeckPos++;
        if (DeckLogic.instance.currentDeckPos == DeckLogic.instance.gunCard)
        {
            Debug.Log("Lose");
        }
        TurnCounter.instance.player1 = true;
    }
}
