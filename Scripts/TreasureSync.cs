using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSync : Photon.MonoBehaviour {

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void ChangeAcrossNetwork(bool setting)
    {
        photonView.RPC("ChangeGravity", PhotonTargets.All, setting);
    }

    [PunRPC]
    void ChangeGravity(bool setting)
    {
        Debug.Log("RPC Sent");
        rb.useGravity = setting;
    }
}