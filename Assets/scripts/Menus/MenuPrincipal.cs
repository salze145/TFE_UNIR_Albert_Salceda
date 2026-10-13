using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour

{
    //referencia a los objetos de paneles
    public GameObject PanelComoJugar;
    public GameObject PanelCreditos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }


    // activa el panel de como jugar y desactiva el de creditos por si acaso para evitar solapamiento
    public void ComoJugar()
    {
        PanelCreditos.SetActive(false);
        PanelComoJugar.SetActive(true);
    }

    // activa el panel de creditos y desactiva el de cómo jugar por si acaso para evitar solapamiento
    public void Creditos()
    {
        PanelComoJugar.SetActive(false);
        PanelCreditos.SetActive(true);
      
    }


    // cierra paneles al apretar boton (x)
    public void CerrarPaneles()
    {
        if (PanelCreditos != null) PanelCreditos.SetActive(false);
        if (PanelComoJugar != null) PanelComoJugar.SetActive(false);
    }

    // Update is called once per frame
    public void Salir()
    {
        // sale del juego, sólo funciona en el ejecutable del juego, no desde el unity
        Application.Quit();
    }
}
