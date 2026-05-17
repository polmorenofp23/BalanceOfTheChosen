using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // FixedUpdate is called once per frame (50 times per second)
    void FixedUpdate()
    {
        
    }

    // Loads the first level
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1"); // , LoadSceneMode.Additive para añadir el contenido de la escena sin eliminar lo anterior
    }

    public void LoadCustomScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadCustomLevel (string levelName)
    {
        SceneManager.LoadScene(levelName);
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