using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    #region Methods
    public void ChangeScene(string _sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(_sceneName);
    }
    public void Reload()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void CursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
    }
    #endregion
}