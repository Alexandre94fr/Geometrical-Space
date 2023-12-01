using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpawningManager : MonoBehaviour
{
    #region Variables
    [Header("References :")]
    [SerializeField] GameObject _shotsParent;
    [SerializeField] GameObject _enemiesParent;

    [Header("Enemy Wave Parameters :")]
    [SerializeField] GameObject _spawnerPrefab;
    [SerializeField] GameObject _spawnerParent;
    [SerializeField] float _timeBetweenEachWave = 10f;
    public bool _isLevelInfinite;
    [ShowCondition("_isLevelInfinite")]
        [SerializeField] int _loopIntoTheXLastWave;
    [SerializeField] List<EnemiesPerWave> _waveDetails = new();

    [Serializable]
    public struct EnemiesPerWave
    {
        public Vector2 gapBetweenSpawners;
        public List<EnemyStats> enemyStatsList;
    }
    #endregion

    #region Methods
    void Start()
    {
        if (_isLevelInfinite)
        {
            StartCoroutine(InfiniteSpawningLoop(_spawnerPrefab, _spawnerParent, _timeBetweenEachWave, _waveDetails, _shotsParent, _enemiesParent));
        }
        else
        {
            StartCoroutine(FiniteSpawningLoop(_spawnerPrefab, _spawnerParent, _timeBetweenEachWave, _waveDetails, _shotsParent, _enemiesParent));
        }
    }

    IEnumerator InfiniteSpawningLoop(
        GameObject spawnerPrefab,
        GameObject spawnerParent,
        float timeBetweenEachWave,
        List<EnemiesPerWave> waveDetails,
        GameObject shotsParent,
        GameObject enemiesParent)
    {
        // We wait the player to load
        yield return new WaitForSeconds(0.02f);

        int waveNumber = 0;

        // While player is alife
        while (PlayerSpawnerManager.Instance.gameObject != null)
        {
            if (waveNumber == 0) 
            {
                for (int i = 0; i < _waveDetails.Count; i++)
                {
                    waveNumber = i;
                    SpawnNewEnemyWave(spawnerPrefab, spawnerParent, waveDetails[i], shotsParent, enemiesParent);

                    // Wait for the new wave
                    yield return new WaitForSeconds(timeBetweenEachWave);
                }
            }

            // Will loop into the selected waves
            waveNumber -= _loopIntoTheXLastWave;

            for (int i = 0; i < _loopIntoTheXLastWave; i++)
            {
                waveNumber++;

                SpawnNewEnemyWave(spawnerPrefab, spawnerParent, waveDetails[waveNumber], shotsParent, enemiesParent);

                // Wait for the new wave
                yield return new WaitForSeconds(timeBetweenEachWave);
            }
        }
    }

    IEnumerator FiniteSpawningLoop(
    GameObject spawnerPrefab,
    GameObject spawnerParent,
    float timeBetweenEachWave,
    List<EnemiesPerWave> waveDetails,
    GameObject shotsParent,
    GameObject enemiesParent)
    {
        // We wait the player to load
        yield return new WaitForSeconds(0.02f);

        for (int i = 0; i < _waveDetails.Count; i++)
        {
            SpawnNewEnemyWave(spawnerPrefab, spawnerParent, waveDetails[i], shotsParent, enemiesParent);

            // Wait for the new wave
            yield return new WaitForSeconds(timeBetweenEachWave);
        }

        // Player survived
    }

    void SpawnNewEnemyWave(
        GameObject spawnerPrefab,
        GameObject spawnerParent,
        EnemiesPerWave waveDetails,
        GameObject shotsParent,
        GameObject enemiesParent)
    {
        // To optimize
        Vector3 gameObjectPosition = transform.position;
        int numberOfEnemies = waveDetails.enemyStatsList.Count;
        Vector2 gapBetweenSpawners = waveDetails.gapBetweenSpawners;

        Vector2 spawnerPosition = new();

        int extremity = (int)(-numberOfEnemies / 2f);

        // Spawning of the spawners
        for (int i = 0; i < numberOfEnemies; i++) 
        {
            // If numberOfEnemies is not a odd number
            if (numberOfEnemies % 2 != 0)
            {
                spawnerPosition = new(
                    gameObjectPosition.x + (extremity + i) * gapBetweenSpawners.x,
                    gameObjectPosition.y + (extremity + i) * gapBetweenSpawners.y
                );
            }
            else if (numberOfEnemies % 2 == 0)
            {
                //(gapBetweenSpawners.x / 2)
                spawnerPosition = new(
                    gameObjectPosition.x + (gapBetweenSpawners.x / 2) + (extremity + i ) * gapBetweenSpawners.x,
                    gameObjectPosition.y + (gapBetweenSpawners.y / 2) + (extremity + i ) * gapBetweenSpawners.y
                );
            }

            GameObject spawner = Instantiate(spawnerPrefab, spawnerPosition, Quaternion.identity, spawnerParent.transform);

            EnemySpawn enemySpawn = spawner.GetComponent<EnemySpawn>();

            enemySpawn.RecieveInfo(shotsParent, enemiesParent);
            enemySpawn.SpawnEnemy(waveDetails.enemyStatsList[i]);

            Destroy(spawner);
        }
    }

    #if UNITY_EDITOR
    void OnValidate()
    {
        // Limite the _loopIntoTheXLastWave value between 1 and the total number of waves
        if (_loopIntoTheXLastWave > _waveDetails.Count)
        {
            _loopIntoTheXLastWave = _waveDetails.Count;
        }
        if (_loopIntoTheXLastWave < 1)
        {
            _loopIntoTheXLastWave = 1;
        }
    }
    #endif
#endregion
}