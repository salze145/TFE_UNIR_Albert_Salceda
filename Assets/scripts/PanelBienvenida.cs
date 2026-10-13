using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class PanelBienvenida : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI textoTitulo;       
    public TextMeshProUGUI textoDescripcion;  
    public Button botonComenzar;

    [Header("Datos del Nivel Actual")]
    public string tituloNivel = "Nivel 1: La Tierra de los Hongos";
    [TextArea(3, 5)]
    public string descripcionNivel = "Debes ascender hacia arriba para poder escapar de la tierra de los hongos, enfrentándote al final con el jefe Mushroom y poder seguir tu viaje...";

    private void Awake()
    {
        // Rellenamos los textos del panel con los datos configurados
        if (textoTitulo != null) textoTitulo.text = tituloNivel;
        if (textoDescripcion != null) textoDescripcion.text = descripcionNivel;

        // Configuramos el botón para cerrar el panel
        if (botonComenzar != null)
        {
            botonComenzar.onClick.RemoveAllListeners();
            botonComenzar.onClick.AddListener(CerrarPanel);
        }
    }

    private void Start()
    {
        // Pausamos el tiempo del juego para parar todo
        Time.timeScale = 0f;
    }

    public void CerrarPanel()
    {
        // Reanudamos el tiempo de juego y ocultamos el panel
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
