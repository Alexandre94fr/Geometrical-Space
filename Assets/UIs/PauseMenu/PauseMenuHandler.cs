using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuHandler : MonoBehaviour
{
    #region Variables
    public static PauseMenuHandler Instance;

    [SerializeField] GameObject _resumeButton;
    #endregion

    #region Methods
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        gameObject.SetActive(false);
    }

    // When the player leave the window (Alt Tab)
    // NOT WORKING (for some reason)
    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        EventSystem.current.SetSelectedGameObject(_resumeButton);

        Cursor.visible = true;

        gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Cursor.visible = false;

        gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
    #endregion
}