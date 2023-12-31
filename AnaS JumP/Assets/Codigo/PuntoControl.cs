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
                if (controlador.Parte < PartePuntoControl)
                {
                    controlador.Parte = PartePuntoControl;

                    if(controlador.FrutaNivelRecogida != -1)
                        controlador.RecogerFruta(controlador.FrutaNivelRecogida);

                    if (controlador.controladorNivel != null)
                        controlador.controladorNivel.ReiniciarTiempo();
                }

                if (SiguienteNivel)
                {
                    PlayerPrefs.SetInt("Parte.Persite", 0);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
    }
}
