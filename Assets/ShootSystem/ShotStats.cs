using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotStats", menuName = "ScriptableObject/ShotStats")]
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
    public Vector2 baseProjectileDirection = new(0, -90);
    #endregion

    #region Single Shot Stats
    [Header("Single Shot Stats")]
    [ShowCondition("singleShot")]
        public ProjectileTag projectileTag;
    [ShowCondition("singleShot")]
        public ProjectileSpriteType projectileSpriteType;
    [ShowCondition("singleShot")]
        public Sprite projectileSprite;
    [ShowCondition("singleShot")]
        public Color spriteColor;
    [ShowCondition("singleShot")]
        public Vector2 spriteSize = new(1, 1);
    [ShowCondition("singleShot")]
        public int projectileDamage;
    #endregion

    #region Multiple Shot Stats
    [Header("Multiple Shot Stats")]
    [ShowCondition("multiShot")]
        public int angledDifferenceBetweenProjectile;
    
        public List<ProjectilesStats> projectilesStats;

    [Serializable]
    public struct ProjectilesStats
    {
         public ProjectileTag projectileTag;
         public ProjectileSpriteType projectileSpriteType;
         public Vector2 projectileSize;
         public Sprite projectileSprite;
         public Color spriteColor;
         public int projectileDamage;
    }
    #endregion

    #region Spawning Shot Stats
    [Header("Spawning Shot Stats")]
    [ShowCondition("spawningShot")]
        public int angledDifferenceBetweenEnemies;
    [ShowCondition("spawningShot")]
        public List<EnemyStats> enemyList;

    #endregion

    #region Enums
    public enum ProjectileType
    {
        PlayerSingleShot,
        EnemySingleShot,
        FrontAngledTripleShot,
        MinionsShot,
        EnemyMultiShot,
    }

    public enum ProjectileSpriteType
    {
        Rectangular,
        Circular,
    }

    public enum ProjectileTag
    {
        PlayerProjectile,
        EnemyProjectile,
    }
    #endregion

    #if UNITY_EDITOR
    private void OnValidate()
    {
        #region Show and Hide attributes
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
        #endregion
    }
    #endif
}