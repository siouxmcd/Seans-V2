using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour {

    string _room = "Tutorial_Seans";

    public GameObject thief;
    public GameObject deceiver;

    public readonly byte InstantiateVrAvatarEventCode = 123;

	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	void OnJoinedLobby()
    {
        Debug.Log("joined lobby");

        RoomOptions roomOptions = new RoomOptions() { };
        PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        int viewId = PhotonNetwork.AllocateViewID();

        PhotonNetwork.RaiseEvent(InstantiateVrAvatarEventCode, viewId, true, new RaiseEventOptions() { CachingOption = EventCaching.AddToRoomCache, Receivers = ReceiverGroup.All });

        PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);

        if (PhotonNetwork.isMasterClient)
        {
            Instantiate(thief);
        }
        else
        {
            Instantiate(deceiver);
        }
    }

    private void OnEvent(byte eventCode, object content, int senderId)
    {
        if(eventCode == InstantiateVrAvatarEventCode)
        {
            GameObject go = null;

            if(PhotonNetwork.player.ID == senderId)
            {
                go = Instantiate(Resources.Load("LocalAvatar")) as GameObject;
            }
            else
            {
                go = Instantiate(Resources.Load("RemoteAvatar")) as GameObject;
            }

            if(go != null)
            {
                PhotonView pView = go.GetComponent<PhotonView>();

                if (pView != null)
                {
                    pView.viewID = (int)content;
                }
            }
        }
    }

    public void OnEnable()
    {
        PhotonNetwork.OnEventCall += OnEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.OnEventCall -= OnEvent;
    }
}
