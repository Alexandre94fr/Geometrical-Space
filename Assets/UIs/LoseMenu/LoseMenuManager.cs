using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoseMenuManager : MonoBehaviour
{
    #region Variables
    public static LoseMenuManager Instance;

    [SerializeField] TextMeshProUGUI _scoreTextUI;
    [SerializeField] TextMeshProUGUI _scoreOutlineTextUI;
    [SerializeField] GameObject _restartButtonUI;
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

    public void PlayerLose(int playerScore)
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_restartButtonUI);

        _scoreTextUI.text = "You had " + playerScore.ToString() + " score";
        _scoreOutlineTextUI.text = "You had " + playerScore.ToString() + " score";
    }
    #endregion
}