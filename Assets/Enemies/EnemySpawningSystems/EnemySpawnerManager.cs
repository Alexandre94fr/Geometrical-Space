using System;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    #region Variables
    public static EnemySpawn Instance;

    // To return to other script
    GameObject _lastEnemyCreated;

    [Header("References :")]
    [SerializeField] GameObject _allEnemiesParent;
    #endregion

    #region Methods
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void RecieveInfo(GameObject ennemiesParent)
    {
        _allEnemiesParent = ennemiesParent;
    }

    public void SpawnEnemy(EnemyStats enemyStats)
    {
        GameObject enemy = Instantiate(enemyStats.enemyPrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);

        #region Settings up projectile's caracteristics
        // Setting name and tag of the projectile to his correspondant values
        enemy.name = enemyStats.name;
        enemy.tag = "Enemy";

        // Assignation of a sprite, sprite size, and sprite color to the projectile
        SpriteRenderer enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();
        enemySpriteRenderer.sprite = enemyStats.enemySprite;
        enemy.transform.localScale = enemyStats.spriteSize;
        enemySpriteRenderer.color = enemyStats.spriteColor;

        // Transfert data to the shooting system
        if (enemyStats.isShooting)
        {
            enemy.GetComponent<ShootingSystem>().ReceiveShootingSystemStats(enemyStats.shotType);
        }
        else
        {
            enemy.GetComponent<ShootingSystem>().enabled = false;
        }

        // Transfert projectile data to the projectile -> Make the projectile move into a precise direction, and can deal damage
        enemy.GetComponent<MovementManager>().RecieveEnemyStats(enemyStats);

        // Transfert health stats
        enemy.GetComponent<HealthManager>().RecieveEnemyStats(enemyStats.enemyHP, enemyStats.givenScoreWhenDestroyed);

        _lastEnemyCreated = enemy;
        #endregion
    }

    public GameObject ReturnTheLastEnemyCreated()
    {
        return _lastEnemyCreated;
    }
    #endregion
}