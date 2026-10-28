using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class patrullaje : MonoBehaviour
{

    public float velocidad;
    public Transform ray;
    public Transform ray2;
    public float groundDistance;
    public float frontDistance;
    public LayerMask suelo;
    private bool mirandoDerecha = true;

    private Rigidbody2D RB2D;

    // Evita bloqueos y giros en los bordes
    public float cdGiro = 0.25f; 
    private float tiempoProximoGiro;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RB2D.linearVelocity = new Vector2(transform.right.x *  velocidad, RB2D.linearVelocity.y);
        if (!isGrounded())
        {
            Flip();
            
        }
        else if (Time.time >= tiempoProximoGiro && isFront())
        //if (isFront())
        {
            Flip();
            //velocidad *= -1;
        }


    }

    private void Flip()
    {
        //mirandoDerecha = !mirandoDerecha; 
       
        transform.Rotate(0, 180, 0);
        tiempoProximoGiro = Time.time + cdGiro;
    }

    private bool isGrounded() { 

        if (ray == null) return true;
        RaycastHit2D hit = Physics2D.Raycast(ray.position, Vector2.down, groundDistance, suelo);

        // necesitamos comprobar si impactó contra algo de la capa 'suelo'
        return hit.collider != null;
    }

    private bool isFront()
    {

        if (ray2 == null) return false;
        // Determinamos el vector de dirección según hacia dónde esté mirando el enemigo
        Vector2 direccionPared = mirandoDerecha ? Vector2.right : Vector2.left;
        // Lanzamos el raycast horizontal desde 'ray' (o desde transform.position) hacia adelante
        //RaycastHit2D hit = Physics2D.Raycast(ray2.position, direccionPared, frontDistance, suelo);
        // transform.right apunta siempre hacia el frente real del rinoceronte, 
        // sin importar hacia dónde haya girado.
        RaycastHit2D hit = Physics2D.Raycast(ray2.position, direccionPared, frontDistance, suelo);

        //return hit.collider != null;


        return hit.collider != null;
    }

  
    
        private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.position, new Vector2(ray.position.x, ray.position.y - groundDistance));
        Gizmos.color = Color.blue;
        Vector2 direccionPared = mirandoDerecha ? Vector2.right : Vector2.left;
       // Vector2 direccionPared = Vector2.right;
        Gizmos.DrawLine(ray2.position, (Vector2)ray2.position + (direccionPared * frontDistance));
    }

}
