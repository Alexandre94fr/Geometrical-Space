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
                //PoolingSystemManager.Instance.InstantiateShot(_shotStats);
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
    #endregion
}