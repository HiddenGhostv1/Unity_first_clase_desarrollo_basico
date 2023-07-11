using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptme : MonoBehaviour
{
    private int vidas = 3;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteR;

    float nivelTecho             = 8.82f;
    //float limiteR                = 10.6604f;
    //float limitel                = -11.44f;
    //float Velocidad              = 4f;
    float fuerzaSalto            = 110f;
    float fuerzaDesplazamiento   = 80;

    bool enElPiso = false;
    bool hasjump = false;

    [SerializeField] private AudioSource salto_SFX;


    // Start is called before the first frame update
    void Start()
    {
       // Punto de inicio personaje (-10.44,-2.28)
       gameObject.transform.position = new Vector3(-10.44f,nivelTecho,0);
       Debug.Log("INIT");
       Debug.Log("VIDAS: " + vidas);
       rb = GetComponent<Rigidbody2D>();
       animator = gameObject.GetComponent<Animator>();
       spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //float movimientoH = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(movimientoH * Velocidad, rb.velocity.y);
        //animator.SetFloat("Horizontal", Mathf.Abs(movimientoH));

        //if (Input.GetKeyDown(KeyCode.Space) && enElPiso)
        //{rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        //enElPiso = false;
        //animator.SetBool("Suelo", false);}
        //if (Input.GetKeyDown(KeyCode.RightArrow)){
        //    gameObject.GetComponent<SpriteRenderer>().flipX = false;}
        //else if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //}
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

        if (rb.velocity.y < -0.1){
            animator.SetBool("Falling", true);
        }    

        if((Input.GetKeyDown("space") && enElPiso)||(Input.GetKeyDown("space") && hasjump)){
                Debug.Log("UP - enElPiso: " + enElPiso);
                if(hasjump){
                    //Esto se ejecuta cyabdi ta ha saldo por primera vez
                    animator.SetBool("doblejump", true);
                }
                    else{
                        //Esto se ejecuta cuando es el primer salto
                        salto_SFX.Play();
                        hasjump = true;
                        animator.SetBool("jump", true);
                        animator.SetBool("doblejump", false);
                    }
                
                rb.AddForce(new Vector2(0, -fuerzaSalto*Physics2D.gravity[1]*rb.mass));
                salto_SFX.Play();
                enElPiso = false;
                }
            }        
                
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Ground"){
            enElPiso = true;
            animator.SetBool("doblejump", false);
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
