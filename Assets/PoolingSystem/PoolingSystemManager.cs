using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
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
        Debug.Assert(projectileNumber > 0, "The value 'projectileNumber' can't be under, or equal to 0");

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
        Debug.Assert(projectileNumber > 0, "The value 'projectileNumber' can't be under, or equal to 0");

        // Instatiation and adding all the projectiles to the projectile list
        for (int i = 0; i < projectileNumber; i++)
        {
            GameObject projectile = Instantiate(_circularShotPrefab, transform.position, Quaternion.identity, _allShotsParent.transform);

            projectile.SetActive(false);

            _circularProjectileList.Add(projectile);
        }
    }
    #endregion

    #region Get Disable Projectiles
    public List<GameObject> GetDisableRectangularProjectile(int number) 
    {
        Debug.Assert(number > 0, "The value 'number' can't be under, or equal to 0");

        List<GameObject> projectileList = new();

        // Loop throw the Rectangular projectile list
        for (int i = 0; i < _rectangularProjectileList.Count; i++)
        {
            if (_rectangularProjectileList[i].activeSelf == false)
            {
                projectileList.Add(_rectangularProjectileList[i]);
            }

            if (projectileList.Count == number)
            {
                return projectileList;
            }
        }

        // If there are not enought projectiles available :
        int numberOfProjectileMissing = number - projectileList.Count;

        InstantiationOfRectangularProjectiles(numberOfProjectileMissing);

        // We add the new instantiated projectile(s) in our local list
        for (int i = 0; i < numberOfProjectileMissing; i++)
        {
            projectileList.Add(_rectangularProjectileList[_rectangularProjectileList.Count - numberOfProjectileMissing - i]);
        }

        return projectileList;
    }

    public List<GameObject> GetDisableCircularProjectile(int number)
    {
        Debug.Assert(number > 0, "The value 'number' can't be under, or equal to 0");

        List<GameObject> projectileList = new();

        // Loop throw the Circular projectile list
        for (int i = 0; i < _circularProjectileList.Count; i++)
        {
            if (_circularProjectileList[i].activeSelf == false)
            {
                projectileList.Add(_circularProjectileList[i]);
            }

            if (projectileList.Count == number)
            {
                return projectileList;
            }
        }

        // If there are not enought projectiles available :
        int numberOfProjectileMissing = number - projectileList.Count;

        InstantiationOfCircularProjectiles(numberOfProjectileMissing);

        // We add the new instantiated projectile(s) in our local list
        for (int i = 0; i < numberOfProjectileMissing; i++)
        {
            projectileList.Add(_circularProjectileList[_circularProjectileList.Count - numberOfProjectileMissing + i]);
        }

        return projectileList;
    }
    #endregion
    #endregion
}