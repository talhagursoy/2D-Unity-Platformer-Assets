using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject levelMenu;
    public void startGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void quit() {
        Application.Quit();
    }
    public void loadLevel(){
        string levelName = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(levelName);
    }
    public void mainMenu() {
        startMenu.SetActive(true);
        levelMenu.SetActive(false);
    }
    public void showLevelMenu() {
        startMenu.SetActive(false);
        levelMenu.SetActive(true);
    }
}
