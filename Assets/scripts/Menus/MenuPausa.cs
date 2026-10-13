using UnityEngine;
using System.Collections.Generic;


public class MenuPausa : MonoBehaviour
{
    
    private bool estaEnPausa;
    public GameObject PanelPausa;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(estaEnPausa)
                JuegoRetomado();
            else
                JuegoPausado();
               
        }

        if (Input.GetButtonDown("Submit"))
        { 
                JuegoRetomado();
           
        }

    }

    public void JuegoPausado()
    {
        Time.timeScale = 0;
        PanelPausa.SetActive(true);
        estaEnPausa = true;
    }

    public void JuegoRetomado()
    {
        Time.timeScale = 1;
        PanelPausa.SetActive(false);
        estaEnPausa = false;
    }

    public void Salir()
    {
        Application.Quit();
    }

}
