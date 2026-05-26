using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class MenuUIActions : MonoBehaviour
{
    [Header("Referencias de Texto Dinámico (Opcional)")]
    [Tooltip("Arrastra aquí el texto de Nivel Completado. Déjalo vacío en otros menús.")]
    public TextMeshProUGUI textoNivelCompletado;

    private void Start()
    {
        // Solo actualizará el texto si le hemos asignado un elemento en el Inspector
        if (textoNivelCompletado != null && SceneManage.Instance != null)
        {
            textoNivelCompletado.text = "Nivel " + SceneManage.Instance.CurrentLevel + " completado!";
        }
    }

    public void StartGame()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.LoadCustomLevel(1);
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void LoadCustomScene(string sceneName)
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.LoadCustomScene(sceneName);
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void LoadCustomLevel(int levelNumber)
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.LoadCustomLevel(levelNumber);
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void ReloadCurrentLevel()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.ReloadCurrentLevel();
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void LoadNextLevel()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.LoadNextLevel();
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void ContinueFromLevelIntro()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.HideCurrentLevelIntro();
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void ShowCustomScene(string sceneName)
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.ShowCustomScene(sceneName);
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }

    public void HideCustomScene(string sceneName)
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.HideCustomScene(sceneName);
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
            SceneManage.Instance.ShowCustomScene("PauseMenu");
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
            SceneManage.Instance.HideCustomScene("PauseMenu");
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
            SceneManage.Instance.ShowCustomScene("GameOver");
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
            SceneManage.Instance.HideCustomScene("GameOver");
        }
        else
        {
            Debug.LogError("MenuUIActions: SceneManage.Instance is null");
        }
    }
}