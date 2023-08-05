using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject defaultCharacter;
    private GameObject actualCharacter;

    private bool lockCharacterStats; // Agregado: propiedad para bloquear estadísticas de personaje

    private static GameManager Instance;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            actualCharacter = defaultCharacter;
            lockCharacterStats = false; // Inicializa la propiedad
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager GetInstance()
    {
        return GameManager.Instance;
    }

    public void SetCharacter(GameObject character)
    {
        actualCharacter = character;
    }

    public GameObject GetCharacter()
    {
        return actualCharacter;
    }

    public bool LockCharacterStats // Agregado: propiedad para acceder a lockCharacterStats
    {
        get { return lockCharacterStats; }
        set { lockCharacterStats = value; }
    }
}