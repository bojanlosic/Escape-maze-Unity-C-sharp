using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraj : MonoBehaviour {
    public static bool kockaKraj;

	void OnTriggerEnter()
    {
        kockaKraj = true;
    }
    void OnTriggerExit()
    {
        kockaKraj = false;
    }
}
