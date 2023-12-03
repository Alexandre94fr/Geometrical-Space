using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObject/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    #region Based stats
    [Header("Basic Stats")]
    public GameObject enemyPrefab;
    public Sprite enemySprite;
    public Color spriteColor;
    public Vector2 spriteSize = new(1, 1);
    public int enemyHP;
    public int givenScoreWhenDestroyed;
    public int enemyDamageWhenCollide = 1;
    #endregion

    #region Enemy Mouvement
    [Header("Enemy Mouvement")]
    public bool enemyMoveToTheLastPlayerPosition;
    [ShowCondition("enemyMoveToTheLastPlayerPosition")]
        public bool enemyMoveToThePlayerPosition;
    [HideCondition("enemyMoveToTheLastPlayerPosition")]
        public Vector2 baseEnemyDirection;
    public float enemySpeed;
    #endregion

    #region Enemy Shooting
    [Header("Enemy Shooting")]
    public bool isShooting;
    [ShowCondition("isShooting")]
    public ShotStats shotType;
    #endregion
}