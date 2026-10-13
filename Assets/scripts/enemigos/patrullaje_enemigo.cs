using UnityEngine;
using UnityEngine.Rendering;

public class patrullaje_enemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorFrente;
    [SerializeField] private Transform controladorFrente2;
    // [SerializeField] private float distancia;
    [SerializeField] private bool moviendoDerecha;
    [SerializeField] private bool tocandoSueloFrente;
    [SerializeField] private bool tocandoFrente;
    [SerializeField] private float distanciaRayoFrente;
    [SerializeField] private float distanciaFrente;
    [SerializeField] private LayerMask capasSuelo;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        tocandoSueloFrente = Physics2D.Raycast(controladorFrente.position, transform.right * -1, distanciaRayoFrente, capasSuelo);
        tocandoFrente = Physics2D.Raycast(controladorFrente2.position, transform.right * -1, distanciaFrente, capasSuelo);

    }

    private void FixedUpdate()
    { 
       // RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);

        rb2D.linearVelocity = new Vector2(velocidad, rb2D.linearVelocity.y);

        if (tocandoSueloFrente)
        {
            velocidad *= -1;
            Girar();
        }
        MirarDireccionMovimiento();


    }

    private void MirarDireccionMovimiento()
    {
        // RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);

      
        if ((velocidad > 0 && !MirandoDerecha()) || (velocidad < 0 && MirandoDerecha()))
        {
            Girar();
        }


    }


    private void Girar()
    {
        //gira el sprite del personaje actuando sobre localScale

       
        Vector3 rotacion = transform.eulerAngles;
        //operador ternarios, es lo mismo que un if-else pero en una sola linea
        rotacion.y = rotacion.y == 0 ? 180 : 0;
        transform.eulerAngles = rotacion;


    }

    private bool MirandoDerecha()
    {
        return transform.eulerAngles.y == 180;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorFrente.position, controladorFrente.position + distanciaRayoFrente * transform.right * -1);
        Gizmos.DrawLine(controladorFrente.position, controladorFrente.position + distanciaRayoFrente * transform.up * -1);
    }
}
