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

        // Creation of the lists of size we want
        _rectangularProjectileList = new List<GameObject>(_numberOfRectangularPreloadedProjectiles);
        _circularProjectileList = new List<GameObject>(_numberOfCircularPreloadedProjectiles);

        InstantiationOfRectangularProjectiles(_numberOfRectangularPreloadedProjectiles);
        InstantiationOfCircularProjectiles(_numberOfCircularPreloadedProjectiles);
    }

    #region Initial Instantiation
    void InstantiationOfRectangularProjectiles(int projectileNumber)
    {
        // Instatiation and adding all the projectiles to the projectile list
        for (int i = 0; i < projectileNumber; i++)
        {
            GameObject projectile = Instantiate(_rectangularShotPrefab, transform.position, Quaternion.identity, _allShotsParent.transform);

            projectile.SetActive(false);

            _rectangularProjectileList.Add(projectile);
        }
    }
    void InstantiationOfCircularProjectiles(int projectileNumber)
    {
        // Instatiation and adding all the projectiles to the projectile list
        for (int i = 0; i < projectileNumber; i++)
        {
            GameObject projectile = Instantiate(_circularShotPrefab, transform.position, Quaternion.identity, _allShotsParent.transform);

            projectile.SetActive(false);

            _circularProjectileList.Add(projectile);
        }
    }
    #endregion

    #region Get Disable Projectile
    public GameObject GetDisableRectangularProjectile() 
    { 
        for (int i = 0; i < _rectangularProjectileList.Count; i++)
        {
            if (_rectangularProjectileList[i].activeSelf == false)
            {
                return _rectangularProjectileList[i];
            }
        }
        // If there is no projectile available
        InstantiationOfRectangularProjectiles(1);

        return _rectangularProjectileList[_rectangularProjectileList.Count - 1];
    }

    public GameObject GetDisableCircularProjectile()
    {
        for (int i = 0; i < _circularProjectileList.Count; i++)
        {
            if (_circularProjectileList[i].activeSelf == false)
            {
                return _circularProjectileList[i];
            }
        }
        // If there is no projectile available
        InstantiationOfCircularProjectiles(1);

        return _circularProjectileList[_circularProjectileList.Count - 1];
    }
    #endregion
    #endregion
}