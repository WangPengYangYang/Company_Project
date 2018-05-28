using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameGUI : MonoBehaviour
{

    public static InGameGUI instance;
    public Text scoreText;
    public Text specialPointsText;
    


    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = ScoreHandler.instance.score.ToString();
        specialPointsText.text = ScoreHandler.instance.specialPoints.ToString();

    }


    public void OnTutorialButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();
        GUIManager.instance.ShowTutorialGUI();
    }

    public void OnPauseButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();
        GUIManager.instance.ShowPauseGUI();
    }


}
