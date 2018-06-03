using UnityEngine;
using UnityEngine.SceneManagement;
public class PauzaMeni : MonoBehaviour {

    public GameObject pauzaUI;
    public static bool ukljucenaPauza;
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toogle();
        }
	}
    public void Toogle()
    {
        if(GameManager.zavrsenaIgra == false)
        {
            pauzaUI.SetActive(!pauzaUI.activeSelf);
        }
        

        if (pauzaUI.activeSelf && GameManager.zavrsenaIgra == false)
        {
            ukljucenaPauza = true;
            pauzaUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            ukljucenaPauza = false;
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toogle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        Toogle();
        Kraj.kockaKraj = false;
        GameManager.zavrsenaIgra = false;
        SceneManager.LoadScene("GlavniMeni");
    }
}
