using UnityEngine;

public class TurnCounter : MonoBehaviour
{
    public bool player1 = true;
    public static TurnCounter instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public bool checkTurn()
    {
        if (player1 == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
