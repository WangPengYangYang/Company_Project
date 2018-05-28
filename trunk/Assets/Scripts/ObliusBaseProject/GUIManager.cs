using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{


    public static GUIManager instance;

    public TutorialGUI tutorialGUI;
    public PauseGUI pauseGUI;
    public ShopGUI shopGUI;
    public GameOverGUI gameOverGUI;
    public MainMenuGUI mainMenuGUI;
    public OneMoreChanceGUI oneMoreChanceGUI;
    public InGameGUI inGameGUI;

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

        


    }

    public void ShowInGameGUI()
    {
        inGameGUI.gameObject.SetActive(true);
    }

    public void ShowGameOverGUI()
    {
        gameOverGUI.gameObject.SetActive(true);
    }

    public void HideGameOverGUI()
    {
        gameOverGUI.gameObject.SetActive(false);
    }


    public void ShowTutorialGUI()
    {
        tutorialGUI.Activate();
    }

    public void HideTutorialGUI()
    {
        tutorialGUI.Deactivate();
    }

    public void ShowPauseGUI()
    {
        pauseGUI.Activate();
    }

    public void ShowOneMoreChanceGUI()
    {
        oneMoreChanceGUI.gameObject.SetActive(true);
    }

    public void HideOneMoreChanceGUI()
    {
        oneMoreChanceGUI.gameObject.SetActive(false);
    }

    public void ShowShopGUI()
    {
        ShopHandler.instance.Activate();
    }

    public void ShowMainMenuGUI()
    {
        ObliusGameManager.instance.gameState = ObliusGameManager.GameState.menu;
        mainMenuGUI.gameObject.SetActive(true);
    }



}
