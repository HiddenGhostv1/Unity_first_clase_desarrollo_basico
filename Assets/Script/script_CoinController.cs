using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    [SerializeField] private AudioSource getCoin_SFX;

    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Moneda");
        getCoin_SFX.Play();
        
       StartCoroutine(goNextLevel(getCoin_SFX.clip.length));
       gameObject.GetComponent<Renderer>().enabled = false;
    }   

    private IEnumerator goNextLevel(float delay){
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);        
        
        if(SceneManager.GetActiveScene().name=="SceneOne"){
            SceneManager.LoadScene("SceneTwo");
        }
        else{
            SceneManager.LoadScene("SceneOne");
        }
        
    }
}
