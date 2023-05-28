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
    private void OnEnable() {
        EnableButtonsUpToCurrentLevel();
    }
    public void startGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+PlayerPrefs.GetInt("SavedLevel", 1));
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
    private void EnableButtonsUpToCurrentLevel(){
        Component panel=transform.Find("LevelMenu");
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        if (panel != null){
            for (int i = 1; i <= savedLevel; i++){
                string buttonName = "Level" + i;
                Transform button = panel.transform.Find(buttonName);
                if (button != null)
                    button.GetComponent<Button>().interactable=true;
            }
        }
    }
}
