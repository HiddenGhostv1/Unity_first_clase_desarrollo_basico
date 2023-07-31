using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    private Animator animator;
    private bool mirandoDerecha = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Obtener el valor de entrada horizontal (izquierda -1, derecha 1, quieto 0)
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        // Mover la animación en la dirección indicada
        transform.Translate(Vector3.right * movimientoHorizontal * velocidadMovimiento * Time.deltaTime);

        // Actualizar la dirección de la mirada
        if (movimientoHorizontal > 0 && !mirandoDerecha)
        {
            MirarDerecha();
        }
        else if (movimientoHorizontal < 0 && mirandoDerecha)
        {
            MirarIzquierda();
        }
    }

    private void MirarDerecha()
    {
        mirandoDerecha = true;
        // Aquí debes cambiar la orientación de la animación hacia la derecha
    }

    private void MirarIzquierda()
    {
        mirandoDerecha = false;
        // Aquí debes cambiar la orientación de la animación hacia la izquierda
    }
}