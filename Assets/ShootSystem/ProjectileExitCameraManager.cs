using UnityEngine;

public class ProjectileExitCameraManager : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
    }
}