using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject _rectangularShotPrefab;
    [SerializeField] GameObject _circularShotPrefab;
    [SerializeField] ShotStats _shotStats;
    #endregion

    #region Methods

    private void Start()
    {
        StartCoroutine(ShootEndlessly());
    }

    public void ReceiveShootingSystemStats(ShotStats shotStats)
    {
        _shotStats = shotStats;
    } 

    IEnumerator ShootEndlessly()
    {
        while (gameObject.activeSelf)
        {
            if (_shotStats.singleShot)
            {
                // Return the first rectangular projectile available
                GameObject projectile = PoolingSystemManager.Instance.GetDisableRectangularProjectile(1)[0];

                SettingsProjectileStats(projectile, _shotStats);
            }
            else if (_shotStats.multiShot)
            {
                List<GameObject> projectiles = PoolingSystemManager.Instance.GetDisableCircularProjectile(_shotStats.projectilesStats.Count);

                SettingsProjectilesStats(projectiles, _shotStats);
            }
            else if (_shotStats.spawningShot)
            {
                InstantiateOtherEnemies(_shotStats.enemyList, _shotStats);
            }

            // Cooldown before shooting another time
            yield return new WaitForSeconds(_shotStats.projectileTimeBetweenShot);
        }
    }

    /// <summary> Used to set the value of ONE projectile (used for singleShot) </summary>
    void SettingsProjectileStats(GameObject projectile, ShotStats shotStats)
    {
        // To optimize
        float spriteYSize = GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        #region Calcul of the start position of the projectile
        // Normalization of the baseProjectileDirection Vector2 variable
        shotStats.baseProjectileDirection.Normalize();

        // Calculation of the startPosition of the projectile
        Vector2 startPosition = new(transform.position.x, transform.position.y + spriteYSize * shotStats.baseProjectileDirection.y);
        #endregion

        #region Settings up projectile's caracteristics
        // New projectile's position
        projectile.transform.position = startPosition;

        // Setting name and tag of the projectile to his correspondant values
        projectile.name = shotStats.name;
        projectile.tag = shotStats.projectileTag.ToString();

        // Assignation of a sprite, sprite size, and sprite color to the projectile
        SpriteRenderer shotSpriteRenderer = projectile.GetComponent<SpriteRenderer>();
        shotSpriteRenderer.sprite = shotStats.projectileSprite;
        projectile.transform.localScale = shotStats.spriteSize;
        shotSpriteRenderer.color = shotStats.spriteColor;

        // Transfert projectile data to the projectile -> Make the projectile move into a precise direction, and can deal damage
        projectile.GetComponent<MovementManager>().RecieveShotStats(shotStats, gameObject);
        #endregion

        #region PlayShootingSFX
        AudioSource audioSource = projectile.GetComponent<AudioSource>();

        if (shotStats.projectileTag == ShotStats.ProjectileTag.PlayerProjectile)
        {
            audioSource.clip = SFXManager.Instance.ReturnSFX(SFXManager.TypesOfSFX.PlayerShotting);
        }
        else if (shotStats.projectileTag == ShotStats.ProjectileTag.EnemyProjectile)
        {
            audioSource.clip = SFXManager.Instance.ReturnSFX(SFXManager.TypesOfSFX.EnemyShotting);
        }

        audioSource.Play();
        #endregion

        projectile.SetActive(true);
    }

    /// <summary> Used to set the value of MULTIPLE projectile (used for multipleShot) </summary>
    void SettingsProjectilesStats(List<GameObject> projectiles, ShotStats shotStats)
    {
        // To optimize
        float spriteYSize = GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        // Normalization of the baseProjectileDirection Vector2 variable
        shotStats.baseProjectileDirection.Normalize();

        // Number of projectile other than the middle one divide by two
        float numberProjectilesOnOneSide = (projectiles.Count - 1) / 2f;

        for (int i = 0; i < projectiles.Count; i++)
        {
            // To optimize
            GameObject projectile = projectiles[i];

            #region Calcul and settings of the start position of the projectile
            // Calculation of the offset from the shooter position
            Vector2 offset = 
                // Angle offset
                Quaternion.Euler(0, 0, (-shotStats.angledDifferenceBetweenProjectile * i) + shotStats.angledDifferenceBetweenProjectile * numberProjectilesOnOneSide) 
                // Direction who start the offset
                * shotStats.baseProjectileDirection * spriteYSize;

            // Settings the new position
            projectile.transform.position = new(transform.position.x + offset.x, transform.position.y + offset.y);
            #endregion

            #region Settings up projectile's caracteristics
            // Setting name and tag of the projectile to his correspondant values
            projectile.name = shotStats.name;
            projectile.tag = shotStats.projectilesStats[i].projectileTag.ToString();

            // Assignation of projectile size, sprite, and sprite color to the projectile
            projectile.transform.localScale = shotStats.projectilesStats[i].projectileSize;
            SpriteRenderer shotSpriteRenderer = projectile.GetComponent<SpriteRenderer>();
            shotSpriteRenderer.sprite = shotStats.projectilesStats[i].projectileSprite;
            shotSpriteRenderer.color = shotStats.projectilesStats[i].spriteColor;

            // Transfert projectile data to the projectile -> Make the projectile move into a precise direction, and can deal damage
            projectile.GetComponent<MovementManager>().RecieveShotStats(shotStats, gameObject);
            #endregion

            #region PlayShootingSFX
            AudioSource audioSource = projectile.GetComponent<AudioSource>();

            if (shotStats.projectileTag == ShotStats.ProjectileTag.PlayerProjectile)
            {
                audioSource.clip = SFXManager.Instance.ReturnSFX(SFXManager.TypesOfSFX.PlayerShotting);
            }
            else if (shotStats.projectileTag == ShotStats.ProjectileTag.EnemyProjectile)
            {
                audioSource.clip = SFXManager.Instance.ReturnSFX(SFXManager.TypesOfSFX.EnemyShotting);
            }

            audioSource.Play();
            #endregion

            projectile.SetActive(true);
        }
    }

    /// <summary> Used to Instantiate new enemies around the shooter (used for spawningShot) </summary>
    void InstantiateOtherEnemies(List<EnemyStats> enemyList, ShotStats shotStats)
    {
        // To optimize
        EnemySpawn enemySpawn = EnemySpawn.Instance;
        float spriteYSize = GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        // Normalization of the baseProjectileDirection Vector2 variable
        shotStats.baseProjectileDirection.Normalize();

        // Number of projectile other than the middle one divide by two
        float numberEnemiesOnOneSide = (enemyList.Count - 1) / 2f;

        for (int i = 0; i < enemyList.Count; i++)
        {
            // Anti-crash mesure
            if(enemyList[i].shotType == shotStats)
            {
                Debug.LogError("CRITICAL ERROR! You can't configure an enemies spawner to spawn other enemies spawner, else the game will be in an infinity loop.");
                return;
            }

            // Creation of a enemy and store his reference into a variable
            enemySpawn.SpawnEnemy(enemyList[i]);
            GameObject enemy = enemySpawn.ReturnTheLastEnemyCreated();

            #region Calcul and settings of the start position of the enemy
            // Calculation of the offset from the shooter position
            Vector2 offset =
                // Angle offset
                Quaternion.Euler(0, 0, (-shotStats.angledDifferenceBetweenEnemies * i) + shotStats.angledDifferenceBetweenEnemies * numberEnemiesOnOneSide)
                // Direction who start the offset
                * shotStats.baseProjectileDirection * (spriteYSize + 0.5f);

            // Settings the new position
            enemy.transform.position = new(transform.position.x + offset.x, transform.position.y + offset.y);
            #endregion

            // Transfert projectile data to the projectile -> Make the projectile move into a precise direction, and can deal damage
            //enemy.GetComponent<MovementManager>().RecieveEnemyStats(shotStats);
        }
    }
    #endregion
}