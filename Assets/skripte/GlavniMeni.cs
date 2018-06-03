using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlavniMeni : MonoBehaviour {

    public string scenaZaLoad = "OdabirLevela";

	public void Play()
    {
        SceneManager.LoadScene(scenaZaLoad);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
