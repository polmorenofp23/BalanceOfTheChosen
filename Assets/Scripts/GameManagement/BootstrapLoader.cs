using UnityEngine;

public class BootstrapLoader : MonoBehaviour
{
    private void Start()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.LoadMainMenu();
        }
        else
        {
            Debug.LogError("BootstrapLoader: SceneManage.Instance is null");
        }
    }
}
