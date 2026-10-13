using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//import system;


public class Items : MonoBehaviour
{

    [SerializeField] int puntosItem;
   // private HUD controlPuntos;
   
    void Start()
    {
        //controlPuntos = GameObject.Find("ControladorPuntos").GetComponent<ControlPuntos>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
        GameManager.Instance.SumarPuntos(puntosItem);
            //Debug.Log(cantidadPuntos);
            Destroy(gameObject, 0.1f);

        }
    }


}
