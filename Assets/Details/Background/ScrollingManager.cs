using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    #region Variables
    [SerializeField] Renderer _renderer3D;
    [SerializeField] Vector2 _scrollingDirection;
    [SerializeField] Vector2 _scrollingSpeed;
    #endregion

    #region Methods
    // Update is called once per frame
    void Update()
    {
        _renderer3D.material.mainTextureOffset += new Vector2(
            _scrollingDirection.x * _scrollingSpeed.x * Time.deltaTime,
            _scrollingDirection.y * _scrollingSpeed.y * Time.deltaTime
        );
    }
    #endregion
}