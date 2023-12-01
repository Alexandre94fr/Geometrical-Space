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

        if (CompareTag("PlayerProjectile") || CompareTag("EnemyProjectile"))
        {
            RotateOurselfToOurDirection(_direction);
        }
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

    void RotateOurselfToOurDirection(Vector2 direction)
    {
        // Reset last rotation
        transform.rotation = new(0, 0, 0, 1);

        // Calcul and setting of the angle
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        // Apply rotation to the transform
        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }
    #endregion
}