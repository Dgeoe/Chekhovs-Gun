using System.Collections.Generic;
using UnityEngine;

public class OpponentLogic : MonoBehaviour
{
    [SerializeField] private List<CardTypes> currentHand = new();
    [SerializeField] private List<GameObject> handVisuals = new();

    [Header("Card Designs")]
    [SerializeField] private Transform cardStartPos;
    [SerializeField] private GameObject cardPrefab;

    private Vector3 cardOffset = new Vector3((float)0.39, (float)-0.88, (float)0.1);

    public void EnemyTake()
    {
        if (DeckLogic.instance.currentDeck.Count == 0) return;

        // Add card to front of hand
        currentHand.Insert(0, DeckLogic.instance.currentDeck[0]);
        DeckLogic.instance.currentDeck.RemoveAt(0);

        // Create visual at the start position
        GameObject card = Instantiate(cardPrefab, cardStartPos.position, cardStartPos.rotation, cardStartPos);
        handVisuals.Insert(0, card);

        UpdateHandVisuals();

        DeckLogic.instance.currentDeckPos++;
        UpdateDisplay();

        TurnCounter.instance.checkTurn();
    }

    private void UpdateHandVisuals()
    {
        for (int i = 0; i < handVisuals.Count; i++)
        {
            handVisuals[i].transform.localPosition = cardOffset * i;
        }
    }

    private void UpdateDisplay()
    {
        DeckLogic.instance.DisplayGunCardPos();
    }
}