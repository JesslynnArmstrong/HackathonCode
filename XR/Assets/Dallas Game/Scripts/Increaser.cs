using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Increaser : MonoBehaviour
{
    private Text text;
    public int score;

    private IEnumerator Start()
    {
        text = GetComponentInChildren<Text>();
        int currentScore = Gamemanager.instance.score;
        Gamemanager.instance.score += score;
        text.text = currentScore.ToString();
        do
        {
            text.text = (++currentScore).ToString();
            yield return new WaitForEndOfFrame();

        } while (currentScore < Gamemanager.instance.score);
        text.text += "!";

        Destroy(gameObject, 3);
    }
}
