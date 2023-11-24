using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvements : MonoBehaviour
{
    #region Variables
    public static PlayerMouvements Instance;

    [SerializeField] float _mouvementSpeed;

    Vector2 _mouvementDirection;
    #endregion

    #region Methods
    private void Awake()
    {
        // Setting a reference to this script
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void RecieveMouvementInputValues(Vector2 direction)
    {
        _mouvementDirection = direction;
    }

    void Move()
    {
        transform.Translate(_mouvementDirection * _mouvementSpeed);
    }
    #endregion
}