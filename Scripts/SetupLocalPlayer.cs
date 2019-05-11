using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

    public GameObject ovrCtrl;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            Camera.main.enabled = false;
        }
        else
        {
            //Destroy Cameras for other players
            Destroy(ovrCtrl);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
