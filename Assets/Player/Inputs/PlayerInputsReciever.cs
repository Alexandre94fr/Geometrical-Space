using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsReciever : MonoBehaviour
{
    #region Variables
    PauseMenuHandler _pauseMenuHandler;
    #endregion

    #region Methods
    private void Start()
    {
        _pauseMenuHandler = PauseMenuHandler.Instance;
    }

    public void RecieveMouvementKeys(InputAction.CallbackContext context)
    {
        PlayerMouvements.Instance.RecieveMouvementInputValues(context.ReadValue<Vector2>());
    }    

    public void RecievePauseKeys(InputAction.CallbackContext context)
    {
        if (context.performed && _pauseMenuHandler.gameObject.activeSelf == true)
        {
            Cursor.visible = false;
            _pauseMenuHandler.UnpauseGame();

        }
        else if (context.performed)
        {
            Cursor.visible = true;
            _pauseMenuHandler.PauseGame();
        }
    }
    #endregion
}