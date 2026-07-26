using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public bool yourTurn = false;
    private bool taken = false;
    private bool played = false;

    [SerializeField] private List<CardTypes> currentHand = new();
    [SerializeField] private List<GameObject> handVisuals = new();

    [Header("Card Designs")]
    [SerializeField] private Transform cardStartPos;
    [SerializeField] private GameObject cardPrefab;

    private Vector3 cardOffset = new Vector3((float)0.39, (float)-0.88, (float)0.1);
    private int selectedIndex = 0;

    public void TakeCard()
    {
        if (!yourTurn || taken || DeckLogic.instance.currentDeck.Count == 0) return;

        // Add card to front of hand
        currentHand.Insert(0, DeckLogic.instance.currentDeck[0]);
        DeckLogic.instance.currentDeck.RemoveAt(0);

        GameObject card = Instantiate(cardPrefab, cardStartPos.position, cardStartPos.rotation, cardStartPos);
        card.GetComponent<MeshRenderer>().material = currentHand[0].texture;

        // Add physical card to front 
        handVisuals.Insert(0, card);
        UpdateHandVisuals();

        DeckLogic.instance.currentDeckPos++;
        UpdateDisplay();

        taken = true;
    }

    public void PlayCard()
    {
        if (!yourTurn || played || currentHand.Count == 0) return;

        // Remove visual + card
        Destroy(handVisuals[selectedIndex]);
        handVisuals.RemoveAt(selectedIndex);
        currentHand.RemoveAt(selectedIndex);

        selectedIndex = Mathf.Clamp(selectedIndex, 0, currentHand.Count - 1);

        UpdateHandVisuals();
        UpdateDisplay();
    }

    public void EndTurn()
    {
        TurnCounter.instance.checkTurn();
    }

    private void UpdateDisplay()
    {
        DeckLogic.instance.DisplayGunCardPos();
    }

    private void UpdateHandVisuals()
    {
        for (int i = 0; i < handVisuals.Count; i++)
        {
            int visualIndex;

            if (i <= selectedIndex)
            {
                // Cards before the selected one are reversed
                visualIndex = selectedIndex - i;
            }
            else
            {
                // Cards after continue behind it
                visualIndex = i;
            }

            handVisuals[i].transform.localPosition = cardOffset * visualIndex;
        }
    }

    public void NowsYourChance()
    {
        yourTurn = true;
        taken = false;
        played = false;
    }

    public void Scroll(int direction)
    {
        if (currentHand.Count <= 1) return;

        selectedIndex += direction;

        //selectedIndex = Mathf.Clamp(selectedIndex, 0, currentHand.Count - 1); //hard ends 
        selectedIndex = (selectedIndex + currentHand.Count) % currentHand.Count; //loop/wraps deck scroll

        UpdateHandVisuals();
    }

}