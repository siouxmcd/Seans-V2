using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sneakyThief : MonoBehaviour {

    [SerializeField] GameObject[] lights;
    public GameObject player;
    public Image indicator;
    public Color c;
    public float snklevel;

    private int length;
    private int[] sneakFactors;
    private float[] dist;
    private bool[] notInShadow;

    // Use this for initialization
    void Start () {
        
        snklevel = 0;
        length = lights.Length;
        dist = new float[length];
        notInShadow = new bool[length];
        sneakFactors = new int[length];
    }
	
	// Update is called once per frame
	void Update () {
		if(player == null)
        {
            try
            {
                player = GameObject.FindWithTag("Player");
            }
            catch
            {
                Debug.LogError("Could not assign player");
            }
        }
        if(indicator == null)
        {
            try
            {
                indicator = GameObject.FindWithTag("Gem").GetComponent<Image>();
                c = indicator.color;
            }
            catch
            {
                Debug.LogError("Could not assign indicator");
            }
        }
	}

    private void FixedUpdate()
    {
        int i = 0;
        foreach (GameObject light in lights)
        {
            if (light.activeSelf)
            {
                RaycastHit hit;
                Ray ray = new Ray(light.transform.position, player.transform.position - light.transform.position);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
                if (Physics.Raycast(ray, out hit, 100))
                {
                    dist[i] = hit.distance;
                    //Debug.Log(dist[i]);
                    checkHit(hit.collider.gameObject, i);
                }
            }


            i++;
        }
    }

    private void checkHit(GameObject hitObject, int i)
    {
        snklevel = 0;
        if (hitObject.GetComponent<CharacterController>() != null)
        {
            notInShadow[i] = true;
            calculateSneaky(i);
            //Debug.Log("I am in view of light " + i);
        }
        else
        {
            notInShadow[i] = false;
            sneakFactors[i] = 5;
            ChangeIndicator();
            //Debug.Log("I am hidden from light " + i);
        }
    }

    private void calculateSneaky(int i)
    {
        if (dist[i] < 1)
        {
            sneakFactors[i] = 0;
        }
        if (dist[i] > 1 && dist[i] < 2)
        {
            sneakFactors[i] = 1;
        }
        if (dist[i] > 2 && dist[i] < 4)
        {
            sneakFactors[i] = 2;
        }
        if (dist[i] > 4 && dist[i] < 6)
        {
            sneakFactors[i] = 3;
        }
        if (dist[i] > 6 && dist[i] < 8)
        {
            sneakFactors[i] = 4;
        }
        if (dist[i] > 8)
        {
            sneakFactors[i] = 5;
        }
        //Debug.Log("Light 1: " + sneakFactors[0]);
        //Debug.Log("Light 2: " + sneakFactors[1]);

        snklevel = 0;

        foreach(int factor in sneakFactors)
        {
            if (factor == 0)
            {
                snklevel += 1;
            }
            else if (factor == 1)
            {
                snklevel += 0.7f;
            }
            else if (factor == 2)
            {
                snklevel += 0.45f;
            }
            else if (factor == 3)
            {
                snklevel += 0.25f;
            }
            else if (factor == 4)
            {
                snklevel += 0.1f;
            }
        }

        if (snklevel > 1)
        {
            snklevel = 1;
        }

        ChangeIndicator();
        Debug.Log("SneakLevel: " + snklevel);
    }

    private void ChangeIndicator()
    {
        c.a = snklevel;
        indicator.color = c;
        if(snklevel > 0.5)
        {
            player.transform.position = new Vector3(0.54f, 2.12f, -1.8f);
        }
    }
}
