using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToNextScene : MonoBehaviour
{
    void Update()
    {
        if (Time.time > 3f)
        {
            SceneManager.LoadScene(1);
        }
    }
}
