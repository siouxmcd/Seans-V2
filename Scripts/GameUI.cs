using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    [SerializeField] Text scoreText;
    [SerializeField] Text sneakText;
    //[SerializeField] Text winText;

    private int score;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (score == 390)
        {
            //winText.text = "You have collected all of the treasure! \n You are a master thief!";
        }
        if(scoreText == null)
        {
            try
            {

                scoreText = GameObject.Find("Score").GetComponent<Text>();
            }
            catch
            {
                Debug.LogError("Could not assign score text");
            }
        }
	}

    public void increaseScore(int amount)
    {
        Debug.Log("gotit");
        score += amount;
        scoreText.text = "" + score;
    }

    public void setSneakText(int snklevel)
    {
        sneakText.text = "" + snklevel;
        switch (snklevel)
        {
            case 0:
                sneakText.color = Color.red;
                break;
            case 1:
                sneakText.color = Color.magenta;
                break;
            case 2:
                sneakText.color = Color.yellow;
                break;
            case 3:
                sneakText.color = Color.cyan;
                break;
            case 4:
                sneakText.color = Color.blue;
                break;
            case 5:
                sneakText.color = Color.green;
                break;
        }
    }
}
