using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject _shotPrefab;
    [SerializeField] ShotStats _shotStats;
    [SerializeField] GameObject _allShotsParent;
    #endregion

    #region Methods

    private void Start()
    {
        StartCoroutine(ShootEndlessly());
    }

    IEnumerator ShootEndlessly()
    {
        while (gameObject.activeSelf)
        {
            if (_shotStats.singleShot)
            {
                InstantiateShot(_shotStats.projectileSpeed, _shotStats.projectileSprite, _shotStats.spriteColor);
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
    void InstantiateShot(float projectileSpeed, Sprite projectileSprite, Color projectileColor)
    {
        Vector2 startPosition = new(transform.position.x, transform.position.y + GetComponent<SpriteRenderer>().sprite.bounds.size.y);    

        GameObject shot = Instantiate(_shotPrefab, startPosition, Quaternion.identity, _allShotsParent.transform);

        // TO DO : Faire que " Vector.up " 
        shot.GetComponent<ProjectileMouvement>().RecieveProjectileInfo(Vector2.up, projectileSpeed);

        SpriteRenderer shotSpriteRenderer = shot.GetComponent<SpriteRenderer>();
        shotSpriteRenderer.sprite = projectileSprite;
        shotSpriteRenderer.color = projectileColor;
    }
    #endregion
}