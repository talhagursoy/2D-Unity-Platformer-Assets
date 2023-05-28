using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject deathMenu;
    [SerializeField]
    private GameObject pauseScreen;
     private void Awake() {
        deathMenu.SetActive(false);
        pauseScreen.SetActive(false);
        Time.timeScale=1;
        //DontDestroyOnLoad(gameObject);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseScreen.activeInHierarchy){
                pause(false);
            }
            else{
                pause(true);
            }
        }
            
    }
    public void showDeathMenu() {
        deathMenu.SetActive(true);
    }
    public void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void mainMenu() {
        SceneManager.LoadScene(0);
    }
    public void pause(bool status) {
        pauseScreen.SetActive(status);
        if(status)
            Time.timeScale=0;
        else
            Time.timeScale=1;
    }
}
