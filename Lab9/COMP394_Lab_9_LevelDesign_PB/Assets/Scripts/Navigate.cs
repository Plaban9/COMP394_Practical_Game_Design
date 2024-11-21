using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate : MonoBehaviour
{
    public void OnNavigate(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
