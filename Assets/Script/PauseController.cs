using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{    
    [SerializeField] GameObject pauseMenu;  // Asignar el panel PauseMenu a este objeto desde 
                                            // el elemento 'script' en el inspector, una vez
                                            // este script se haya asociado a los botones.
    private static bool paused = false;

    
    public void Pause(){
        Debug.Log("BUTTON -> Pause");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NewGame(){
        Debug.Log("BUTTON -> NewGame");
        pauseMenu.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ContactUs(string enlace){
        Application.OpenURL(enlace);
    }

    public void Resume(){
        Debug.Log("BUTTON -> Resume");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void goMenu(int _escena){
        Debug.Log("BUTTON -> Home");
        Time.timeScale = 1f;
        SceneManager.LoadScene(_escena);
    }

    public void Audio(){
        Debug.Log("BUTTON -> Audio");
    }

    public static bool isPaused(){
        return PauseController.paused;
    }
}
