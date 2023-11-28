using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    #region Variables
    public static EnemySpawn Instance;

    [SerializeField] GameObject _allEnemiesParent;
    [SerializeField] EnemyStats _enemyStats;

    [Header("Enemy Prefabs")]
    [SerializeField] GameObject _minionPrefab;
    [SerializeField] GameObject _squarePrefab;
    [SerializeField] GameObject _pantagonPrefab;
    [SerializeField] GameObject _hexagonPrefab;
    [SerializeField] GameObject _octogonPrefab;
    #endregion

    #region Methods

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SpawnEnemy(_enemyStats);
    }

    void SpawnEnemy(EnemyStats enemyStats)
    {
        GameObject enemy = null;

        switch (enemyStats.enemyType)
        {
            case EnemyStats.EnemyListEnum.Minion:
                enemy = Instantiate(_minionPrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);
                break;
            case EnemyStats.EnemyListEnum.Square:
                enemy = Instantiate(_squarePrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);
                break;
            case EnemyStats.EnemyListEnum.Pantagon:
                enemy = Instantiate(_pantagonPrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);
                break;
            case EnemyStats.EnemyListEnum.Hexagon:
                enemy = Instantiate(_hexagonPrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);
                break;
            case EnemyStats.EnemyListEnum.Octogon:
                enemy = Instantiate(_octogonPrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);
                break;
            default:
                Debug.Assert(false, $"The enemy type nammed {enemyStats.enemyType}, have a value not planned in the switch");
                Debug.Break();
                break;
        }

        #region Settings up projectile's caracteristics
        // Setting name and tag of the projectile to his correspondant values
        enemy.name = enemyStats.enemyType.ToString();
        enemy.tag = "Enemy";

        // Transfert projectile data to the projectile -> Make the projectile move into a precise direction, and can deal damage
        enemy.GetComponent<ProjectileManager>().RecieveStats(enemyStats); // TO DO : SEPARER LE PROJECTILE MANAGER EN DUEX SCRIPTS (PROJECTILE / ENEMY)
        enemy.GetComponent<HealthManager>().RecieveEnemyHealthStats(enemyStats.enemyHP);

        // Assignation of a sprite, sprite size, and sprite color to the projectile
        SpriteRenderer shotSpriteRenderer = enemy.GetComponent<SpriteRenderer>();
        shotSpriteRenderer.sprite = enemyStats.enemySprite;
        enemy.transform.localScale = enemyStats.spriteSize;
        shotSpriteRenderer.color = enemyStats.spriteColor;
        #endregion
    }
    #endregion
}