using UnityEngine;

public class MouvementManager : MonoBehaviour
{
    #region Variables
    Vector2 _direction;
    float _mouvementSpeed;
    #endregion

    #region Methods
    // Update is called once per frame
    void Update()
    {
        MoveAndRotateOurselves(_direction, _mouvementSpeed);
    }

    public void RecieveStats(ShotStats shotStats)
    {
        // Normalization of the direction Vector2 variable
        shotStats.baseProjectileDirection.Normalize();

        _direction = shotStats.baseProjectileDirection;
        _mouvementSpeed = shotStats.projectileSpeed;
    }
    public void RecieveStats(EnemyStats enemyStats)
    {
        // Normalization of the direction Vector2 variable
        enemyStats.baseEnemyDirection.Normalize();

        _direction = enemyStats.baseEnemyDirection;
        _mouvementSpeed = enemyStats.enemySpeed;
    }

    void MoveAndRotateOurselves(Vector2 direction, float mouvementSpeed)
    {
        // Move the projectile in a certain direction at a certain speed over time
        transform.Translate(mouvementSpeed * Time.deltaTime * direction, Space.World);

        if (CompareTag("Enemy"))
        {
            transform.rotation = new(0f, 0f, direction.y, 0f);
        }
        else if (CompareTag("PlayerProjectile") || CompareTag("EnemyProjectile"))
        {
            // Nothing now
        }
    }
    #endregion
}