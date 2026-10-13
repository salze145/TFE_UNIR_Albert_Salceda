using System;
using UnityEngine;

public class Frutas : MonoBehaviour
{


    public static Action<int> ObjetoRecolectado;

    [SerializeField] private int valorPuntos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Recolectar();
        }
    }

    private void Recolectar()
    {
        ObjetoRecolectado?.Invoke(valorPuntos);
        Destroy(gameObject);
    }


}
