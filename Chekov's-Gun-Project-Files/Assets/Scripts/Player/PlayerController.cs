using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private Camera eyes;
    [SerializeField] private bool hasGun;

    private Player_Inputs controls;

    private void Awake()
    {
        controls = new Player_Inputs();
        controls.Player.Click.performed += OnClick;
        controls.Player.BrowseDeck.performed += ScrollHand;
        controls.Player.Take.performed += TakeCard;
        controls.Player.Play.performed += PlayCard;
        controls.Player.End.performed += EndTurn;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();

        Ray clickray = eyes.ScreenPointToRay(mousePos);

        RaycastHit hit;

        bool hitConfirmed = Physics.Raycast(clickray, out hit);

        if (hitConfirmed)
        {
            if (hit.transform.gameObject.CompareTag("TakeButton"))
            {
                playerHand.TakeCard();
            }
            else if (hit.transform.gameObject.CompareTag("PlayButton"))
            {
                playerHand.PlayCard();
            }
            else if (hit.transform.gameObject.CompareTag("EndButton"))
            {
                playerHand.EndTurn();
            }
            else if (hasGun && hit.transform.gameObject.CompareTag("Player") || hasGun && hit.transform.gameObject.CompareTag("Bot"))
            {
                Destroy(hit.transform.gameObject);
                Debug.Log("Win");
            }
            else return;
        }
    }

    private void ScrollHand(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        if (value > 0) playerHand.Scroll(1);
        else if (value < 0) playerHand.Scroll(-1);
    }

    private void TakeCard(InputAction.CallbackContext context)
    {
        playerHand.TakeCard();
    }

    private void PlayCard(InputAction.CallbackContext context)
    {
        playerHand.PlayCard();
    }

    private void EndTurn(InputAction.CallbackContext context)
    {
        playerHand.EndTurn();
    }
}