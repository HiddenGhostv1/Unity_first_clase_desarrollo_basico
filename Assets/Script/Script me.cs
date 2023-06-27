using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptme : MonoBehaviour
{
    private int vidas = 3;

    float nivelTecho             = 8.82f;
    float limiteR                = 10.6604f;
    float limitel                = -11.44f;
    float Velocidad              = 4f;
    float fuerzaSalto            = 110;
    float fuerzaDesplazamiento   = 100;

    bool enElPiso = true;

    [SerializeField] private AudioSource salto_SFX;

    // Start is called before the first frame update
    void Start()
    {
       // Punto de inicio personaje (-10.44,-2.28)
       gameObject.transform.position = new Vector3(-10.44f,nivelTecho,0);
       Debug.Log("INIT");
       Debug.Log("VIDAS: " + vidas);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.rotation.z > 0.3 || gameObject.transform.rotation.z < -0.3){
            Debug.Log("ROTATION: " + gameObject.transform.rotation.z);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if(Input.GetKey("right") && enElPiso){
            Debug.Log("RIGHT");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(fuerzaDesplazamiento, 0));
        }
        else if(Input.GetKey("left") && gameObject.transform.position.x > limitel){
                Debug.Log("LEFT");
                gameObject.transform.Translate(-Velocidad*Time.deltaTime, 0, 0);
        }

        if(Input.GetKeyDown("space") && enElPiso){
                Debug.Log("UP - enElPiso: " + enElPiso);
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -fuerzaSalto*Physics2D.gravity[1]*gameObject.GetComponent<Rigidbody2D>().mass));
                salto_SFX.Play();
                enElPiso = false;
                }
            }        
                
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Ground"){
            enElPiso = true;
            Debug.Log("GROUND COLLISION");
        }
        else if(collision.transform.tag == "obstacle"){
            enElPiso = true;
            Debug.Log("OBSTACLE COLLISION");
        }
    }   
    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("caida");
        vidas -= 1;
        Debug.Log("VIDAS: " + vidas);
        if(vidas <= 0){
            Debug.Log("GAME OVER");
        }
        gameObject.transform.position = new Vector3(-10.44f,nivelTecho,0);
    }
}
