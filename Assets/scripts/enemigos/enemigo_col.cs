using System;
using UnityEngine;
using System.Collections;

public class enemigo_col : MonoBehaviour
{
    
    // Declaración de variables del enemigo
    // referencias a los escripts del player
    public Player_mov movPlayer;
    public Player_respawn vidaPlayer;
    public int danyo;
    //public HUD controlPuntos;
    [SerializeField] int cantidadPuntos;
  
    private Collider2D enemigoCol;
    private Rigidbody2D RB2D;
    // para el efecto hit blanco
    private SpriteRenderer spriteRenderer;
    private Material materialOriginal;
    private Material materialBlanco;
    public float duracionFlash = 0.15f; // Duración del blanco en segundos
    private bool estaMuerto = false;

    
    void Start()
    {
        //controlPuntos = GameObject.Find("HUD").GetComponent<ControlPuntos>();
        
        enemigoCol = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        RB2D = GetComponent<Rigidbody2D>();
        if (spriteRenderer != null)
        {
            materialOriginal = spriteRenderer.material;
            // Creamos un material blanco temporal usando el Shader GUI/Text Shader (blanco sólido)
            materialBlanco = new Material(Shader.Find("GUI/Text Shader"));
        }
    }


    //cuando colisiona con el player por los lados, le reduce una vida y hace un efecto de rebote
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
            
            movPlayer.contadorRebote = movPlayer.tiempoTotalRebote;
            
            if(collision.transform.position.x <= transform.position.x)
            {
                movPlayer.reboteDesdeDerecha = true;
            }
            if(collision.transform.position.x > transform.position.x)
            {
                movPlayer.reboteDesdeDerecha = false;
            }

            // 'collision.otherCollider' identifica exactamente cuál de los colliders
            // del enemigo hizo contacto con el jugador.
            // Verificamos que la colisión ocurrió con el cuerpo (objeto hijo).
            if (collision.otherCollider.transform != transform)
            {
                GameManager.Instance.PerderVida();
            }

        } 

    }

    // CONTROLA LA MUERTE DEL ENEMIGO
    public void Muerte()
    {
        
        //controlPuntos.ActualizarPuntuacion(cantidadPuntos);
        GameManager.Instance.SumarPuntos(cantidadPuntos);
        //StartCoroutine(EfectoFlashBlanco());
        //  quito la restricción de la z para que rote al caer
        RB2D.constraints = RigidbodyConstraints2D.None;
        // desactivo los dos colliders del enemigo para que caiga en el mapa hacia abajo y muera al cabo de 2 segundos
        Collider2D[] todosLosColliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D c in todosLosColliders)
        {
            c.enabled = false;
        }
        Debug.Log("enemigo sin col");

        Destroy(gameObject, 0.5f);
        Debug.Log("enemigo muerto");
    }

    public void HitPlayer(Transform playerTransform)
    {
       // int direccion = GetDirection(playerTransform);
       // Debug.Log(direccion);
       // FindAnyObjectByType<Player_mov>().rebotePlayer(rebotePlayer, direccion);
        
    }



        
    // nos da la dirección hacia donde mira el enemigo
    private int GetDirection(Transform playerTransform)
    {
        //esto significa que el enemigo está a la derecha del player
        if(transform.position.x > playerTransform.position.x)
        {
            //enemigo está a la derecha
            return -1;
        }
        else
        {
            return 1;
        }
    }

    private IEnumerator EfectoFlashBlanco()
    {
        if (spriteRenderer != null && materialBlanco != null)
        {
            // Cambiamos el material al blanco puro
            spriteRenderer.material = materialBlanco;

            yield return new WaitForSeconds(duracionFlash);

            // Volvemos al material normal con sus colores originales
            spriteRenderer.material = materialOriginal;
        }
    }
}
