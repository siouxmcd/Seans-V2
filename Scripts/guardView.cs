using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardView : MonoBehaviour {

    public Animator anim;
    public sneakyThief sT;
    public Transform player;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        /*if (anim.GetCurrentAnimatorStateInfo(0).IsName("prepare"))
        {
            transform.LookAt(player);
        }*/
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (sT.snklevel > 0.5)
            {
                anim.SetBool("isAware", true);

            }
            if (sT.snklevel > 0.3)
            {
                anim.SetBool("isPrepare", true);
            }
            if (sT.snklevel < 0.5)
            {
                anim.SetBool("isAware", false);
                anim.SetBool("isPrepare", false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetBool("isAware", false);
            anim.SetBool("isPrepare", false);
        }
    }
}
