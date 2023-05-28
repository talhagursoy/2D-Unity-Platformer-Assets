using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	[SerializeField]
    private Transform player;
    [SerializeField]
    private LayerMask playerLayer;
    private float damage; //will rewamp later an change these;
	[SerializeField]
	private Transform leftFirePoint;
	[SerializeField]
	private Transform rightFirePoint;
	[SerializeField]
	private GameObject[] orbs;
	[SerializeField]
	private GameObject[] gazes;
	[SerializeField]
	private GameObject[] portals;
	private BossHealth health;
	private Animator anim;
	private float attackCd=0.2f;
	[SerializeField]
	private float orbRadius=2f;
	[SerializeField]
	private Transform tpPoint;
	[SerializeField]
	private GameObject[] spiritPortals;
	[SerializeField]
	private GameObject p2Spell;
	private void Awake() {
		health=GetComponent<BossHealth>();
		health.OnDamageTaken+=shieldHit;
		anim=GetComponent<Animator>();
	}
	private void teleport() {
        transform.position = tpPoint.transform.position;
		changeBodyType(1);
	}
	private void changeBodyType(int act){
		if(act==1){
			transform.GetComponent<Rigidbody2D>().isKinematic=true;
		}
		else{
			transform.GetComponent<Rigidbody2D>().isKinematic=false;
		}
		
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
	IEnumerator activateGaze2() {
		int randomIndex = Random.Range(0,3);
		for (int i = 0; i < gazes.Length; i++){
			gazes[randomIndex].SetActive(true);
			randomIndex++;
			randomIndex%=gazes.Length;
			yield return new WaitForSeconds(1f);
		}	
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
	IEnumerator activatePortal1(){
		for (int i = 0; i < portals.Length; i++)
		{
			portals[i].SetActive(true);
			yield return new  WaitForSeconds(0.2f);
		}
	}
	private int findSpiritPortal(){
		for(int i = 0; i < spiritPortals.Length; i++) {
			if(!spiritPortals[i].activeInHierarchy)
				return i;
		}
		return 0;
	}
	private void activateSpiritPortal() {
		spiritPortals[findSpiritPortal()].SetActive(true);
	}
	IEnumerator activateShield(){
		health.shielded=true;
		yield return new WaitForSeconds(1f);
		health.shielded=false;
	}
	private void shieldHit () {
		anim.ResetTrigger("CreateShield");
		anim.SetTrigger("ShieldHit");
		health.shielded=false;
		sendGravityBall(leftFirePoint);
		sendGravityBall(rightFirePoint);
	}
	IEnumerator summonExplosion(){
		GameObject vortexGround = Instantiate(Resources.Load<GameObject>("CFX3_Vortex_Ground"), transform.position, Quaternion.identity);
		yield return new WaitForSeconds(attackCd);
		Instantiate(Resources.Load<GameObject>("CFXR Hit D 3D (Yellow)"), transform.position, Quaternion.identity);
		Collider2D player=Physics2D.OverlapCircle(transform.position, orbRadius, playerLayer);
        if (player != null) {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null) {
                playerHealth.TakeDamage(damage,(transform.position.x>player.transform.position.x?-1:1));
            }
        }
		Destroy(vortexGround);
	}
	private void darken(){
		DarkWave circleEffect = Instantiate(Resources.Load<DarkWave>("DarkWaveEffect"), transform.position, Quaternion.identity);
		circleEffect.StartEffect();
		GameManager.updateTorch(false);
		Transform darken = player.Find("Darken");
        if (darken != null){
            darken.gameObject.SetActive(true);
        }
	}
	public void animTrigger(string trigger) {
		anim.SetTrigger(trigger);
	}
	private void summonSpell() {
		p2Spell.SetActive(true);
	}
}
