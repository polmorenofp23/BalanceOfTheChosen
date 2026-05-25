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
    public void LoadFirstLevel()
    {
        currentLevel = 1;
        UpdateNextLevelVar();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1"); // , LoadSceneMode.Additive para añadir el contenido de la escena sin eliminar lo anterior
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCustomScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadCustomLevel (string levelName)
    {
        // Si el nombre sigue el patrón "LevelN", intentamos actualizar currentLevel
        if (levelName.StartsWith("Level"))
        {
            string num = levelName.Substring(5);
            int parsed;
            if (int.TryParse(num, out parsed))
            {
                currentLevel = Mathf.Clamp(parsed, 1, maxLevel);
                UpdateNextLevelVar();
            }
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
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
        }
        else
        {
            Debug.Log("Jugador ha completado el nivel final.");
            // Aquí podrías cargar una escena de créditos o menú final
        }
    }

    // Forzar un nivel concreto por número (1..6)
    public void LoadLevelByNumber(int levelNumber)
    {
        currentLevel = Mathf.Clamp(levelNumber, 1, maxLevel);
        UpdateNextLevelVar();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level" + currentLevel);
    }

    // --- Pausa y Game Over --- ADDITIVE SCENES MANAGEMENT ---
    // Muestra el menú de pausa cargando la escena correspondiente en modo aditivo y detiene el tiempo
    public void ShowPauseMenu()
    {
        Debug.Log("SceneManage: ShowPauseMenu() called");
        if (SceneManager.GetSceneByName("PauseMenu").isLoaded)
        {
            Debug.Log("SceneManage: PauseMenu is already loaded, skipping duplicate load");
            Time.timeScale = 0f;
            return;
        }
        Time.timeScale = 0f;
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        Debug.Log("SceneManage: PauseMenu loaded additively");
    }

    // Oculta el menú de pausa y reanuda el juego
    public void HidePauseMenu()
    {
        Debug.Log("SceneManage: HidePauseMenu() called");
        Time.timeScale = 1f;
        if (!SceneManager.GetSceneByName("PauseMenu").isLoaded)
        {
            Debug.Log("SceneManage: PauseMenu was not loaded, nothing to unload");
            return;
        }
        SceneManager.UnloadSceneAsync("PauseMenu");
        Debug.Log("SceneManage: PauseMenu unload requested");
    }

    // Muestra el menú de game over y detiene el tiempo
    public void ShowGameOver()
    {
        Debug.Log("SceneManage: ShowGameOver() called");
        if (SceneManager.GetSceneByName("GameOver").isLoaded)
        {
            Debug.Log("SceneManage: GameOver is already loaded, skipping duplicate load");
            Time.timeScale = 0f;
            return;
        }
        Time.timeScale = 0f;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        Debug.Log("SceneManage: GameOver loaded additively");
    }

    // Reinicia el estado de tiempo si es necesario (por ejemplo al reiniciar desde game over)
    public void HideGameOver()
    {
        Debug.Log("SceneManage: HideGameOver() called");
        Time.timeScale = 1f;
        if (!SceneManager.GetSceneByName("GameOver").isLoaded)
        {
            Debug.Log("SceneManage: GameOver was not loaded, nothing to unload");
            return;
        }
        SceneManager.UnloadSceneAsync("GameOver");
        Debug.Log("SceneManage: GameOver unload requested");
    }

    public void ShowCustomMenu (string menuName)
    {
        if (SceneManager.GetSceneByName(menuName).isLoaded)
        {
            Debug.Log($"SceneManage: {menuName} is already loaded, skipping duplicate load");
            return;
        }
        SceneManager.LoadScene(menuName, LoadSceneMode.Additive);
    }

     public void HideCustomMenu (string menuName)
    {
        if (!SceneManager.GetSceneByName(menuName).isLoaded)
        {
            Debug.Log($"SceneManage: {menuName} was not loaded, nothing to unload");
            return;
        }
        SceneManager.UnloadSceneAsync(menuName);
    }

    private void OnValidate()
    {
        currentLevel = Mathf.Clamp(currentLevel, 1, maxLevel);
        nextLevel = Mathf.Clamp(nextLevel, 1, maxLevel);
    }
}