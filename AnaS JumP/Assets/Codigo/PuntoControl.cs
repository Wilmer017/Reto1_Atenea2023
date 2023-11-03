using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuntoControl : MonoBehaviour
{
    public ControladorEscena controlador;

    public int PartePuntoControl;
    public bool SiguienteNivel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
            if (collision.gameObject.name == "Player")
            {
                controlador.Parte = PartePuntoControl;
                if (SiguienteNivel)
                {
                    PlayerPrefs.SetInt("Parte.Persite", 0);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
    }
}
