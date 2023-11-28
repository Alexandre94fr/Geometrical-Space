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
            if (_currentHP - value <= 0 && CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
            else if (_currentHP - value <= 0 && CompareTag("Player"))
            {
                PlayerDie();
            }
            else
            {
                Debug.Log("Hit");
                _currentHP = value;
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

    void PlayerDie()
    {
        Debug.Log("Player is dead");
    }
    #endregion
}