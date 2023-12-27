using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    #region Methods
    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ReloadScene()
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