using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Variables
    public static ScoreManager Instance;

    int _playerScore;

    public int PlayerScore
    {
        get { return _playerScore; }
        set
        {
            _playerScore = value;
            if (_playerScore < 0)
            {
                _playerScore = 0;
            }
        }
    }
    #endregion

    #region Methods
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion
}