using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
   
    public TMP_Text puntuacionTexto;
   // public int PuntosTotales;
   public GameObject[] corazones;



    void start()
    {
      if (GameManager.Instance != null)
        {
            // Le pide al GameManager que sobrevivió los puntos acumulados del Nivel 1
            ActualizarPuntos(GameManager.Instance.PuntosTotales);
            //vidas = 3;
        }
       //corazones[].SetActive = true; 
    }
    

    public void ActualizarPuntos(int puntosTotales)
    {

        if (puntuacionTexto.text != null)
        {
            puntuacionTexto.text = puntosTotales.ToString();
        }
        Debug.Log($"El texto cambió a desde el script", this);
    }

    public void DesactivarVida(int indice){
        
       
        corazones[indice].SetActive(false);
       
    }

    public void ActualizarCorazones(int vidasActuales)
    {

        // 1. Mensaje para confirmar que este método REALMENTE se ejecuta tras reiniciar
        Debug.Log($"[HUD] Actualizando corazones a: {vidasActuales} en el Canvas: {gameObject.name} (Instance ID: {GetInstanceID()})");

        if (corazones == null || corazones.Length == 0)
        {
            Debug.LogError("[HUD] El array de corazones está vacío.");
            return; 
        }

        for (int i = 0; i < corazones.Length; i++)
        {
            if (corazones[i] != null)
            {
                // Si el índice es menor que las vidas actuales, el corazón debe estar ACTIVO.
                // Por ejemplo, con 2 vidas:
                // i = 0 (0 < 2) -> true (Activo)
                // i = 1 (1 < 2) -> true (Activo)
                // i = 2 (2 < 2) -> false (Desactivado)
                bool activar = (i < vidasActuales);
                corazones[i].SetActive(i < vidasActuales);
                Debug.Log($"[HUD] Corazón [{i}] ({corazones[i].name}) -> SetActive({activar})");
            }
            else
            {
                Debug.LogError($"[HUD] El corazón en la posición [{i}] es NULL.");
            }
        }
    }

}
