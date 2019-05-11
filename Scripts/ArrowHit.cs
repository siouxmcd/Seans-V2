using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : Photon.MonoBehaviour {

    public GameObject splash;
    private Rigidbody rb;

    private GameObject light;

	// Use this for initialization
	void Start () {
        rb = transform.parent.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(enabled)
            Instantiate(splash, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Light")
        {
            light = other.gameObject;
            photonView.RPC("PutOutLight", PhotonTargets.All);
            //other.gameObject.SetActive(false);
        }
    }

    [PunRPC]
    void PutOutLight()
    {
        Debug.Log("RPC Sent");
        light.SetActive(false);
    }
}
