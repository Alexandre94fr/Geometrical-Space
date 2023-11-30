using UnityEngine;

public class ProjectileExitCameraManager : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile") || collision.CompareTag("EnemyProjectile"))
        {
            collision.gameObject.SetActive(false);
        }
        else
        {
            Destroy(collision.gameObject);
        }       
    }
}