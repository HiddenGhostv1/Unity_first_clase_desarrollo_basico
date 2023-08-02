using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterController : MonoBehaviour
{
    [SerializeField] private AudioSource teleporter_SFX;
    [SerializeField] private Transform destinationPoint;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Teleporter");
            teleporter_SFX.Play();
            animator.SetTrigger("collected");

            // ESPERAR A QUE TERMINE LA REPRODUCCIÓN DEL SONIDO Y LUEGO TELETRANSPORTAR
            StartCoroutine(TeleportToDestination(collider.transform));
        }
    }

    private IEnumerator TeleportToDestination(Transform playerTransform)
    {
        yield return new WaitForSeconds(teleporter_SFX.clip.length); // Esperar a que termine la reproducción del sonido

        // Teletransportar al jugador al punto de destino
        playerTransform.position = destinationPoint.position;
    }
}