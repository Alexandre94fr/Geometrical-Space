using System;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    #region Variables
    public static EnemySpawn Instance;

    public EnemyStats EnemyThatWillSpawn;

    [Header("References :")]
    [SerializeField] GameObject _allShotsParent;
    [SerializeField] GameObject _allEnemiesParent;

    [SerializeField] EnemyPrefabs _enemyPrefabs;
    [SerializeField] ShotTypes _shotTypes;

    ShotStats _enemyShotType;

    #region Structs
    [Serializable]
    public struct EnemyPrefabs
    {
        public GameObject MinionPrefab;
        public GameObject SquarePrefab;
        public GameObject PantagonPrefab;
        public GameObject HexagonPrefab;
        public GameObject OctogonPrefab;
    }

    [Serializable]
    public struct ShotTypes
    {
        public ShotStats PlayerSingleShot;
        public ShotStats EnemySingleShot;
        public ShotStats FrontAngledTripleShot;
        public ShotStats MinionsShot;
        public ShotStats OctoShot;
    }

    #endregion

    #endregion

    #region Methods
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //SpawnEnemy(EnemyThatWillSpawn);
    }

    public void RecieveInfo(GameObject shotsParent, GameObject ennemiesParent)
    {
        _allShotsParent = shotsParent;
        _allEnemiesParent = ennemiesParent;
    }

    public void SpawnEnemy(EnemyStats enemyStats)
    {
        GameObject enemy = null;

        switch (enemyStats.enemyType)
        {
            case EnemyStats.EnemyListEnum.Minion:
                enemy = Instantiate(_enemyPrefabs.MinionPrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);
                _enemyShotType = _shotTypes.EnemySingleShot;
                break;
            case EnemyStats.EnemyListEnum.Square:
                enemy = Instantiate(_enemyPrefabs.SquarePrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);
                _enemyShotType = _shotTypes.FrontAngledTripleShot;
                break;
            case EnemyStats.EnemyListEnum.Pantagon:
                enemy = Instantiate(_enemyPrefabs.PantagonPrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);
                _enemyShotType = _shotTypes.MinionsShot;
                break;
            case EnemyStats.EnemyListEnum.Hexagon:
                enemy = Instantiate(_enemyPrefabs.HexagonPrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);
                _enemyShotType = null;
                break;
            case EnemyStats.EnemyListEnum.Octogon:
                enemy = Instantiate(_enemyPrefabs.OctogonPrefab, transform.position, Quaternion.identity, _allEnemiesParent.transform);
                _enemyShotType = _shotTypes.OctoShot;
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

        // Transfert data to the shooting system
        enemy.GetComponent<ShootingSystem>().ReceiveShootingSystemStats(_allShotsParent, _enemyShotType);
        // Transfert projectile data to the projectile -> Make the projectile move into a precise direction, and can deal damage
        enemy.GetComponent<MovementManager>().RecieveEnemyStats(enemyStats);
        enemy.GetComponent<HealthManager>().RecieveEnemyStats(enemyStats.enemyHP, enemyStats.givenScoreWhenDestroyed);

        // Assignation of a sprite, sprite size, and sprite color to the projectile
        SpriteRenderer shotSpriteRenderer = enemy.GetComponent<SpriteRenderer>();
        shotSpriteRenderer.sprite = enemyStats.enemySprite;
        enemy.transform.localScale = enemyStats.spriteSize;
        shotSpriteRenderer.color = enemyStats.spriteColor;
        #endregion
    }
    #endregion
}