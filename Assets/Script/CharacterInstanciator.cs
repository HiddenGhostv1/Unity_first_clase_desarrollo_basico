using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInstanciator : MonoBehaviour
{
    private GameManager manager;
    private CharacterStatsManager statsManager;

    private GameObject character;

    void Start()
    {
        manager = GameManager.GetInstance();
        statsManager = CharacterStatsManager.GetInstance();

        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Debug.Log("JugadorIndex: " + indexJugador);

        GameObject characterPrefab = manager.GetCharacter(); // Corrección aquí
        Debug.Log("Jugador: " + characterPrefab);

        Vector3 respawnPoint = statsManager.GetRespawnPoint();
        Debug.Log("RespawnPoint: " + respawnPoint);

        character = Instantiate(characterPrefab, respawnPoint, Quaternion.identity);
        character.GetComponent<CharacterController>().SetMask(LayerMask.GetMask("Wall"));
    }

    public GameObject GetCharacter()
    {
        return character;
    }
}