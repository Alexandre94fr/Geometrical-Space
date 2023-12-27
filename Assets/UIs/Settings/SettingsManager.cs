using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    #region Variables
    public static SettingsManager Instance;

    //[Header("Graphism references :")]

    [Header("Slider references :")]
    [SerializeField] Slider _mainSoundVolumeSlider;
    [SerializeField] Slider _musicsVolumeSlider;
    [SerializeField] Slider _SFXVolumeSlider;

    // Sound variables
    float _mainVolume = 1f;
    float _musicVolume = 1f;
    float _SFXVolume = 1f;

    public float MainVolume { get { return _mainVolume; }}
    public float MusicVolume { get { return _musicVolume; }}
    public float SFXVolume { get { return _SFXVolume; }}
    #endregion

    #region 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // To not see the Settings UI in the scene we gave -1 in the Settings UI parent OrderInLayer value, so now we change it to see it
            transform.parent.GetComponent<Canvas>().sortingOrder = 1;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(transform.parent.parent.gameObject);
        }

        DontDestroyOnLoad(transform.parent.parent);

        // Link volume sliders to their respective functions
        _mainSoundVolumeSlider.onValueChanged.AddListener(delegate {OnMainSoundVolumeChanged();} );
        _musicsVolumeSlider.onValueChanged.AddListener(delegate {OnMusicSoundVolumeChanged();} );
        _SFXVolumeSlider.onValueChanged.AddListener(delegate {OnSFXSoundVolumeChanged();} );
    }

    #region Audio
    void OnMainSoundVolumeChanged()
    {
        _mainVolume = _mainSoundVolumeSlider.value;
    }

    void OnMusicSoundVolumeChanged()
    {
        _musicVolume = _musicsVolumeSlider.value;
    }

    void OnSFXSoundVolumeChanged()
    {
        _SFXVolume = _SFXVolumeSlider.value;
    }
    #endregion
    #endregion
}