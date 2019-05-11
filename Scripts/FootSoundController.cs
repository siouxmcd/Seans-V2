using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FootSoundController : MonoBehaviour {

    public List<GroundType> GroundTypes = new List<GroundType>();
    public FirstPersonController fpc;
    public string currentGround;

	// Use this for initialization
	void Start () {
        setGroundType(GroundTypes[0]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Carpet")
        {
            setGroundType(GroundTypes[1]);
        }
        else
        {
            setGroundType(GroundTypes[0]);
        }
    }

    public void setGroundType(GroundType ground)
    {
        if(currentGround != ground.name)
        {
            fpc.m_FootstepSounds = ground.footstepSounds;
            fpc.m_WalkSpeed = ground.walkSpeed;
            fpc.m_RunSpeed = ground.runSpeed;
            currentGround = ground.name;
        }
    }
}

[System.Serializable]
public class GroundType
{
    public string name;
    public AudioClip[] footstepSounds;
    public float walkSpeed = 5;
    public float runSpeed = 10;
}