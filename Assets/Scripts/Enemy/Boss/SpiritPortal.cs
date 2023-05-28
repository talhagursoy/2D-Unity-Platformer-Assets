using UnityEngine;

public class SpiritPortal : MonoBehaviour
{
    public GameObject[] spirits;
    private bool isActive = true;
    private bool hasActivatedSpirits;
    [SerializeField]
    private Transform player;
    private float summonOffset=2f;
    private void OnEnable() {
        transform.position=new Vector2(player.transform.position.x+summonOffset*-Input.GetAxisRaw("Horizontal"),player.transform.position.y+summonOffset);
        hasActivatedSpirits=false;
    }
    private void Start(){
        spirits = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++){
            spirits[i] = transform.GetChild(i).gameObject;
        }
    }
    private void Update(){
        if (!isActive){
            return;
        }
        bool anySpiritsActive = false;
        foreach (GameObject spirit in spirits){
            if (spirit.activeSelf){
                anySpiritsActive = true;
                break;
            }
        }
        if(!anySpiritsActive&&hasActivatedSpirits)
            DeactivatePortal();
    }
    private void activateSpirits() {
        foreach (var spirit in spirits){
            spirit.SetActive(true);
        }
        hasActivatedSpirits=true;
    }
    private void DeactivatePortal(){
        isActive = false;
        gameObject.SetActive(false);
    }
}