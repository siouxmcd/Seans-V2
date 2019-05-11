using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawn : MonoBehaviour {

    public static ArrowSpawn singleton;

    public GameObject arrowPrefab;
    public GameObject rightHand;
    public GameObject stringAttachPoint;
    public GameObject arrowStartPoint;
    public GameObject stringStartPoint;

    public GameObject currentArrow;

    private bool isAttached = false;
    private float stringDist;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
    }

    private void OnDestroy()
    {
        if (singleton == this)
            singleton = null;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PullString();
	}

    public void AttachArrowToBow()
    {
        currentArrow.transform.parent = stringAttachPoint.transform;
        currentArrow.transform.localPosition = arrowStartPoint.transform.localPosition;
        currentArrow.transform.localRotation = arrowStartPoint.transform.localRotation;

        isAttached = true;
    }

    private void PullString()
    {
        if (isAttached)
        {
            float dist = (stringStartPoint.transform.position - rightHand.transform.position).magnitude;
            stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(dist * 10f, 0f, 0f);

            stringDist = (stringStartPoint.transform.position - stringAttachPoint.transform.position).magnitude;

            if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
        currentArrow.transform.parent = null;
        rb.velocity = currentArrow.transform.forward * 80f * stringDist;
        rb.useGravity = true;
        rb.isKinematic = false;

        currentArrow.GetComponent<Arrow>().Fired();
        currentArrow.GetComponentInChildren<ArrowHit>().enabled = true;

        stringAttachPoint.transform.position = stringStartPoint.transform.position;

        currentArrow = null;
        isAttached = false;
    }

    private void AttachArrow()
    {
        if(currentArrow == null)
        {
            Debug.Log("spawning arrow");
            currentArrow = PhotonNetwork.Instantiate("Arrow (1)", Vector3.zero, Quaternion.identity, 0);
            currentArrow.transform.parent = rightHand.transform;
            currentArrow.transform.localPosition = Vector3.zero + new Vector3 (0,-0.03f,0.29f);
            currentArrow.transform.localRotation = Quaternion.identity;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "RightHand")
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
            {
                Debug.Log("arrow");
                AttachArrow();
            }
        }
    }
}
