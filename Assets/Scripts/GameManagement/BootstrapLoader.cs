using UnityEngine;

public class BootstrapLoader : MonoBehaviour
{
    private void Start()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.LoadCustomScene("MainMenu");
        }
        else
        {
            Debug.LogError("BootstrapLoader: SceneManage.Instance is null");
        }
    }
}
