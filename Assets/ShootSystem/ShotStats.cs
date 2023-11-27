using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotData", menuName = "ScriptableObject/ShotData")]
public class ShotStats : ScriptableObject
{
    #region Variables
    [HideInInspector] public bool _hideSingleShotEnable;
    [HideInInspector] public bool _hideMultiShotEnable;
    [HideInInspector] public bool _hideSpawingShotEnable;
    #endregion

    #region Types of shot
    [Header("Types of shot")]
    [HideCondition("_hideSingleShotEnable")]
        public bool singleShot;
    [HideCondition("_hideMultiShotEnable")]
        public bool multiShot;
    [HideCondition("_hideSpawingShotEnable")]
        public bool spawningShot;
    #endregion

    #region Based stats
    [Header("Basic Stats")]
    public float projectileSpeed;
    public float projectileTimeBetweenShot;
    #endregion

    #region Single Shot Stats
    [Header("Single Shot Stats")]
    [ShowCondition("singleShot")]
        public Sprite projectileSprite;
    [ShowCondition("singleShot")]
        public Color spriteColor;
    #endregion

    #region Multiple Shot Stats
    [Header("Multiple Shot Stats")]
    [ShowCondition("multiShot")]
        public Color allSpritesColor;
    [ShowCondition("multiShot")]
        public int totalNomberOfProjectile;
    [ShowCondition("multiShot")]
        public int angledDifferenceBetweenProjectile;
    #endregion

    #region Spawning Shot Stats
    [Header("Spawning Shot Stats")]
    [ShowCondition("spawningShot")]
        public EnemyList.EnemyListEnum enemyList;
    #endregion

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (singleShot)
        {
            _hideSingleShotEnable = false;
            _hideMultiShotEnable = true;
            _hideSpawingShotEnable = true;

            multiShot = false;
            spawningShot = false;
        }
        else if (multiShot)
        {
            _hideSingleShotEnable = true;
            _hideMultiShotEnable = false;
            _hideSpawingShotEnable = true;

            singleShot = false;
            spawningShot = false;
        }
        else if (spawningShot)
        {
            _hideSingleShotEnable = true;
            _hideMultiShotEnable = true;
            _hideSpawingShotEnable = false;

            singleShot = false;
            multiShot = false;
        }
        else
        {
            _hideSingleShotEnable = false;
            _hideMultiShotEnable = false;
            _hideSpawingShotEnable = false;
        }
    }
    #endif
}