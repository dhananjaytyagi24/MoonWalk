using UnityEngine;
using UnityEngine.UI;

public class HighScoreEndGame : MonoBehaviour
{

    private Text highScoreText;

    void Awake()
    {
        highScoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        highScoreText.text = "HIGH SCORE: " + GameMaster.highScore.ToString();
    }
}
