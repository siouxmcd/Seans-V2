using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : Photon.MonoBehaviour {

    public GameObject avatar;
    public GameObject remote;
    public Transform playerGlobal;
    public Transform playerLocal;

    private Vector3 realPosition = Vector3.zero;
    private Quaternion realRotation = Quaternion.identity;

	// Use this for initialization
	void Start () {
        Debug.Log("instantiated babaaaaayyyy");

        if (photonView.isMine)
        {
            StartCoroutine(CheckForAvatar());
            playerGlobal = GameObject.Find("OVRCameraRig/TrackingSpace").transform;

            this.transform.SetParent(playerGlobal);
            this.transform.localPosition = Vector3.zero;
        }
	}

    private void Update()
    {
        if (!photonView.isMine)
        {
            if (remote == null)
            {
                try
                {
                    remote = GameObject.Find("RemoteAvatar(Clone)");
                    remote.transform.SetParent(this.transform);
                    remote.transform.localPosition = Vector3.zero;
                    remote.transform.localRotation = Quaternion.identity;
                    this.transform.SetParent(playerGlobal);
                    this.transform.localPosition = Vector3.zero;
                }
                catch
                {
                    Debug.Log("Couldn't assign RemoteAvatar");
                }
            }
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);

        }
    }

    void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
            if (stream.isWriting)
            {
                stream.SendNext(playerGlobal.position);
                stream.SendNext(playerGlobal.rotation);
            }
            else
            {
                realPosition = (Vector3)stream.ReceiveNext();
                realRotation = (Quaternion)stream.ReceiveNext();
                avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
                avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
            }
	}

    IEnumerator CheckForAvatar()
    {
        yield return new WaitForSeconds(1);
        avatar = GameObject.Find("LocalAvatar(Clone)");
        avatar.transform.SetParent(playerGlobal);
        avatar.transform.localPosition = Vector3.zero;
        avatar.transform.localRotation = Quaternion.identity;
        //avatar.transform.SetParent(this.transform);
    }
}
