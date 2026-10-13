using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_mov : MonoBehaviour
{
    
    
    public float velocidad = 5;
    private Rigidbody2D rb2D;
    private Animator animator;

    private float move;
    public float fuerzaSalto = 4;
    private bool estaEnSuelo;
    public Transform esSuelo;
    public LayerMask capaSuelo;
    public float radioSuelo = 0.1f;
    //private bool enRebote = false;

    public float fuerzaRebote;
    public float contadorRebote;
    public float tiempoTotalRebote;
    public bool reboteDesdeDerecha;
   

    [SerializeField] private AudioClip saltoSonido;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //si está rebotando ignora el movimiento
        if(contadorRebote <= 0){
            rb2D.linearVelocity = new Vector2(move * velocidad, rb2D.linearVelocity.y);
        }else
        {
            if(reboteDesdeDerecha == true)
            {
                rb2D.linearVelocity = new Vector2(-fuerzaRebote, fuerzaRebote);
            }
            if(reboteDesdeDerecha == false)
            {
                rb2D.linearVelocity = new Vector2(fuerzaRebote, fuerzaRebote);
            }
            contadorRebote -= Time.deltaTime;
        }
        //move es la variable que controla si se mueve en horizontal a izquierda y derecha
        move = Input.GetAxisRaw("Horizontal");
        
        //cambiamos a dónde mira el player según su dirección
        if(move != 0)
            transform.localScale =  new Vector3(Mathf.Sign(move), 1, 1);

        if(Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, fuerzaSalto);
            ControladorAudio.Instance.EjecutarSonido(saltoSonido);
        }

        //aqui se cambia la variable que invoca el cambio de la animación
        animator.SetFloat("Speed", Mathf.Abs(move));

    }

    // sirve para detectar si hay suelo debajo creando un pequeño círculo debajo del personaje
    private void FixedUpdate()
    {
        estaEnSuelo = Physics2D.OverlapCircle(esSuelo.position, radioSuelo, capaSuelo);

    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("pinchos"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //collision.transform.GetComponent<Player_respawn>().PlayerDamage();
        }
       
    }

    
}
