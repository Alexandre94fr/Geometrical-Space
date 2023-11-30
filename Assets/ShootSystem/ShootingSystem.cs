using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject _rectangularShotPrefab;
    [SerializeField] GameObject _circularShotPrefab;
    [SerializeField] ShotStats _shotStats;
    [SerializeField] GameObject _allShotsParent;
    #endregion

    #region Methods

    private void Start()
    {
        StartCoroutine(ShootEndlessly());
    }

    public void ReceiveShootingSystemStats(GameObject allShotGameObject, ShotStats shotStats)
    {
        _allShotsParent = allShotGameObject;
        _shotStats = shotStats;
    } 

    IEnumerator ShootEndlessly()
    {
        while (gameObject.activeSelf)
        {
            if (_shotStats.singleShot)
            {
                GameObject projectile = PoolingSystemManager.Instance.GetDisableRectangularProjectile();

                SettingsProjectileStats(projectile, _shotStats);
            }
            else if (_shotStats.multiShot)
            {

            }
            else if (_shotStats.spawningShot)
            {

            }

            // Cooldown before shooting another time
            yield return new WaitForSeconds(_shotStats.projectileTimeBetweenShot);
        }
    }

    void SettingsProjectileStats(GameObject projectile, ShotStats shotStats)
    {
        #region Calcul of the start position of the projectile
        // Normalization of the baseProjectileDirection Vector2 variable
        shotStats.baseProjectileDirection.Normalize();

        // Calculation of the startPosition of the projectile
        Vector2 startPosition = new(transform.position.x, transform.position.y + GetComponent<SpriteRenderer>().sprite.bounds.size.y * shotStats.baseProjectileDirection.y);
        #endregion

        #region Settings up projectile's caracteristics
        // New projectile's position
        projectile.transform.position = startPosition;

        // Setting name and tag of the projectile to his correspondant values
        projectile.name = shotStats.projectileType.ToString();
        projectile.tag = shotStats.projectileTag.ToString();

        // Transfert projectile data to the projectile -> Make the projectile move into a precise direction, and can deal damage
        projectile.GetComponent<MovementManager>().RecieveShotStats(shotStats);

        // Assignation of a sprite, sprite size, and sprite color to the projectile
        SpriteRenderer shotSpriteRenderer = projectile.GetComponent<SpriteRenderer>();
        shotSpriteRenderer.sprite = shotStats.projectileSprite;
        projectile.transform.localScale = shotStats.spriteSize;
        shotSpriteRenderer.color = shotStats.spriteColor;

        projectile.SetActive(true);
        #endregion
    }
    #endregion
}