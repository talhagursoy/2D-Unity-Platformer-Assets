using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager current;
    List<ChestScript> chests;
    DoorScript currentDoor;	
    void Awake()
	{
		if (current!=null &&current!=this)
		{
			Destroy(gameObject);
			return;
		}
		current = this;
        chests=new List<ChestScript>();
		DontDestroyOnLoad(gameObject);//decide if wanna keep around the game manager or reset
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
    public static void chestOpened(ChestScript chest){
        if(current==null)
            return;
        if(!current.chests.Contains(chest))
            return;
        current.chests.Remove(chest);
        if(current.chests.Count==0)
            current.currentDoor.openDoor();
            //update ui that chest is opened       
    }
}
