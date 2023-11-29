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
                InstantiateShot(_shotStats);
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

    // TO DO : Faire de cette fonction une class pour le pooling
    void InstantiateShot(ShotStats shotStats)
    {
        #region Calcul of the start position of the projectile
        // Normalization of the baseProjectileDirection Vector2 variable
        shotStats.baseProjectileDirection.Normalize();

        // Calculation of the startPosition of the projectile
        Vector2 startPosition = new(transform.position.x, transform.position.y + GetComponent<SpriteRenderer>().sprite.bounds.size.y * shotStats.baseProjectileDirection.y);
        #endregion

        #region Apparition of the projectile
        // TO CHANGE FOR THE POOLING SYSTEM
        GameObject shot = null;

        switch (shotStats.projectileSpriteType)
        {
            case ShotStats.ProjectileSpriteType.Rectangular:
                shot = Instantiate(_rectangularShotPrefab, startPosition, Quaternion.identity, _allShotsParent.transform);
                break;
            case ShotStats.ProjectileSpriteType.Circular:
                shot = Instantiate(_circularShotPrefab, startPosition, Quaternion.identity, _allShotsParent.transform);
                break;
            default:
                Debug.Assert(false, $"The projectile sprite type nammed {shotStats.projectileSpriteType}, have a value not planned in the switch");
                Debug.Break();
                break;
        }
        #endregion

        #region Settings up projectile's caracteristics
        // Setting name and tag of the projectile to his correspondant values
        shot.name = shotStats.projectileType.ToString();
        shot.tag = shotStats.projectileTag.ToString();

        // Transfert projectile data to the projectile -> Make the projectile move into a precise direction, and can deal damage
        shot.GetComponent<MouvementManager>().RecieveShotStats(shotStats);

        // Assignation of a sprite, sprite size, and sprite color to the projectile
        SpriteRenderer shotSpriteRenderer = shot.GetComponent<SpriteRenderer>();
        shotSpriteRenderer.sprite = shotStats.projectileSprite;
        shot.transform.localScale = shotStats.spriteSize;
        shotSpriteRenderer.color = shotStats.spriteColor;
        #endregion
    }
    #endregion
}