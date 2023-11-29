using UnityEngine;

public class MouvementManager : MonoBehaviour
{
    #region Variables
    [HideInInspector] public EnemyStats _enemyStats;
    [HideInInspector] public ShotStats _shotStats;

    Vector2 _direction;
    float _mouvementSpeed;
    #endregion

    #region Methods
    // Update is called once per frame
    void Update()
    {
        MoveAndRotateOurselves(_direction, _mouvementSpeed);
    }

    #region Recievers
    public void RecieveShotStats(ShotStats shotStats)
    {
        // Normalization of the direction Vector2 variable
        shotStats.baseProjectileDirection.Normalize();

        _direction = shotStats.baseProjectileDirection;
        _mouvementSpeed = shotStats.projectileSpeed;

        _shotStats = shotStats;
    }

    public void RecieveEnemyStats(EnemyStats enemyStats)
    {
        // Normalization of the direction Vector2 variable
        enemyStats.baseEnemyDirection.Normalize();

        _direction = enemyStats.baseEnemyDirection;
        _mouvementSpeed = enemyStats.enemySpeed;

        _enemyStats = enemyStats;
    }
    #endregion

    void MoveAndRotateOurselves(Vector2 direction, float mouvementSpeed)
    {
        // Move the projectile in a certain direction at a certain speed over time
        transform.Translate(mouvementSpeed * Time.deltaTime * direction, Space.World);

        if (CompareTag("PlayerProjectile") || CompareTag("EnemyProjectile"))
        {
            // Rotate in direction, exemple Projectile goes to the right, Projectile look to the right
        }
    }
    #endregion
}