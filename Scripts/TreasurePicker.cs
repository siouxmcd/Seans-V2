using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePicker : MonoBehaviour {

    public GameUI gameUI;
    public AudioClip coinDrop;
    public LayerMask mask;
    private AudioSource bagSoundEffects;
    public Renderer mat;
    public Renderer prevmat;
    private float matDefault;
    private bool changedMat;

    // Use this for initialization
    void Start () {
        bagSoundEffects = this.GetComponentInParent<AudioSource>();
        mask = LayerMask.GetMask("Treasure");
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray treasureRay = new Ray(this.transform.position, transform.forward);
        Debug.DrawRay(treasureRay.origin, treasureRay.direction * 2, Color.red);
        if (Physics.Raycast(treasureRay, out hit, 1.5f, mask))
        {
            //Debug.Log("hit");
            if(hit.collider.tag == "Coin")
            {
                if (mat != null)
                {
                    Debug.Log(matDefault);
                    mat.material.SetFloat("_Metallic", matDefault);
                }
                mat = hit.transform.gameObject.GetComponent<Renderer>();
                if (!changedMat)
                {
                    matDefault = mat.material.GetFloat("_Metallic");
                }
                mat.material.SetFloat("_Metallic", 2);
                changedMat = true;

                /*gameUI.increaseScore(15);
                Destroy(hit.transform.gameObject);
                bagSoundEffects.PlayOneShot(coinDrop);*/

                if (Input.GetMouseButtonDown(0))
                {
                    gameUI.increaseScore(15);
                    Destroy(hit.transform.gameObject);
                    bagSoundEffects.PlayOneShot(coinDrop);
                }
            }
            if (hit.collider.tag == "Goblet")
            {
                mat = hit.transform.gameObject.GetComponent<Renderer>();
                if (!changedMat)
                {
                    matDefault = mat.material.GetFloat("_Metallic");
                }
                mat.material.SetFloat("_Metallic", 2);
                changedMat = true;

                if (Input.GetMouseButtonDown(0))
                {
                    gameUI.increaseScore(150);
                    Destroy(hit.transform.gameObject);
                    bagSoundEffects.PlayOneShot(coinDrop);
                }
            }
        } else
        {
            if (mat != null)
            {
                Debug.Log(matDefault);
                mat.material.SetFloat("_Metallic", matDefault);
            }
        }
	}
}
