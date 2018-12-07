using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighScoreUI : MonoBehaviour
{

    private Text highScoreText;
    private float startTime;
    int seconds;

    void Awake()
    {
        startTime = Time.time;
        highScoreText = GetComponent<Text>();
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime + 1;
        seconds = (int)(t % 60);
        if (seconds <= 5)
        {
            highScoreText.text = "HIGHSCORE: " + GameMaster.highScore.ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
