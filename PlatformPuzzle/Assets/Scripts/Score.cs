using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public GameObject platform;
    int score = 0;
    int targetScore = 0;

    public void Addscore(int amount)
    {
        score = score + amount;
        GetComponent<Text>().text = "Score: " + score + "/" + targetScore;
		if(score == targetScore)
		{
			platform.SetActive(true);
		}
    }

    public void AddTargetsScore(int amount)
    {
        targetScore = targetScore + amount;
    }
}
