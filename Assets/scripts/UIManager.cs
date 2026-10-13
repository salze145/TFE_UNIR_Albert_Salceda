using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Referencias de la Escena Local")]
    public HUD hudActual;
    public GameObject GameOver;

    private void Awake()
    {
        // El panel empieza oculto al cargar la escena
        if (GameOver != null)
        {
            GameOver.SetActive(false);
        }
    }
}
