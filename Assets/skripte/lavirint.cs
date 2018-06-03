using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavirint : MonoBehaviour {
    [System.Serializable]
    public class celija
    {
        public bool posecena;
        public GameObject sever;
        public GameObject istok;
        public GameObject zapad;
        public GameObject jug;
    }
    public GameObject zid;
    public float duzinaZida = 1.0f;
    public int velicinaPoX = 5;
    public int velicinaPoY = 5;
    private Vector3 pocetnaPozicija;
    private GameObject imeZida;
    private celija[] celije;
    public int trenutnaCelija = 0;
    private int ukupanBrojCelija;
    private int poseceneCelije = 0;
    private bool pravljenjeZgrade = false;
    private int trenutniKomsija = 0;
    private List<int> poslednjeCelije;
    private int vracanje = 0;
    private int zidZaSrusiti = 0;

    // Use this for initialization
    void Start () {
        napraviZidove();
	}
	
    void napraviZidove()
    {
        imeZida = new GameObject();
        imeZida.name = "Zid";
        pocetnaPozicija = new Vector3((-velicinaPoX / 2) + duzinaZida / 2, 0.0f, (-velicinaPoY / 2) + duzinaZida / 2);
        Vector3 mojaPozicija = pocetnaPozicija;
        GameObject privremeniZid;

        //Za X osu
        for(int i = 0; i < velicinaPoY; i++)
        {
            for(int j = 0; j <= velicinaPoX; j++)
            {
                mojaPozicija = new Vector3(pocetnaPozicija.x + (j * duzinaZida) - duzinaZida / 2, 0.0f, pocetnaPozicija.z + (i * duzinaZida) - duzinaZida / 2);
                privremeniZid = Instantiate(zid, mojaPozicija, Quaternion.identity);
                privremeniZid.transform.parent = imeZida.transform;
            }
        }

        //Za Y osu
        for (int i = 0; i <= velicinaPoY; i++)
        {
            for (int j = 0; j < velicinaPoX; j++)
            {
                mojaPozicija = new Vector3(pocetnaPozicija.x + (j * duzinaZida), 0.0f, pocetnaPozicija.z + (i * duzinaZida) - duzinaZida);
                privremeniZid = Instantiate(zid, mojaPozicija, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                privremeniZid.transform.parent = imeZida.transform;
            }
        }
        napraviCelije();
    }

    void napraviCelije()
    {
        poslednjeCelije = new List<int> ();
        poslednjeCelije.Clear ();
        ukupanBrojCelija = velicinaPoX * velicinaPoY;
        GameObject[] sviZidovi;
        int deca = imeZida.transform.childCount;
        sviZidovi = new GameObject[deca];
        celije = new celija[velicinaPoX * velicinaPoY];
        int istokZapad = 0;
        int pravljenjeDece = 0;
        int privremeniBrojac = 0;

        for (int i = 0; i < deca; i++)
        {
            sviZidovi[i] = imeZida.transform.GetChild(i).gameObject;
        }

        for(int pravljenjeCelije = 0; pravljenjeCelije < celije.Length; pravljenjeCelije++)
        {
            if (privremeniBrojac == velicinaPoX)
            {
                istokZapad++;
                privremeniBrojac = 0;
            }
            celije[pravljenjeCelije] = new celija();
            celije[pravljenjeCelije].istok = sviZidovi[istokZapad];
            celije[pravljenjeCelije].jug = sviZidovi[pravljenjeDece + (velicinaPoX + 1) * velicinaPoY];
            istokZapad++;
            privremeniBrojac++;
            pravljenjeDece++;
            celije[pravljenjeCelije].zapad = sviZidovi[istokZapad];
            celije[pravljenjeCelije].sever = sviZidovi[(pravljenjeDece + (velicinaPoX + 1) * velicinaPoY) + velicinaPoX - 1];

        }
        napraviLavirint();
    }

    void napraviLavirint()
    {
        while (poseceneCelije < ukupanBrojCelija)
        {
            if (pravljenjeZgrade)
            {
                nadjiKomsiju();
                if(celije[trenutniKomsija].posecena == false && celije[trenutnaCelija].posecena == true)
                {
                    rastruriZid();
                    celije[trenutniKomsija].posecena = true;
                    poseceneCelije++;
                    poslednjeCelije.Add(trenutnaCelija);
                    trenutnaCelija = trenutniKomsija;
                    if(poslednjeCelije.Count > 0)
                    {
                        vracanje = poslednjeCelije.Count - 1;
                    }
                }
            }
            else
            {
                trenutnaCelija = Random.Range(0, ukupanBrojCelija);
                celije[trenutnaCelija].posecena = true;
                poseceneCelije++;
                pravljenjeZgrade = true;
            }
            //Invoke("napraviLavirint", 0.0f);
        }
        Destroy(celije[0].istok);
        Debug.Log("Zavrseno");
    }

    void rastruriZid()
    {
        switch (zidZaSrusiti)
        {
            case 1: Destroy(celije[trenutnaCelija].sever); break;
            case 2: Destroy(celije[trenutnaCelija].istok); break;
            case 3: Destroy(celije[trenutnaCelija].zapad); break;
            case 4: Destroy(celije[trenutnaCelija].jug); break;
        }
    }

    void nadjiKomsiju()
    {
        
        int duzina = 0;
        int[] komsije = new int[4];
        int[] poveziZid = new int[4];
        int proveri = 0;
        proveri = ((trenutnaCelija + 1) / velicinaPoX);
        proveri -= 1;
        proveri *= velicinaPoX;
        proveri += velicinaPoX;

        //zapad
        if (trenutnaCelija + 1 < ukupanBrojCelija && (trenutnaCelija + 1) != proveri)
        {
            if (celije[trenutnaCelija + 1].posecena == false)
            {
                komsije[duzina] = trenutnaCelija + 1;
                poveziZid[duzina] = 3;
                duzina++;
            }
        }

        //istok
        if (trenutnaCelija - 1 >= 0 && trenutnaCelija != proveri)
        {
            if (celije[trenutnaCelija - 1].posecena == false)
            {
                komsije[duzina] = trenutnaCelija - 1;
                poveziZid[duzina] = 2;
                duzina++;
            }
        }

        //sever
        if (trenutnaCelija + velicinaPoX < ukupanBrojCelija)
        {
            if (celije[trenutnaCelija + velicinaPoX].posecena == false)
            {
                komsije[duzina] = trenutnaCelija + velicinaPoX;
                poveziZid[duzina] = 1;
                duzina++;
            }
        }

        //jug
        if (trenutnaCelija - velicinaPoX >= 0)
        {
            if (celije[trenutnaCelija - velicinaPoX].posecena == false)
            {
                komsije[duzina] = trenutnaCelija - velicinaPoX;
                poveziZid[duzina] = 4;
                duzina++;
            }
        }

        if (duzina != 0)
        {
            int izabrana = Random.Range(0, duzina);
            trenutniKomsija = komsije[izabrana];
            zidZaSrusiti = poveziZid[izabrana];
        }
        else
        {
            if(vracanje > 0)
            {
                trenutnaCelija = poslednjeCelije[vracanje];
                vracanje--;
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
