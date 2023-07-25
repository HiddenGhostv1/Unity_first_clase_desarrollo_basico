using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float delayTime = 2f; // Tiempo de demora en segundos antes de desaparecer.
    private bool isDisappearing = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged") && !isDisappearing)
        {
            isDisappearing = true;
            Invoke("DisappearPlatform", delayTime);
        }
    }

    void DisappearPlatform()
    {
        // Desactivar el renderer y el collider para hacer que la plataforma "desaparezca".
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Restablecer la plataforma despu�s de un breve per�odo de tiempo.
        Invoke("ResetPlatform", 5f);
    }

    void ResetPlatform()
    {
        // Activar el renderer y el collider para que la plataforma vuelva a aparecer.
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        // Reiniciar la variable para que la plataforma pueda desaparecer nuevamente si el jugador la toca.
        isDisappearing = false;
    }
}
