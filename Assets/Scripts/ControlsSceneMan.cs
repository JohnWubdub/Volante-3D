using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsSceneMan : MonoBehaviour {


	public void Exit ()
    {
        SceneManager.LoadScene("Menu");
    }
	
	void Update ()
    {
        if(Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
