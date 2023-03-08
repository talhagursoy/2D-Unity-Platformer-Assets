using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float leftAngle;
    [SerializeField]
    private float rightAngle;
    private bool clockWise=true;

    // Start is called before the first frame update
    void Start()
    {
        body=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(clockWise)
            body.angularVelocity=-1*speed;
        else
            body.angularVelocity=speed;
        changeDirection();
    }
    private void changeDirection() {
        if(transform.rotation.z>=rightAngle){
            clockWise=true;
;        }
        else if(transform.rotation.z<leftAngle)
            clockWise=false;
    }
}
