using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Transform player;
	private bool isFlipped = false;
    [SerializeField]
    private LayerMask playerLayer;
    private float damage; //will rewamp later an change these;
	private float teleportDistance=10f;
	[SerializeField]
	private Transform leftFirePoint;
	[SerializeField]
	private Transform rightFirePoint;
	private GameObject[] orbs;
	private GameObject[] gazes;
	private GameObject[] portals;
	private Health health;
	private Animator anim;
	private void Awake() {
		health=GetComponent<Health>();
		health.OnDamageTaken+=shieldHit;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		anim=GetComponent<Animator>();
	}
	void Start() {
		GameObject orbHolder = GameObject.Find("OrbHolder");
		GravityOrb[] gravityOrbs = null;
		if (orbHolder != null) {
			gravityOrbs = orbHolder.GetComponentsInChildren<GravityOrb>(true);
		}
		GameObject gazeHolder = GameObject.Find("GazeHolder");
		GazeScript[] gazeObjects = null;
		if (gazeHolder != null) {
			gazeObjects = gazeHolder.GetComponentsInChildren<GazeScript>(true);
		}
		GameObject portalHolder = GameObject.Find("PortalHolder");
		PortalScript[] portalObjects = null;
		if (portalHolder != null) {
			portalObjects = portalHolder.GetComponentsInChildren<PortalScript>(true);
		}
		if (gravityOrbs != null && gravityOrbs.Length > 0) {
			orbs = new GameObject[gravityOrbs.Length];
			for (int i = 0; i < gravityOrbs.Length; i++) {
				orbs[i] = gravityOrbs[i].gameObject;
			}
		}
		if (gazeObjects != null && gazeObjects.Length > 0) {
			gazes = new GameObject[gazeObjects.Length];
			for (int i = 0; i < gazeObjects.Length; i++) {
				gazes[i] = gazeObjects[i].gameObject;
			}
		}
		if (portalObjects != null && portalObjects.Length > 0) {
			portals = new GameObject[portalObjects.Length];
			for (int i = 0; i < portalObjects.Length; i++) {
				portals[i] = portalObjects[i].gameObject;
			}
		}
	}
    public void Attack(){
		Vector3 pos = transform.position;
		pos += transform.right;
		pos += transform.up;
		Collider2D colInfo = Physics2D.OverlapCircle(pos, 5f,playerLayer);
		if (colInfo != null){
			Health health=colInfo.GetComponent<Health>();//bad place probably
            if(health!=null)
                health.takeDamage(damage,-1,4,2);
		}
	}
	public void LookAtPlayer(){
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}
	private void teleport() {
        Vector3 newBossPos = player.position + Vector3.right * teleportDistance;
        transform.position = newBossPos;
	}
	private int findOrb() {
        for (int i = 0; i < orbs.Length; i++){
            if(!orbs[i].activeInHierarchy){
				return i;
			}           
        }
        return 0;
    }
	private void sendGravityBall(Transform firePoint){
		if(firePoint==null){
			if(player.position.x>transform.position.x)
				firePoint=rightFirePoint;
			else
				firePoint=leftFirePoint;
		}
        orbs[findOrb()].transform.position=firePoint.position;
        orbs[findOrb()].GetComponent<GravityOrb>().activateOrb(firePoint.position.x > transform.position.x ? 1 : -1);
    }
	private int findGaze() {
        int randomIndex = 1;
		do{
			randomIndex = Random.Range(1,4);
		} 
		while(gazes[randomIndex - 1].activeInHierarchy);
		return randomIndex;
	}
	private void activateGazeObject() {
		gazes[findGaze()].SetActive(true);
	}
	private int findPortal() {
        int randomIndex = 1;
		do{
			randomIndex = Random.Range(1,4);
		} 
		while(portals[randomIndex - 1].activeInHierarchy);
		return randomIndex;
	}
	private void activatePortal() {
		portals[findPortal()].SetActive(true);
	}
	IEnumerator activateShield(){
		health.canTakeDamage=false;
		yield return new WaitForSeconds(1f);
		health.canTakeDamage=true;
	}
	private void shieldHit () {
		anim.ResetTrigger("CreateShield");
		anim.SetTrigger("ShieldHit");
		health.canTakeDamage=true;
		sendGravityBall(leftFirePoint);
		sendGravityBall(rightFirePoint);
	}
}
