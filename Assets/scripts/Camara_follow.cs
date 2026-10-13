using UnityEngine;

public class Camara_follow : MonoBehaviour
{
    //es el objetivo de la cámara
    public Transform target;
    [Tooltip("Velocidad de reacción de la cámara. Valores entre 3 y 10 dan un movimiento fluido.")]
    public float velocidadSuavizado = 5f;
    public Vector3 desplazamiento = new Vector3(0f, 0f, -10f);
    [Header("Límites del Nivel (Bounds)")]
    [Tooltip("Mantiene activada la restricción de la cámara a los límites del mapa.")]
    public bool usarLimites = true; // Activo por defecto
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        Debug.Log(">>> SCRIPT DE CÁMARA INICIADO EN: " + gameObject.name);
    }

   
    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogError(">>> LA CÁMARA NO TIENE TARGET ASIGNADO");
            return;
        }

        // 1. Definimos la posición final donde debe llegar la cámara
        Vector3 posicionDeseada = target.position + desplazamiento;

        // 2. Si usarLimites está activo, restringimos X e Y con Mathf.Clamp
        if (usarLimites)
        {
            posicionDeseada.x = Mathf.Clamp(posicionDeseada.x, minX, maxX);
            posicionDeseada.y = Mathf.Clamp(posicionDeseada.y, minY, maxY);
        }

        // 2. Interpolación suave adaptada al tiempo de frame (Time.deltaTime)
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuavizado * Time.deltaTime);

        // 3. Aplicamos la nueva posición
        transform.position = posicionSuavizada;

        
    }

    // Dibujar el área de límites en la ventana Scene para facilitar el ajuste
    private void OnDrawGizmosSelected()
    {
        if (!usarLimites) return;

        Gizmos.color = Color.green;
        Vector3 centro = new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f);
        Vector3 tamaño = new Vector3(maxX - minX, maxY - minY, 1f);
        Gizmos.DrawWireCube(centro, tamaño);
    }

}

