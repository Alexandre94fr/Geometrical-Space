using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnerManager : MonoBehaviour
{
    #region Variables
    [Header("Player Spawn Parameters :")]
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] GameObject _playerParent;
    [SerializeField] Vector2 _playerSpawnPosition;
    [SerializeField] ShotStats _playerShotType;
    [SerializeField] GameObject _shotsParents;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPlayer(_playerPrefab, _playerParent, _playerSpawnPosition, _playerShotType, _shotsParents));
    }

    IEnumerator SpawnPlayer(
        GameObject playerPrefab,
        GameObject playersParent,
        Vector2 spawnPosition,
        ShotStats playerShotType,
        GameObject shotsParent)
    {
        // To avoid error, we wait a little time, to let the variable being set
        yield return new WaitForSeconds(0.01f);

        GameObject player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity, playersParent.transform);

        #region Settings up projectile's caracteristics
        // Setting name and tag of the projectile to his correspondant values
        player.name = playerPrefab.name;
        player.tag = "Player";

        // Transfert data to the shooting system
        player.GetComponent<ShootingSystem>().ReceiveShootingSystemStats(shotsParent, playerShotType);

        /// Will be used of there is a local multiplier

        // Transfert projectile data to the projectile -> Make the projectile move into a precise direction, and can deal damage
        //player.GetComponent<PlayerMouvements>().MouvementSpeed = 0.1f;
        //enemy.GetComponent<HealthManager>().RecieveEnemyHealthStats(playerShotType.enemyHP);

        // Assignation of a sprite, sprite size, and sprite color to the projectile
        //SpriteRenderer shotSpriteRenderer = enemy.GetComponent<SpriteRenderer>();
        //shotSpriteRenderer.sprite = enemyStats.enemySprite;
        //enemy.transform.localScale = enemyStats.spriteSize;
        //shotSpriteRenderer.color = enemyStats.spriteColor;
        #endregion
    }
    #endregion
}
