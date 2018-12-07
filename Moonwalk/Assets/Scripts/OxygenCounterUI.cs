using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class OxygenCounterUI : MonoBehaviour
{

    private Text oxygenText;

    void Awake()
    {
        oxygenText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        oxygenText.text = "OXYGEN: " + (GameMaster.gm.startingOxygen - GameMaster.remainingOxygen).ToString();
    }
}
