using UnityEngine;
using TMPro;

public class ControlPuntos : MonoBehaviour
{
    public int puntuacion = 0;
    public TMP_Text puntuacionTexto;
    //public static ControlPuntos Instance;


    /*
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }
    
    */
   

    void Start()
    {
      puntuacion = 0;
    }


    public void ActualizarPuntuacion(int puntos)
    {
        puntuacion += puntos;
        puntuacionTexto.text = puntuacion.ToString("0");
        //Debug.Log(puntos);
        Debug.Log(puntuacion);
    }
}
