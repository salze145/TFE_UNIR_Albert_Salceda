using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private AudioClip colectar1;
    

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ControladorAudio.Instance.EjecutarSonido(colectar1);
            Destroy(gameObject);
        }
    }
}
