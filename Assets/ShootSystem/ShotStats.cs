using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotData", menuName = "ScriptableObject/ShotData")]
public class ShotStats : ScriptableObject
{
    // TO DO : Faire que quand un boulean Type of shot est sélectionné les autres sont désactivés,
    // et seul les variables du type correspond sont affichés

    // TO DO : Bonus : Faire que quand le dev set une valeur à totalNomberOfProjectile,
    // la taille projectilesSpriteList s'adapte

    [Header("Type of shot")]
    public bool singleShot = true;
    public bool multiShot;
    public bool spawningShot;

    [Header("Shot Stats")]
    public Sprite projectileSprite;
    public Color spriteColor;
    public int projectileDamage;
    public float projectileSpeed;
    public float projectileTimeBetweenShot;

    [Header("Multiple Shot Stats")]
    public int totalNomberOfProjectile;

    [Tooltip("You have to assign a sprite for each projectile")]
    public List<Sprite> projectilesSpriteList = new();

    public int angledDifferenceBetweenProjectile;

    [Header("Spawning Shot Stats")]
    public EnemyList.EnemyListEnum enemyList;
}