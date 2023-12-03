using UnityEngine;

public class MovementManager : MonoBehaviour
{
    #region Variables

    [HideInInspector] public EnemyStats _enemyStats;
    [HideInInspector] public ShotStats _shotStats;

    PlayerMouvements _player;
    Vector2 _direction;
    float _mouvementSpeed;
    #endregion

    #region Methods
    void Awake()
    {
        _player = PlayerMouvements.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyStats != null)
        {
            if (_enemyStats.enemyMoveToThePlayerPosition && _player != null)
            {
                _direction = (_player.transform.position - transform.position).normalized;
                RotateOurselfToOurDirection(_direction);
            }
        }

        MoveOurselves(_direction, _mouvementSpeed);
    }

    #region Recievers
    public void RecieveShotStats(ShotStats shotStats, GameObject shooter)
    {
        if (shotStats.multiShot)
        {
            _direction = (transform.position - shooter.transform.position).normalized;
        }
        // The projectile will have the direction of the shooter + is set direction
        else
        {
            if (CompareTag("PlayerProjectile"))
            {
                _direction = shooter.transform.rotation * shotStats.baseProjectileDirection.normalized;
            }
            else if (CompareTag("EnemyProjectile"))
            {
                _direction = shooter.transform.rotation * -shotStats.baseProjectileDirection.normalized;
            }
        }
        
        _mouvementSpeed = shotStats.projectileSpeed;

        _shotStats = shotStats;

        RotateOurselfToOurDirection(_direction);
    }

    public void RecieveEnemyStats(EnemyStats enemyStats)
    {
        if (enemyStats.enemyMoveToTheLastPlayerPosition && _player != null)
        {
            _direction = (_player.transform.position - transform.position).normalized;
        }
        else
        {
            _direction = enemyStats.baseEnemyDirection.normalized;
        }

        _mouvementSpeed = enemyStats.enemySpeed;

        _enemyStats = enemyStats;

        RotateOurselfToOurDirection(_direction);
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