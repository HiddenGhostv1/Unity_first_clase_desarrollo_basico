using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool canForce;
    private int vidas = 3;
    private Vector3 puntoAparicion = new Vector3(0,6.1f,0);

    [SerializeField] private AudioSource jump_SFX;
    
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.transform.position = puntoAparicion;
        Debug.Log("INIT");
        Debug.Log("GRAVITY: " + Physics2D.gravity[1] + " [m/s²]");
        Debug.Log("DELTA TIME: " + Time.deltaTime + " [s]");
        Debug.Log("FREQUENCY: " + 1/Time.deltaTime + " [fps]");
        Debug.Log(Physics2D.simulationMode);
        Debug.Log("***********************************************");
        Debug.Log("Vidas: " + vidas);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.rotation.z > 0.3 || gameObject.transform.rotation.z < -0.3){
            Debug.Log("ROTATION: " + gameObject.transform.rotation.z);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if(Input.GetKey("right") && canForce){
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f, 0)); // Fuerza en Newtons
        }
        else if(Input.GetKey("left") && canForce && gameObject.transform.position.x >= -8.40){
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f, 0)); // Fuerza en Newtons
        }

        if(Input.GetKeyDown("space") && canForce){
            Debug.Log("JUMP - canForce: " + canForce);
            jump_SFX.Play();
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -50*Physics2D.gravity[1]*gameObject.GetComponent<Rigidbody2D>().mass));
            canForce = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Ground"){
            canForce = true;
            Debug.Log("GROUND COLLISION - canForce: " + canForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "FallDetector"){
            Debug.Log("**AL ABISMO**");
            vidas-=1;
            Debug.Log("Vidas: " + vidas);
            gameObject.transform.position = puntoAparicion;
        }
    }
}
