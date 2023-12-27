using UnityEngine;

public class HitManager : MonoBehaviour
{
    #region Variables
    HealthManager _healthManager;
    #endregion

    #region Methods
    private void Awake()
    {
        _healthManager = GetComponent<HealthManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the GameObject is an Enemy
        if (CompareTag("Enemy"))
        {
            // If player projectile collide enemy
            if (collision.CompareTag("PlayerProjectile"))
            {
                _healthManager.CurrentHP -= collision.GetComponent<MovementManager>()._shotStats.projectileDamage;
                // Projectile will be re-usable thanks to the Pooling system
                collision.gameObject.SetActive(false);
            }
        }
        // If the GameObject is an Player
        else if (CompareTag("Player"))
        {
            // If enemy projectile collide player
            if (collision.CompareTag("EnemyProjectile"))
            {
                _healthManager.CurrentHP -= collision.GetComponent<MovementManager>()._shotStats.projectileDamage;
                // Projectile will be re-usable thanks to the Pooling system
                collision.gameObject.SetActive(false);
            }
            // If enemy body collide player
            else if (collision.CompareTag("Enemy"))
            {
                _healthManager.CurrentHP -= collision.GetComponent<MovementManager>()._enemyStats.enemyDamageWhenCollide;
            }
        }
    }
    #endregion
}