using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public Text skor;
    public Text victoryDefeat;
    public Image pozadina;

    void Start()
    {
        pozadina = GetComponent<Image>();
    }

    void OnEnable()
    {
        if(GameManager.getSekunde() > 0)
        {
            pozadina.color = Color.green;
            //UnityEngine.Color.cyan; 
            //new Color(173, 68, 68, 197);

            victoryDefeat.text = "POBEDILI STE!";
        }
        else
        {
            pozadina.color = Color.red;
            //pozadina.color = new Color(68, 173, 134, 197);
            victoryDefeat.text = "GAME OVER!";
        }
        
        skor.text = ((int)GameManager.getSekunde()).ToString() + "s";
    }

    public void Retry()
    {
        Kraj.kockaKraj = false;
        GameManager.zavrsenaIgra = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Kraj.kockaKraj = false;
        GameManager.zavrsenaIgra = false;
        SceneManager.LoadScene("GlavniMeni");
    }
}
