using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region Variables
    int _maxHP = 1;
    int _currentHP = 1;
    int _givenScoreWhenDestroyed;
    #endregion

    #region Getter / Setter
    public int MaxHP {
        get { return _maxHP; } 
    }

    public int CurrentHP {
        get { return _currentHP; }
        set {
            _currentHP = value;

            if (_currentHP <= 0 && CompareTag("Enemy"))
            {
                EnemyDie();
            }
            else if (_currentHP <= 0 && CompareTag("Player"))
            {
                PlayerDie();
            }
        }
    }
    #endregion

    #region Methods
    // Only used by the enemies
    public void RecieveEnemyStats(int maxHP, int givenScoreWhenDestroyed)
    {
        _maxHP = maxHP;
        _currentHP = maxHP;
        _givenScoreWhenDestroyed = givenScoreWhenDestroyed;
    }

    void EnemyDie()
    {
        ScoreManager.Instance.PlayerScore += _givenScoreWhenDestroyed;

        Destroy(gameObject);
    }

    void PlayerDie()
    {
        LoseMenuManager.Instance.PlayerLose(ScoreManager.Instance.PlayerScore);

        Destroy(gameObject);
    }
    #endregion
}