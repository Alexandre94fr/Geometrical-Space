using System.Collections.Generic;
using UnityEngine;

public class PoolingSystemManager : MonoBehaviour
{
    #region Variables
    public static PoolingSystemManager Instance;

    [SerializeField] GameObject _allShotsParent;
    [SerializeField] ShotStats _shotStats;

    [Header("Rectangular Projectile")]
    [SerializeField] List<GameObject> _rectangularProjectileList;
    [SerializeField] int _numberOfRectangularPreloadedProjectiles;
    [SerializeField] GameObject _rectangularShotPrefab;

    [Header("Circular Projectile")]
    [SerializeField] List<GameObject> _circularProjectileList;
    [SerializeField] int _numberOfCircularPreloadedProjectiles;
    [SerializeField] GameObject _circularShotPrefab;

    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        InstantiationOfPreloadedRectangularProjectiles(_numberOfRectangularPreloadedProjectiles, _allShotsParent);
        InstantiationOfPreloadedCircularProjectiles(_numberOfCircularPreloadedProjectiles, _allShotsParent);
    }

    #region Initial Instantiation
    void InstantiationOfPreloadedRectangularProjectiles(                 
        int projectileNumber,
        GameObject projectilesParent)
    {
        // Creation of the list of size we want
        _rectangularProjectileList = new List<GameObject>(projectileNumber);

        // Instatiation and adding all the projectiles to the projectile list
        for (int i = 0; i < projectileNumber; i++)
        {
            GameObject projectile = Instantiate(_rectangularShotPrefab, transform.position, Quaternion.identity, projectilesParent.transform);

            projectile.SetActive(false);

            _rectangularProjectileList.Add(projectile);
        }
    }
    void InstantiationOfPreloadedCircularProjectiles(
        int projectileNumber,
        GameObject projectilesParent)
    {
        // Creation of the list of size we want
        _circularProjectileList = new List<GameObject>(projectileNumber);

        // Instatiation and adding all the projectiles to the projectile list
        for (int i = 0; i < projectileNumber; i++)
        {
            GameObject projectile = Instantiate(_circularShotPrefab, transform.position, Quaternion.identity, projectilesParent.transform);

            projectile.SetActive(false);

            _circularProjectileList.Add(projectile);
        }
    }
    #endregion

    void Reactive()
    {
        /*#region Calcul of the start position of the projectile
        // Normalization of the baseProjectileDirection Vector2 variable
        shotStats.baseProjectileDirection.Normalize();

        // Calculation of the startPosition of the projectile
        Vector2 startPosition = new(transform.position.x, transform.position.y + GetComponent<SpriteRenderer>().sprite.bounds.size.y * shotStats.baseProjectileDirection.y);
        #endregion

        #region Apparition of the projectile
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

       
        #region Settings up projectile's caracteristics
        // Setting name and tag of the projectile to his correspondant values
        shot.name = shotStats.projectileType.ToString();
        shot.tag = shotStats.projectileTag.ToString();

        // Transfert projectile data to the projectile -> Make the projectile move into a precise direction, and can deal damage
        shot.GetComponent<MovementManager>().RecieveShotStats(shotStats);

        // Assignation of a sprite, sprite size, and sprite color to the projectile
        SpriteRenderer shotSpriteRenderer = shot.GetComponent<SpriteRenderer>();
        shotSpriteRenderer.sprite = shotStats.projectileSprite;
        shot.transform.localScale = shotStats.spriteSize;
        shotSpriteRenderer.color = shotStats.spriteColor;
        #endregion */
    }

    void Desactive()
    {

    }
    #endregion
}