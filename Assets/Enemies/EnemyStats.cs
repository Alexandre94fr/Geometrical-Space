using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObject/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    #region Based stats
    [Header("Basic Stats")]
    public EnemyListEnum enemyType;
    public Sprite enemySprite;
    public Color spriteColor;
    public Vector2 spriteSize = new(1, 1);
    public int enemyHP;
    public int givenScoreWhenDestroyed;
    public int enemyDamageWhenCollide = 1;
    #endregion

    #region Enemy Mouvement
    [Header("Enemy Mouvement")]
    public Vector2 baseEnemyDirection;
    public float enemySpeed;
    #endregion

    #region Enemy Shooting
    [Header("Enemy Shooting")]
    public bool isShooting;
    [ShowCondition("isShooting")]
    public ShotStats.ProjectileType shotType;
    #endregion

    #region Enums
    public enum EnemyListEnum
    {
        Minion,
        Square,
        Pantagon,
        Hexagon,
        Octogon
    }
    #endregion
}