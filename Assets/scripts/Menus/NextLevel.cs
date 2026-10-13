using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class NextLevel : MonoBehaviour
{

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

           
            SiguienteNivel();

        }
    }


    public void SiguienteNivel()
    {
        // aqui tendré que comprobar que no sea el ultimo nivel. si lo fuera mostrariamos la última escena de victoria!!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
