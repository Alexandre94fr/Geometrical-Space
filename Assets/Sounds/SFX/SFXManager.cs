using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SFXManager : MonoBehaviour
{
    #region Variables
    public static SFXManager Instance;

    [Header("Shooting SFX :")]
    [SerializeField] List<AudioClip> _playerShootingSFX;
    [SerializeField] List<AudioClip> _enemyShootingSFX;

    [Header("Player hit SFX :")]
    [SerializeField] AudioClip _playerHit;
    [SerializeField] AudioClip _playerDestroyed;

    [Header("Power up SFX :")]
    [SerializeField] AudioClip _powerUpTaken;
    [SerializeField] AudioClip _lastPowerUpReached;

    #region Enum
    public enum TypesOfSFX
    {
        PlayerShotting,
        EnemyShotting,
        PlayerHit,
        PlayerDestroyed,
        PowerUpTaken,
        LastPowerUpReached
    }
    #endregion
    #endregion

    #region Methods
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Debug.Assert(!(_playerShootingSFX == null), $"Warning ! The list {_playerShootingSFX} is empty.");
        Debug.Assert(!(_enemyShootingSFX == null), $"Warning ! The list {_enemyShootingSFX} is empty.");
    }

    /// <summary> Take a random SFX in the PlayerShootingSFX list, and play it </summary>
    public AudioClip ReturnSFX(TypesOfSFX typeOfSFX)
    {
        // Return a random SFX in the list
        switch (typeOfSFX)
        {
            case TypesOfSFX.PlayerShotting:
                return _playerShootingSFX[Random.Range(0, _playerShootingSFX.Count - 1)];

            case TypesOfSFX.EnemyShotting:
                return _enemyShootingSFX[Random.Range(0, _enemyShootingSFX.Count - 1)];

            case TypesOfSFX.PlayerHit:
                return _playerHit;

            case TypesOfSFX.PlayerDestroyed:
                return _playerDestroyed;

            case TypesOfSFX.PowerUpTaken:
                return _powerUpTaken;

            case TypesOfSFX.LastPowerUpReached:
                return _lastPowerUpReached;

            default:
                Debug.LogError($"The type of SFX {typeOfSFX} is not planned in the switch statement.");
                return null;
        }
    }
    #endregion
}