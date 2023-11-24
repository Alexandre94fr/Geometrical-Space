using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsReciever : MonoBehaviour
{
    public void RecieveMouvementKeys(InputAction.CallbackContext context)
    {
        PlayerMouvements.Instance.RecieveMouvementInputValues(context.ReadValue<Vector2>());
    }    

    public void RecievePauseKeys(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
        }
    }
}