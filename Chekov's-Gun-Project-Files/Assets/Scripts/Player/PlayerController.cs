using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private Camera eyes;

    private Player_Inputs controls;

    private void Awake()
    {
        controls = new Player_Inputs();
        controls.Player.Click.performed += OnClick;
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
                return;
            }
            else return;
        }
    }

    private void ScrollHand()
    {

    }
}
