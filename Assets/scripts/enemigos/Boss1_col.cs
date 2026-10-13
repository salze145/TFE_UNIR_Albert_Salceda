using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss1_col : MonoBehaviour
{
    
  
    public Player_mov movPlayer;
    public Player_respawn vidaPlayer;
    public int danyo;
    public HUD controlPuntos;
    [SerializeField] int cantidadPuntos;
    //vida del boss 1
    [SerializeField] int vidaBoss;
    private Collider2D enemyCollider;
    // fara efecto flash
    private SpriteRenderer spriteRenderer;
    private Material materialOriginal;
    private Material materialBlanco;
    public float duracionFlash = 0.2f; // Duración del blanco en segundos

    // panel vistoria
    public GameObject PanelVictoria;
    public float tiempoEsperaReiniciar = 2.5f; // Segundos en reiniciar

    public bool victoria = false;

    void Start()
    {
        //controlPuntos = GameObject.Find("ControladorPuntos").GetComponent<ControlPuntos>();
       // GameManager.Instance.SumarPuntos(cantidadPuntos);
        enemyCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
       
        if (spriteRenderer != null)
        {
            materialOriginal = spriteRenderer.material;
            // Creamos un material blanco temporal usando el Shader GUI/Text Shader (blanco sólido)
            materialBlanco = new Material(Shader.Find("GUI/Text Shader"));
        }
        if (PanelVictoria != null)
        {
            PanelVictoria.SetActive(false);
        }
    }


    //cuando colisiona con el player, le reduce una vida y hace un efecto de rebote
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //GameManager.Instance.PerderVida();
            movPlayer.contadorRebote = movPlayer.tiempoTotalRebote;
            
            if(collision.transform.position.x <= transform.position.x)
            {
                movPlayer.reboteDesdeDerecha = true;
            }
            if(collision.transform.position.x > transform.position.x)
            {
                movPlayer.reboteDesdeDerecha = false;
            }

            if (collision.otherCollider.transform != transform)
            {
                GameManager.Instance.PerderVida();
            }

        } 

    }

    //el boss 1 tiene 3 vidas, hay que darle 3 toques
    public void Muerte()
    {
        vidaBoss -= 1;
        StartCoroutine(EfectoFlashBlanco());
        if (vidaBoss <= 0)
            {

            Victoria();
            Debug.Log(vidaBoss);
            Collider2D[] todosLosColliders = GetComponentsInChildren<Collider2D>();
            foreach (Collider2D c in todosLosColliders)
            {
                c.enabled = false;
            }
            Destroy(gameObject, 1f);
            //controlPuntos.ActualizarPuntuacion(cantidadPuntos);
            GameManager.Instance.SumarPuntos(cantidadPuntos);
            
        }
       
        
    }

    public void HitPlayer(Transform playerTransform)
    {
       // int direccion = GetDirection(playerTransform);
       // Debug.Log(direccion);
       // FindAnyObjectByType<Player_mov>().rebotePlayer(rebotePlayer, direccion);
        
    }
    

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

    //rutina de flash de color blanco
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


    private void Victoria()
    {
        victoria = true;
        Debug.Log("ViCtoria!!!!");
        // Mostrar el mensaje "Has ganado"
        //PanelVictoria.SetActive(true); // muestra el panel de victoria
        
        if (PanelVictoria != null)
        {
            // Desactiva los controles/scripts del jugador para que no se mueva
            MonoBehaviour[] scriptsJugador = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scriptsJugador)
            {
                if (script != this) script.enabled = false;
            }
            PanelVictoria.SetActive(true); // muestra el panel de victoria
            //RutinaEsperar();
            RutinaReiniciarEscena();
        }
        

        
        
        

        // Inicia la cuenta atrás para reiniciar
        //StartCoroutine(RutinaEsperar());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator RutinaReiniciarEscena()
    {
        // Espera los segundos indicados
        yield return new WaitForSeconds(tiempoEsperaReiniciar);

        // Vuelve a cargar la escena activa actual
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
    }


    private IEnumerator RutinaEsperar()
    {
        yield return new WaitForSeconds(3f); // Espera 2 segundos
      
        Scene escenaActual = SceneManager.GetActiveScene();

    }
}
