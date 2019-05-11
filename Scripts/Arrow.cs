using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private bool isFired = false;

    private void Update()
    {
        if (isFired)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
    }

    public void Fired()
    {
        isFired = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Bow")
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                ArrowSpawn.singleton.AttachArrowToBow();
            }
        }
    }
}
