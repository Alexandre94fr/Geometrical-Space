using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    #region Variables
    ShotStats _shotStats;
    Vector2 _direction;
    bool _projectileHitHisEnemy;
    #endregion

    #region Methods
    // Update is called once per frame
    void Update()
    {
        MoveAndRotateOurselves(_direction, _shotStats.projectileSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_projectileHitHisEnemy)
        {
            if (collision.gameObject.CompareTag("Player") && CompareTag("EnemyProjectile") ||
                collision.gameObject.CompareTag("Enemy") && CompareTag("PlayerProjectile"))
            {
                _projectileHitHisEnemy = true;
                collision.gameObject.GetComponent<HealthManager>().CurrentHP =- _shotStats.projectileDamage;
            }
        }
    }

    public void RecieveShotStats(ShotStats shotStats)
    {
        _shotStats = shotStats;

        // Normalization of the direction Vector2 variable
        shotStats.baseProjectileDirection.Normalize();

        _direction = shotStats.baseProjectileDirection;
    }

    public void RecieveStats(EnemyStats enemyStats)
    {
        // Normalization of the direction Vector2 variable
        enemyStats.baseEnemyDirection.Normalize();

        _direction = enemyStats.baseEnemyDirection;
    }

    void MoveAndRotateOurselves(Vector2 direction, float mouvementSpeed)
    {
        // Move the projectile in a certain direction at a certain speed over time
        transform.Translate(mouvementSpeed * Time.deltaTime * direction, Space.World);

        // Nothing now for the rotation
    }
    #endregion
}

// TO DO : Quand il rencontre une condition de disparition il repart dans le système de pooling