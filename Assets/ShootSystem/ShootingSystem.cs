using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    #region Variables

    #endregion

    #region Methods
    void Update()
    {
        
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            // Cooldown before shooting another time
            yield return new WaitForSeconds(1);
        }
    }
    #endregion
}