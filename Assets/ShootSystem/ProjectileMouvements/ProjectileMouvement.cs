using UnityEngine;

public class ProjectileMouvement : MonoBehaviour
{
    #region Variables
    Vector2 _projectileDirection;
    float _projectileSpeed;
    #endregion

    #region Methods
    public void RecieveProjectileInfo(Vector2 projectileDirection, float projectileSpeed)
    {
        _projectileDirection = projectileDirection;
        _projectileSpeed = projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile in a certain direction at a certain speed over time
        transform.Translate(_projectileDirection * _projectileSpeed * Time.deltaTime, Space.World);
    }
    #endregion

}