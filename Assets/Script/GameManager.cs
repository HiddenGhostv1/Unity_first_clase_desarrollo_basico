using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject defaultCharacter;
    private GameObject actualCharacter; // Mantiene el Prefab del personaje con el que se desea jugar

    //////////////////////////////////////////////
    /*          SINGLETON PATTERN               */
    private static GameManager Instance;
    private void Awake()
    {
        if(GameManager.Instance == null){
            GameManager.Instance = this;
            actualCharacter = defaultCharacter;
            DontDestroyOnLoad(this);
            Debug.Log("Owwo");
        }
        else{
            Destroy(this);
            Debug.Log("Twwo");
        }
    }
    public static GameManager getInstance(){
        return GameManager.Instance;
    }
    ///////////////////////////////////////////////

    public void setCharacter(GameObject character){
        actualCharacter = character;
    }
    public GameObject getCharacter(){
        return actualCharacter;
    }
}
