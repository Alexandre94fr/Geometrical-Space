using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundsManager : MonoBehaviour
{
    #region Variables
    public static SoundsManager Instance;

    // References
    [Header("References")]
    [SerializeField] AudioSource _musicsPlayerAudioSource;
    [SerializeField] AudioSource _SFXPlayerAudioSource;

    SettingsManager _settingsManager;

    // Getters Setters
    public AudioSource MusicsPlayerAudioSource { get { return _musicsPlayerAudioSource; } }
    public AudioSource SFXPlayerAudioSource { get { return _SFXPlayerAudioSource; } }

    [Header("Sounds")]
    // Musics
    [SerializeField] AllMusics _allMusics;
    // SFX
    [SerializeField] AllSFX _allSFX;

    #region Enum
    public enum TypesOfMusics
    {
        // Menu
        MainMenu,

        // Cinematics
        StartCinematic,
        VictoryCinematic,
        LoseCinematic,

        // In-Game
        LevelThemes,
        BossTheme,
    }

    public enum TypesOfSFX
    {
        #region Players's actions
        PlayerMoves,
        #endregion

        #region Players's moves
        PlayerShotting,
        #endregion

        #region Power Up
        PowerUpTaken,
        LastPowerUpReached,
        #endregion

        #region Player Damaged
        PlayerHitten,
        PlayerDestroyed,
        #endregion

        #region Enemies's actions
        EnemyMoves,
        #endregion

        #region Enemies's moves
        EnemyShotting,
        EnemySpawningOtherEnemies,
        #endregion

        #region Enemy Damaged
        EnemyHitten,
        EnemyDestroyed,
        #endregion

        #region Environment
        // NULL
        #endregion

        #region UIs
        HoverButton,
        ButtonPressed,
        #endregion
    }
    #endregion

    #region Structs
    [Serializable]
    struct AllMusics
    {
        [Header("Menus :")]
        public List<AudioClip> _mainMenu;

        [Header("Cinematics :")]
        public List<AudioClip> _startCinematic;
        public List<AudioClip> _victoryCinematic;
        public List<AudioClip> _loseCinematic;

        [Header("In-game")]
        public List<AudioClip> _levelThemes;
        public List<AudioClip> _bossTheme;
    }

    [Serializable]
    struct AllSFX
    {
        #region Player's actions
        [Header("Player actions :")]
        public AudioClip[] _playerMoves;
        #endregion

        #region Players's moves
        [Header("Players's moves :")]
        public AudioClip[] _playerShotting;
        #endregion

        #region Power up
        [Header("Power up :")]
        public AudioClip[] _powerUpTaken;
        public AudioClip[] _lastPowerUpReached;
        #endregion

        #region Players damaged
        [Header("Players damaged :")]
        public AudioClip[] _playerHitten;
        public AudioClip[] _playerDestroyed;
        #endregion

        #region Enemies's actions
        [Header("Enemies's actions :")]
        public AudioClip[] _enemyMoves;
        #endregion

        #region Enemies's moves
        [Header("Enemies's moves :")]
        public AudioClip[] _enemyShotting;
        public AudioClip[] _enemySpawningOtherEnemies;
        #endregion

        #region Enemy damaged
        [Header("Enemy damaged :")]
        public AudioClip[] _enemyHitten;
        public AudioClip[] _enemyDestroyed;
        #endregion

        #region Environment
        //[Header("Environment :")]
        #endregion

        #region UIs
        [Header("UIs :")]
        public AudioClip[] _hoverButton;
        public AudioClip[] _buttonPressed;
        #endregion
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
        else
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    void Start()
    {
        _settingsManager = SettingsManager.Instance;
    }

    #region Music
    /// <summary> Return a random AudioClip of the type of Music wanted </summary>
    public List<AudioClip> ReturnMusic(TypesOfMusics typesOfMusics)
    {
        // NOTE : If you are here because of a Unity error (not in default) that's surely because :
        // The music is not define. Go in SoundManager GameObject to define it in the Inspector. 

        switch (typesOfMusics)
        {
            #region Menus
            case TypesOfMusics.MainMenu:
                return _allMusics._mainMenu;
            #endregion

            #region Cinematics
            case TypesOfMusics.StartCinematic:
                return _allMusics._startCinematic;

            case TypesOfMusics.VictoryCinematic:
                return _allMusics._victoryCinematic;

            case TypesOfMusics.LoseCinematic:
                return _allMusics._loseCinematic;
            #endregion

            #region In-game
            case TypesOfMusics.LevelThemes:
                return _allMusics._levelThemes;

            case TypesOfMusics.BossTheme:
                return _allMusics._bossTheme;
            #endregion

            default:
                Debug.LogError($"The type of SFX {typesOfMusics} is not planned in the switch statement.");
                return null;
        }
    }

    /// <summary> Randomize the music list given and play it endlessly /!\ It's a Coroutine /!\ </summary>
    public IEnumerator PlayMusicEndlessly(TypesOfMusics typesOfMusics, float musicVolume = 1)
    {
        List<AudioClip> musicList = ReturnMusic(typesOfMusics);

        while (true)
        {
            // Generation of a random number
            System.Random _randomNumber = new();

            // Shuffling the musicList
            for (int i = musicList.Count - 1; i > 0; i--)
            {
                // Get a random emplacement in the list
                int randomIndex = _randomNumber.Next(0, i + 1);

                // Change the position of the music into a random one in the list without making a temporary variable
                (musicList[randomIndex], musicList[i]) = (musicList[i], musicList[randomIndex]);
            }

            // Playing the music list entierely
            for (int i = 0; i < musicList.Count; i++)
            {
                _musicsPlayerAudioSource.clip = musicList[i];

                _musicsPlayerAudioSource.Play();

                _musicsPlayerAudioSource.volume = musicVolume * _settingsManager.MainVolume * _settingsManager.MusicVolume;

                yield return new WaitForSecondsRealtime(musicList[i].length);
            }
        }
    }
    
    public void StopMusic()
    {
        _musicsPlayerAudioSource.Stop();
    }
    #endregion

    #region SFX
    /// <summary> Return a random AudioClip of the type of SFX wanted </summary>
    public AudioClip ReturnSFX(TypesOfSFX typeOfSFX)
    {
        // NOTE : If you are here because of a Unity error (not in default) that's surely because :
        // The SFX is not define. Go in SoundManager GameObject to define it in the Inspector. 

        switch (typeOfSFX)
        {
            #region Players's actions
            case TypesOfSFX.PlayerMoves:
                return _allSFX._playerMoves[Random.Range(0, _allSFX._playerMoves.Length)];
            #endregion

            #region Players's moves
            case TypesOfSFX.PlayerShotting:
                return _allSFX._playerShotting[Random.Range(0, _allSFX._playerShotting.Length)];
            #endregion

            #region Power up
            case TypesOfSFX.PowerUpTaken:
                return _allSFX._powerUpTaken[Random.Range(0, _allSFX._powerUpTaken.Length - 1)];

            case TypesOfSFX.LastPowerUpReached:
                return _allSFX._lastPowerUpReached[Random.Range(0, _allSFX._lastPowerUpReached.Length - 1)];
            #endregion

            #region Player damaged
            case TypesOfSFX.PlayerHitten:
                return _allSFX._playerHitten[Random.Range(0, _allSFX._playerHitten.Length)];

            case TypesOfSFX.PlayerDestroyed:
                return _allSFX._playerDestroyed[Random.Range(0, _allSFX._playerDestroyed.Length)];
            #endregion

            #region Enemies's actions
            case TypesOfSFX.EnemyMoves:
                return _allSFX._enemyMoves[Random.Range(0, _allSFX._enemyMoves.Length)];
            #endregion

            #region Enemies's moves
            case TypesOfSFX.EnemyShotting:
                return _allSFX._enemyShotting[Random.Range(0, _allSFX._enemyShotting.Length)];

            case TypesOfSFX.EnemySpawningOtherEnemies:
                return _allSFX._enemySpawningOtherEnemies[Random.Range(0, _allSFX._enemySpawningOtherEnemies.Length)];
            #endregion

            #region Enemy damaged
            case TypesOfSFX.EnemyHitten:
                return _allSFX._enemyHitten[Random.Range(0, _allSFX._enemyHitten.Length)];

            case TypesOfSFX.EnemyDestroyed:
                return _allSFX._enemyDestroyed[Random.Range(0, _allSFX._enemyDestroyed.Length)];
            #endregion

            #region Environment
            // NULL
            #endregion

            #region UIs
            case TypesOfSFX.HoverButton:
                return _allSFX._hoverButton[Random.Range(0, _allSFX._hoverButton.Length)];

            case TypesOfSFX.ButtonPressed:
                return _allSFX._buttonPressed[Random.Range(0, _allSFX._buttonPressed.Length)];
            #endregion

            default:
                Debug.LogError($"The type of SFX {typeOfSFX} is not planned in the switch statement.");
                return null;
        }
    }

    /// <summary> Play a random SFX of the type of SFX you wanted </summary>
    public void PlaySFX(TypesOfSFX typesOfSFX, float SFXvolume = 1)
    {
        _SFXPlayerAudioSource.PlayOneShot(ReturnSFX(typesOfSFX), SFXvolume * _settingsManager.MainVolume * _settingsManager.SFXVolume);
    }
    #endregion
    #endregion
}