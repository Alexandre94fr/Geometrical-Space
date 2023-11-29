using UnityEngine;

public class MovementManager : MonoBehaviour
{
    #region Variables
    [HideInInspector] public EnemyStats _enemyStats;
    [HideInInspector] public ShotStats _shotStats;

    Vector2 _direction;
    float _mouvementSpeed;
    #endregion

    #region Methods
    void Start()
    {
        if (CompareTag("PlayerProjectile") || CompareTag("EnemyProjectile"))
        {
            RotateOurselfToOurDirection();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveOurselves(_direction, _mouvementSpeed);
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

    void MoveOurselves(Vector2 direction, float mouvementSpeed)
    {
        // Move the projectile in a certain direction at a certain speed over time
        transform.Translate(mouvementSpeed * Time.deltaTime * direction, Space.World);
    }

    void RotateOurselfToOurDirection()
    {
        // To optimize
        Transform actualTransform = transform;

        // Calcul and setting of the angle
        float angle = Mathf.Atan2(_direction.x - actualTransform.position.x, _direction.y - actualTransform.position.y) * Mathf.Rad2Deg;
        actualTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
    #endregion
}