using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vreme : MonoBehaviour {
    public Text nosiocTeksta;
	// Use this for initialization
	void Start () {
        nosiocTeksta = GetComponent<Text>() as Text;
	}

    // Update is called once per frame
    void Update(){
        if (GameManager.getSekunde() > 0 && GameManager.zavrsenaIgra == false)
        {
            nosiocTeksta.text = "Vreme: " + ((int)GameManager.getSekunde()).ToString() + "s";
        }
        else
        {
            nosiocTeksta.text = "";
        }
    }
}
