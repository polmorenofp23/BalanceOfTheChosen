using UnityEngine;

public class MenuUIActions : MonoBehaviour
{
    public void StartGame()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.LoadFirstLevel();
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void LoadMainMenu()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.LoadMainMenu();
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void ShowPauseMenu()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.ShowPauseMenu();
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void HidePauseMenu()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.HidePauseMenu();
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void ShowGameOverMenu()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.ShowGameOver();
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void HideGameOverMenu()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.HideGameOver();
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void LoadLevel1()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.LoadLevelByNumber(1);
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void LoadLevel2()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.LoadLevelByNumber(2);
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }
}
