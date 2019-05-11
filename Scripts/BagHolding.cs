using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagHolding : MonoBehaviour {

    public GameObject bag;
    public GameUI gameUI;
    public AudioClip coinDrop;
    public bool canDrop;
    private AudioSource bagSoundEffects;

	// Use this for initialization
	void Start () {
        bagSoundEffects = GetComponent<AudioSource>();
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (canDrop)
        {
            if (other.tag == "Coin")
            {
                gameUI.increaseScore(15);
                Destroy(other.gameObject);
                bagSoundEffects.PlayOneShot(coinDrop);
            }
            if (other.tag == "Goblet")
            {
                gameUI.increaseScore(150);
                Destroy(other.gameObject);
                bagSoundEffects.PlayOneShot(coinDrop);
            }
        }
    }
}
