using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptme : MonoBehaviour
{
    private int vidas = 3;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteR;

    float nivelTecho           = 9f;
    float fuerzaSalto          = 80;     // x veces la masa del personaje
    float fuerzaImpulso        = 2000;  // Fuerza en Newtons
    float fuerzaDesplazamiento = 100;   // Fuerza en Newtons

    bool enElPiso = false;
    bool hasJump = false;

    [SerializeField] private AudioSource salto_SFX;


    // Start is called before the first frame update
    void Start()
    {
       // Punto de inicio personaje (-10.44,-2.28)
       //gameObject.transform.position = new Vector3(-5.04f,nivelTecho,0);
       Debug.Log("INIT");
       Debug.Log("VIDAS: " + vidas);
       rb = GetComponent<Rigidbody2D>();
       animator = gameObject.GetComponent<Animator>();
       spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetKey("right") && enElPiso){
               Debug.Log("RIGHT");
               rb.AddForce(new Vector2(fuerzaDesplazamiento, 0));
               animator.SetBool("running", true);
               spriteR.flipX=false;
        }
        else if(Input.GetKey("left") && enElPiso){
                Debug.Log("LEFT");
                rb.AddForce(new Vector2(-fuerzaDesplazamiento, 0));
                animator.SetBool("running", true);
                spriteR.flipX=true;
        }
        if( !(Input.GetKey("right") || Input.GetKey("left")) ){
            animator.SetBool("running", false);
            }

        if(rb.velocity.y < -0.1){
            hasJump = false;
            animator.SetBool("Falling", true);
            animator.SetBool("jump", false);
            animator.SetBool("doubleJump", false);
        }

        if((Input.GetKeyDown("space") && enElPiso)||(Input.GetKeyDown("space") && hasJump)){
                Debug.Log("UP - enElPiso: " + enElPiso);
               if(hasJump){
                // Esto se ejecuta cuando YA HA SALTADO por primera vez
                animator.SetBool("doubleJump", true);
                hasJump  = false;
                float d_i = 1;
                if(rb.velocity.x < 0) d_i = -1; // ¿El personaje va para la derecha o la izquierda?
                //fuerza vertical y horizontal - como el personaje está en el aire es necesario imprimirle también fuerza horizontal
                rb.AddForce(new Vector2(d_i*fuerzaImpulso, -fuerzaSalto*Physics2D.gravity[1]*rb.mass));
                }

            else{
                // Esto se ejecuta cuando es el PRIMER SALTO
                salto_SFX.Play();
                hasJump  = true;
                animator.SetBool("jump", true);
                animator.SetBool("doubleJump", false);
                //fuerza vertical - el desplazamiento horizontal lo da la inercia que lleve el personaje
                rb.AddForce(new Vector2(0, -fuerzaSalto*Physics2D.gravity[1]*rb.mass));
            }
            enElPiso = false;
        }
    }        
                
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Ground"){
            enElPiso = true;
            animator.SetBool("doubleJump", false);
            animator.SetBool("jump", false);
            animator.SetBool("Falling", false);
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
            vidas = 3;
        }
        gameObject.transform.position = new Vector3(-10.44f,nivelTecho,0);
    }
}
