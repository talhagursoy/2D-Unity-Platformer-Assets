using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager current;
    List<ChestScript> chests;
    DoorScript currentDoor;	
    string sceneName;
    Transform bossSpawnPoint;
    Image image;
    Torch[] torches;
    float countTorch;
    GameObject boss;
    void Awake(){
		if (current!=null &&current!=this)
		{
			Destroy(gameObject);
			return;
		}
		current = this;
        chests=new List<ChestScript>();
		DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        sceneName=SceneManager.GetActiveScene().name;
    }
    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        torches=GameObject.FindObjectsOfType<Torch>();
        current.countTorch=0;
        GameObject keyBarObject = GameObject.Find("KeyBar");
        if (keyBarObject != null){
            image = keyBarObject.GetComponent<Image>();
            current.image.fillAmount=1f;
        }     
    }
    public static void registerChest(ChestScript chest) {
        if(current==null)
            return;
        if(!current.chests.Contains(chest)){
            current.chests.Add(chest);
        }    
    }
    public static void registerDoor(DoorScript door){
        if(current==null)
            return;
        current.currentDoor=door;
    }
    public static void regbossSpawn(BossSpawnPoint bossSpawnPoint){
        if(current==null)
            return;
        current.bossSpawnPoint=bossSpawnPoint.transform;
    }
    public static void chestOpened(ChestScript chest){
        if(current==null)
            return;
        if(!current.chests.Contains(chest))
            return;
        current.chests.Remove(chest);
        current.image.fillAmount=(float)current.chests.Count/3f;
        if(current.chests.Count==0){
            current.currentDoor.openDoor();
        }          
    }
    public static void loadNextScene(){
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        int activeLevel = SceneManager.GetActiveScene().buildIndex;
        if (activeLevel >= savedLevel){
            PlayerPrefs.SetInt("SavedLevel", savedLevel + 1);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public static IEnumerator summonBoss1(){
        current.boss=GameObject.FindObjectOfType<Boss>(true).gameObject;
        GameObject bossVFX = Instantiate(Resources.Load<GameObject>("CFX2_BatsCloud"), current.bossSpawnPoint.transform.position, Quaternion.identity);
        current.boss.SetActive(false);
        float vfxDuration = bossVFX.GetComponent<ParticleSystem>().main.duration;
        yield return new WaitForSeconds(vfxDuration);
        current.boss.SetActive(true);
    }
    public static void updateTorch(bool act) {
        foreach(var torch in current.torches) {
			torch.active(act);
		}
    }
    public static void torchCount(){
        current.countTorch++;
        if(current.countTorch==current.torches.Length){
            current.countTorch=0;
            current.boss.GetComponent<Boss>().animTrigger("LoseWings");
        }
    }
}
