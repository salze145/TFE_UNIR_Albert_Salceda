using UnityEngine;
using UnityEngine.UI;

public class BotonGameOver : MonoBehaviour
{

    private Button miBoton;

    private void Awake()
    {
        VincularBoton();
    }

    private void OnEnable()
    {
        VincularBoton();
    }

    private void VincularBoton()
    {
        if (miBoton == null)
        {
            miBoton = GetComponent<Button>();
        }

        if (miBoton != null)
        {
            // Limpia cualquier listener antiguo y añade el actual
            miBoton.onClick.RemoveAllListeners();
            miBoton.onClick.AddListener(ReiniciarJuego);
        }
    }



    // Función pública para asignar al botón
    public void ReiniciarJuego()
    {
        //reanudar el tiempo por si el juego estaba en pausa
        Time.timeScale = 1f;

        //Buscamos la instancia viva del GameManager en la memoria y llamamos al reinicio
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarJuegoCompleto();
        }
        else
        {
            Debug.LogError("[BotonGameOver] No se encontró ninguna instancia de GameManager activa.");
        }
    }
}
