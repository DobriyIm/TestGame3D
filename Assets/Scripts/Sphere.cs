using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private Rigidbody rb;
    private Vector3 jump = Vector3.up * 200;
    private Vector3 forceDirection;
    private const float FORCE_AMPL = 1;

    private AudioSource hitSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.hitSound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move(){
        float fx = Input.GetAxis("Horizontal");
        float fy = Input.GetAxis("Vertical");

        //rb.AddForce(new Vector3(fx,0,fy) * 2);

        forceDirection = cam.transform.forward;
        forceDirection.y = 0;
        forceDirection = forceDirection.normalized * fy;
        forceDirection += cam.transform.right * fx;
        rb.AddForce(forceDirection * FORCE_AMPL);

    }
    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(jump);
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Wall")){
            if(GameMenu.SoundEnabled){
                this.hitSound.volume = GameMenu.SoundValue;
                this.hitSound.Play();
            }
        }
    }

}
