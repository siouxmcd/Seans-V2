using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject[] lights;
    public GameObject player;
    public GameObject player2;
    public GameUI gameUI;
    public int[] snkfactors;
    public int snklevel;
    public int currentSnkLevel;
    private int length;
    private float[] dist;
    private bool[] notInShadow;

	// Use this for initialization
	void Start () {
        length = lights.Length;
        dist = new float[length];
        snkfactors = new int[length];
        notInShadow = new bool[length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        for (int i = 0; i < length; i++)
        {
            //if (player.activeInHierarchy)
            //{
            //    RaycastHit hit;
            //    Ray ray = new Ray(lights[i].transform.position, player.transform.position - lights[i].transform.position);
            //    Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
            //    if (Physics.Raycast(ray, out hit, 100))
            //    {
            //        dist[i] = hit.distance;
            //        checkHit(hit.collider.gameObject, i);
            //    }
            //}
            //else
            //{
            //    //Debug.Log("Where's player 1?");
            //}
            if (player2.activeInHierarchy)
            {
                RaycastHit hit;
                Ray ray = new Ray(lights[i].transform.position, player2.transform.position - lights[i].transform.position);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
                if (Physics.Raycast(ray, out hit, 100))
                {
                    dist[i] = hit.distance;
                    //Debug.Log(dist[i]);
                    checkHit(hit.collider.gameObject, i);
                }
            }
            //else
            //{
            //    //Debug.Log("Where's player 2?");
            //}
        }
        foreach (GameObject light in lights)
        {

        }
    }

    private void checkHit(GameObject hitObject, int i)
    {
        if (hitObject.GetComponent<CharacterController>() != null)
        {
            notInShadow[i] = true;
            calculateSneaky(i);
            //Debug.Log("I am in view of light " + i);
        }
        else
        {
            notInShadow[i] = false;
            //Debug.Log("I am hidden from light " + i);
            if(!notInShadow[0] && !notInShadow[1])
            {
                snklevel = 5;
                //Debug.Log(snklevel);
                gameUI.setSneakText(snklevel);
            }
        }

    }

    private void calculateSneaky(int j)
    {
        for(int i = 0; i < length; i++)
        {
            if (dist[i] < 2.5)
            {
                snkfactors[i] = 0;
                //Debug.Log(snkfactors);
            }
            if (dist[i] < 5 && dist[i] >= 2.5)
            {
                snkfactors[i] = 1;
                //Debug.Log(snkfactors);
            }
            if (dist[i] < 7.5 && dist [i] >= 5)
            {
                snkfactors[i] = 2;
            }
            if (dist[i] < 10 && dist[i] >= 7.5)
            {
                snkfactors[i] = 3;
            }
            if (dist[i] >= 10)
            {
                snkfactors[i] = 4;
                gameUI.setSneakText(snklevel);
            }
            //sDebug.Log(snkfactors[i]);
        }
        if (notInShadow[0] && notInShadow[1])
        {
            switch (snkfactors[0])
            {
                case 0:
                    switch (snkfactors[1])
                    {
                        case 0:
                            snklevel = 0;
                            currentSnkLevel = 0;
                            break;
                        case 1:
                            snklevel = 1;
                            currentSnkLevel = 1;
                            break;
                        case 2:
                            snklevel = 2;
                            currentSnkLevel = 2;
                            break;
                        default:
                            Debug.LogError("Unregistered Sneak Factor");
                            break;
                    }
                    break;
                case 1:
                    switch (snkfactors[1])
                    {
                        case 0:
                            snklevel = 1;
                            currentSnkLevel = 1;
                            break;
                        case 1:
                            snklevel = 2;
                            currentSnkLevel = 2;
                            break;
                        case 2:
                            snklevel = 3;
                            currentSnkLevel = 3;
                            break;
                        case 3:
                            snklevel = 3;
                            currentSnkLevel = 3;
                            break;
                        default:
                            Debug.LogError("Unregistered Sneak Factor");
                            break;
                    }
                    break;
                case 2:
                    switch (snkfactors[1])
                    {
                        case 0:
                            snklevel = 2;
                            currentSnkLevel = 2;
                            break;
                        case 1:
                            snklevel = 3;
                            currentSnkLevel = 3;
                            break;
                        case 2:
                            snklevel = 3;
                            currentSnkLevel = 3;
                            break;
                        case 3:
                            snklevel = 4;
                            currentSnkLevel = 4;
                            break;
                        default:
                            Debug.LogError("Unregistered Sneak Factor");
                            break;
                    }
                    break;
                case 3:
                    switch (snkfactors[1])
                    {
                        case 0:
                            snklevel = 2;
                            currentSnkLevel = 2;
                            break;
                        case 1:
                            snklevel = 3;
                            currentSnkLevel = 3;
                            break;
                        case 2:
                            snklevel = 4;
                            currentSnkLevel = 4;
                            break;
                        case 3:
                            snklevel = 4;
                            currentSnkLevel = 4;
                            break;
                    }
                    break;
                case 4:
                    snklevel = 5;
                    currentSnkLevel = 5;
                    break;
            }
        }
        if (!notInShadow[0] && notInShadow[1] || notInShadow[0] && !notInShadow[1])
        {
            if (currentSnkLevel != 5)
            {
                snklevel = currentSnkLevel + 1;
            }

        }
        //if (!notInShadow[0] && !notInShadow[1])
        //{
        //    snklevel = 5;
        //}
        //Debug.Log(snklevel);
        gameUI.setSneakText(snklevel);
    }
}
