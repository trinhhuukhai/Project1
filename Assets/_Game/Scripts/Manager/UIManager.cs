using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public GameObject mainmenu;
    public GameObject finish;

    public void OpenMainMenu()
    {
        mainmenu.SetActive(true);
        finish.SetActive(false);
    } 
    public void OpenFinish()
    {
        mainmenu.SetActive(false);
        finish.SetActive(true);
    }
    // Start is called before the first frame update
    public void PlayButton()
    {
        mainmenu.SetActive(false);
        LevelManager.Instance.OnStart();
     
    }

    public void RetryButton()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        OpenMainMenu();
        LevelManager.Instance.RetryLevel();

        


    }

    public void NextButton() {

        GameManager.Instance.ChangeState(GameState.MainMenu);

        OpenMainMenu();
        LevelManager.Instance.LoadNextLevel();

    }
}
