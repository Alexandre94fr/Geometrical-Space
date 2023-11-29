using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region Variables
    int _maxHP = 1;
    int _currentHP = 1;
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
    public void RecieveEnemyHealthStats(int maxHP)
    {
        _maxHP = maxHP;
        _currentHP = maxHP;
    }

    void EnemyDie()
    {
        // TO DO : Quand il rencontre une condition de disparition il repart dans le système de pooling

        Destroy(gameObject);
    }

    void PlayerDie()
    {
        Destroy(gameObject);
    }
    #endregion
}