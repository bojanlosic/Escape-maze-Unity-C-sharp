using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {
    public static bool zavrsenaIgra;
    private static float sekunde;
    public float mrtveSekunde;
    public GameObject gameOverUI;
	// Use this for initialization
	void Start () {
        zavrsenaIgra = false;
        sekunde = mrtveSekunde;
    }

    public static float getSekunde()
    {
        return sekunde;
    }

	// Update is called once per frame
	void Update () {
        // da li je zavrsena igra
        if (zavrsenaIgra)
        {
            return;
        }

        sekunde -= Time.deltaTime;

        // Pronadjen izlaz iz lavirinta
        if (Kraj.kockaKraj)
        {
            krajIgre();
        }

        if (sekunde <= 0)
        {
            krajIgre();
        }

    }
    void krajIgre()
    {
        zavrsenaIgra = true;
        gameOverUI.SetActive(true);
    }
}
