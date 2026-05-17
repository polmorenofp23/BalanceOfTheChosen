using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    // Nivel actual del jugador (1..6)
    public static int currentLevel = 1;
    // Variable global 'nextLevel' que indica el nivel al que debemos mandar al jugador cuando pase de nivel
    public static int nextLevel = 2;
    // Nivel máximo del juego
    public const int maxLevel = 6;

    // Ajusta 'nextLevel' según el nivel actual
    private static void UpdateNextLevelVar()
    {
        nextLevel = Mathf.Clamp(currentLevel + 1, 1, maxLevel);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Asegurarnos de que las variables de nivel estén consistentes al iniciar
        currentLevel = Mathf.Clamp(currentLevel, 1, maxLevel);
        UpdateNextLevelVar();
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

    // --- Pausa y Game Over ---
    // Muestra el menú de pausa cargando la escena correspondiente en modo aditivo y detiene el tiempo
    public void ShowPauseMenu(string pauseMenuScene)
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(pauseMenuScene, LoadSceneMode.Additive);
    }

    // Oculta el menú de pausa y reanuda el juego
    public void HidePauseMenu(string pauseMenuScene)
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(pauseMenuScene);
    }

    // Muestra el menú de game over y detiene el tiempo
    public void ShowGameOverMenu(string gameOverScene)
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(gameOverScene, LoadSceneMode.Additive);
    }

    // Reinicia el estado de tiempo si es necesario (por ejemplo al reiniciar desde game over)
    public void ResumeFromGameOver(string gameOverScene)
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(gameOverScene);
    }

    public void ShowCustomMenu (string menuName)
    {
        SceneManager.LoadScene(menuName, LoadSceneMode.Additive);
    }

     public void HideCustomMenu (string menuName)
    {
        SceneManager.UnloadSceneAsync(menuName);
    }
}