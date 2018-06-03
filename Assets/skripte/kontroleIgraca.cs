using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kontroleIgraca : MonoBehaviour {
    private float brzina;
    private float brzinaKretanja = 4f;
    private float brzinaTrcanja = 7f;
    public float osetljivost = 2f;
    float napredNazad, levoDesno;
    float misX, misY;
    private bool setanje;
    CharacterController igrac;
    public GameObject kamera;
    private float popravkaMisa = 1;
	// Use this for initialization
	void Start () {
        igrac = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        kretanje();
    }

    void kretanje()
    {
        if (GameManager.zavrsenaIgra)
        {
            return;
        }
        if (!PauzaMeni.ukljucenaPauza)
        {
            //Cursor.visible = false;
            setanje = !Input.GetKey(KeyCode.LeftShift);
            brzina = setanje ? brzinaKretanja : brzinaTrcanja;

            napredNazad = Input.GetAxis("Vertical") * brzina;
            levoDesno = Input.GetAxis("Horizontal") * brzina;
            misX = Input.GetAxis("Mouse X") * osetljivost;
            misY = Input.GetAxis("Mouse Y") * osetljivost;

            Vector3 kretanje = new Vector3(levoDesno, 0.0f, napredNazad);

            transform.Rotate(0.0f, misX, 0.0f);
            if ((popravkaMisa += misY) > -90 && (popravkaMisa += misY) < 90)
            {
                kamera.transform.Rotate(-misY, 0.0f, 0.0f);
            }


            kretanje = transform.rotation * kretanje;
            igrac.Move(kretanje * Time.deltaTime);
        }
    }
}
