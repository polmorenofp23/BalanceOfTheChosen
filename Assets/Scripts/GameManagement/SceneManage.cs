using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage Instance { get; private set; }

    // Nivel actual del jugador (1..6), visible y editable en el Inspector
    [SerializeField]
    private int currentLevel = 1;
    // Variable global que indica el nivel al que debemos mandar al jugador cuando pase de nivel
    [SerializeField]
    private int nextLevel = 2;
    // Nivel máximo del juego
    public const int maxLevel = 6;

    public int CurrentLevel => currentLevel;
    public int NextLevel => nextLevel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        currentLevel = Mathf.Clamp(currentLevel, 1, maxLevel);
        UpdateNextLevelVar();
    }


    // Ajusta 'nextLevel' según el nivel actual
    private void UpdateNextLevelVar()
    {
        nextLevel = Mathf.Clamp(currentLevel + 1, 1, maxLevel);
    }

    // FixedUpdate is called once per frame (50 times per second)
    void FixedUpdate()
    {
        
    }

    // Loads the first level
    public void LoadCustomScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("SceneManage: LoadCustomScene called with an empty scene name");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadCustomLevel(int levelNumber)
    {
        currentLevel = Mathf.Clamp(levelNumber, 1, maxLevel);
        UpdateNextLevelVar();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level" + currentLevel);
        ShowLevelIntroForCurrentLevel();
    }

    // Carga el siguiente nivel si existe (usa la variable currentLevel)
    public void LoadNextLevel()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel = Mathf.Clamp(currentLevel + 1, 1, maxLevel);
            UpdateNextLevelVar();
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level" + currentLevel);
            ShowLevelIntroForCurrentLevel();
        }
        else
        {
            Debug.Log("Jugador ha completado el nivel final.");
            // Aquí podrías cargar una escena de créditos o menú final
        }
    }

    public void ReloadCurrentLevel()
    {
        currentLevel = Mathf.Clamp(currentLevel, 1, maxLevel);
        UpdateNextLevelVar();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level" + currentLevel);
        ShowLevelIntroForCurrentLevel();
    }

    public void HideCurrentLevelIntro()
    {
        HideCustomScene(GetLevelIntroSceneName(currentLevel));
    }


    // --- Pausa y Game Over --- ADDITIVE SCENES MANAGEMENT ---
    // Carga una escena de forma aditiva, manteniendo la escena actual visible por detrás.
    public void ShowCustomScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("SceneManage: ShowCustomScene called with an empty scene name");
            return;
        }

        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            Debug.Log($"SceneManage: {sceneName} is already loaded, skipping duplicate load");
            return;
        }

        Time.timeScale = 0f;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        Debug.Log($"SceneManage: {sceneName} loaded additively");
    }

    // Descarga una escena cargada de forma aditiva y reanuda el juego.
    public void HideCustomScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("SceneManage: HideCustomScene called with an empty scene name");
            return;
        }

        Time.timeScale = 1f;
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            Debug.Log($"SceneManage: {sceneName} was not loaded, nothing to unload");
            return;
        }
        SceneManager.UnloadSceneAsync(sceneName);
        Debug.Log($"SceneManage: {sceneName} unload requested");
    }

    private string GetLevelIntroSceneName(int levelNumber)
    {
        int clampedLevel = Mathf.Clamp(levelNumber, 1, maxLevel);
        return $"Level{clampedLevel}Intro";
    }

    private void ShowLevelIntroForCurrentLevel()
    {
        ShowCustomScene(GetLevelIntroSceneName(currentLevel));
    }


    private void OnValidate()
    {
        currentLevel = Mathf.Clamp(currentLevel, 1, maxLevel);
        nextLevel = Mathf.Clamp(nextLevel, 1, maxLevel);
    }
}