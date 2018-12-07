using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class YourScoreUI : MonoBehaviour
{

    private Text yourScoreText;
    private float startTime;
    int seconds;

    void Awake()
    {
        yourScoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        yourScoreText.text = "YOUR SCORE: " + GameMaster.Money.ToString();
    }
}
