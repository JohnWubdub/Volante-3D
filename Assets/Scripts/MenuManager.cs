using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
	void Update ()
    {
		if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
	}
}
