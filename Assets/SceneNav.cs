using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainRoom");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
