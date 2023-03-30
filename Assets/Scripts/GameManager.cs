using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager current;
    List<ChestScript> chests;
    DoorScript currentDoor;	
    string sceneName;
    Transform bossSpawnPoint;
    void Awake(){
		if (current!=null &&current!=this)
		{
			Destroy(gameObject);
			return;
		}
		current = this;
        chests=new List<ChestScript>();
		DontDestroyOnLoad(gameObject);//decide if wanna keep around the game manager or reset
    }
    private void Start() {
        sceneName=SceneManager.GetActiveScene().name;
    }
    public static void registerChest(ChestScript chest) {
        if(current==null)
            return;
        if(!current.chests.Contains(chest)){
            current.chests.Add(chest);
            //update ui to show correct number of chest
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
        if(current.chests.Count==0){
            current.currentDoor.openDoor();
        }          
    }
    public static void loadScene(){
        SceneManager.LoadScene(nextScene(current.sceneName));
    }
    public static int nextScene(string sceneName) {
        int levelNumber;
        int.TryParse(sceneName.Substring(5), out levelNumber);//if condition maybe to ensure if parse goes wrong
        return levelNumber+1;
    }
    public static IEnumerator summonBoss1(){
        GameObject bossVFX = Instantiate(Resources.Load<GameObject>("CFX2_BatsCloud"), current.bossSpawnPoint.transform.position, Quaternion.identity);
        bossVFX.transform.SetParent(current.bossSpawnPoint.transform);
        GameObject boss = Instantiate(Resources.Load<GameObject>("WitchBoss"), current.bossSpawnPoint.transform.position, Quaternion.identity);
        boss.SetActive(false);
        float vfxDuration = bossVFX.GetComponent<ParticleSystem>().main.duration;
        yield return new WaitForSeconds(vfxDuration);
        boss.SetActive(true);
    }
}
