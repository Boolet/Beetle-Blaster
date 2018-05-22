using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    public Text scoreboard;
    Dictionary<string, int> scores = new Dictionary<string, int>();
	// Use this for initialization
	void Start () {
        //Remove after testing is done
        updateScore("Player1", 1);
        updateScore("Player2", 2);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateScore(string player, int value)
    {
        if(!scores.ContainsKey(player))
        {
            scores[player] = 0;
        }
        scores[player] += value;
        string result = "";
        foreach(KeyValuePair<string, int> entry in scores)
        {
            result += entry.Key + ": " + entry.Value + "\n"; 
        }
        scoreboard.text = result;
    }
}
