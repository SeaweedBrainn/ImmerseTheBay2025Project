using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerClass : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if(sceneName.Equals("self")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName);
    }
}
