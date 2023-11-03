using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorEscena : MonoBehaviour
{
    public string NombreEscena;
    public PlayerController JugadorControl;
    public CamaraControler CamaraControl;
    public LavaControler Lavacontrol;
    public Transform Canvas;

    public int VidasJugador = 5;
    public int[] FrutasRecogidas;
    public Transform[] PuntosControl;
    public int Parte = 0;
    public int EstrellasRecolectadas = 0;
    public int FrutaNivelRecogida = -1;


    public float X = 15;
    public float Y = 9;

    private void Start()
    {
        FrutasRecogidas = new int[11];
        NombreEscena = SceneManager.GetActiveScene().name;
        Parte = PlayerPrefs.GetInt("Parte.Persite", 0);

        if (Parte < PuntosControl.Length)
        {
            JugadorControl.PuntoAparicion = PuntosControl[Parte].position;
        }
        else
        {
            Debug.LogWarning("Parte no existe");
            JugadorControl.PuntoAparicion = PuntosControl[0].position;
        }

        JugadorControl.transform.position = JugadorControl.PuntoAparicion;

        ActualizaCorazones();
        ActualizaFrutas();
    }
    void Update()
    {
        Desplazar();
    }


    void Desplazar()
    {
        if (JugadorControl.transform.position.x > CamaraControl.transform.position.x + X - 1)
        {
            CamaraControl.transform.position += Vector3.right * (X * 2);
            Lavacontrol.transform.position = new Vector3(CamaraControl.transform.position.x, 0, 0);
            JugadorControl.PuntoAparicion += Vector2.right * (X * 2);
        }
        if (JugadorControl.transform.position.x < CamaraControl.transform.position.x - X - 1)
        {
            CamaraControl.transform.position += Vector3.left * (X * 2);
            Lavacontrol.transform.position = new Vector3(CamaraControl.transform.position.x, 0, 0);
            JugadorControl.PuntoAparicion += Vector2.left * (X * 2);
        }
    }

    public void RecogerFruta(int FrutaRecogida)
    {
        FrutaNivelRecogida = FrutaRecogida;
        PlayerPrefs.SetInt("Fruta" + FrutaRecogida + ".Persite", 1);
        ActualizaFrutas();
    }
    public void ActualizaFrutas()
    {
        Transform TFrutas = Canvas.Find("Frutas Coleccion");
        if (TFrutas!= null)
        {
            for (int i = 0; i < FrutasRecogidas.Length; i++)
            {
                FrutasRecogidas[i] = PlayerPrefs.GetInt("Fruta" + i + ".Persite", 0);
                if (FrutasRecogidas[i] == 1)
                    TFrutas.GetChild(i).gameObject.SetActive(true);
                else
                    TFrutas.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ActualizaCorazones()
    {
        VidasJugador = PlayerPrefs.GetInt("Vidas.Persite", 0);

        Transform Tcorazones = Canvas.Find("Panel Corazones");
        if(Tcorazones != null)
        {
            print(Tcorazones.gameObject.name);
            for (int i = 0; i < Tcorazones.childCount; i++)
            {
                if (i < VidasJugador)
                    Tcorazones.GetChild(i).gameObject.SetActive(true);
                else
                    Tcorazones.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ComandoVolverPrincipal()
    {
        SceneManager.LoadScene("Menu_Principal");
    }

    public void Muerto()
    {
        if (FrutaNivelRecogida > 0)
            PlayerPrefs.SetInt("Fruta" + FrutaNivelRecogida + ".Persite", 0);
        ActualizaFrutas();

        if (VidasJugador > 1)
        {
            VidasJugador--;
            PlayerPrefs.SetInt("Vidas.Persite", VidasJugador);
            Reintentar();
        }
        else if (SceneManager.GetActiveScene().name.StartsWith("Nivel"))
        {
            PlayerPrefs.SetInt("Parte.Persite", 0);
            SceneManager.LoadScene("Menu_Niveles");
        }
        else
        {
            PlayerPrefs.SetInt("Parte.Persite", 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void Reintentar()
    {
        if(SceneManager.GetActiveScene().name.StartsWith("Nivel"))
            StartCoroutine(Despues());
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        IEnumerator Despues()
        {
            Transform T = Canvas.Find("Panel Muerto");
            Time.timeScale = 0.025f;

            T.Find("Texto del boton").gameObject.GetComponent<Text>().text = VidasJugador + " Vidas Restantes";
            T.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);

            Time.timeScale = 1;
            PlayerPrefs.SetInt("Parte.Persite", Parte);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            T.gameObject.SetActive(false);
        }
    }
    public void HazGanado()
    {
        Debug.Log("Haz Ganado");
    }


    public void Restablecer()
    {
        PlayerPrefs.SetInt("Vidas.Persite", 5);

        for (int i = 0; i < FrutasRecogidas.Length; i++)
            PlayerPrefs.SetInt("Fruta" + i + ".Persite", 0);
        ActualizaFrutas();
    }
    public void MenuPrincipal()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Parte.Persite", 0);
        SceneManager.LoadScene("Menu_Principal");
    }
    public void Jugar()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Parte.Persite", 0);
        SceneManager.LoadScene("Menu_Niveles");
    }
    public void Opciones()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Parte.Persite", 0);
        SceneManager.LoadScene("Menu_Opciones");
    }
    public void Creditos()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Parte.Persite", 0);
        SceneManager.LoadScene("Creditos");
    }
    public void Salir()
    {
        print("Saliendo ...");
        Application.Quit();
    }

    public void Nivel1()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Parte.Persite", 0);
        PlayerPrefs.SetInt("Vidas.Persite", 5);
        SceneManager.LoadScene("Nivel_1");
    }
    public void Nivel2()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Parte.Persite", 0);
        PlayerPrefs.SetInt("Vidas.Persite", 5);
        SceneManager.LoadScene("Nivel_2");
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReiniciarInicio()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Vidas.Persite", 5);
        PlayerPrefs.SetInt("Parte.Persite", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Pausa()
    {
        Time.timeScale = 0;
        Transform BotonPausa = Canvas.Find("Boton Pausa");
        Transform Panel = Canvas.Find("Panel Pausa");
        BotonPausa.gameObject.SetActive(false);
        Panel.gameObject.SetActive(true);
    }
    public void Reanudar()
    {
        Time.timeScale = 1;
        Transform BotonPausa = Canvas.Find("Boton Pausa");
        Transform Panel = Canvas.Find("Panel Pausa");
        BotonPausa.gameObject.SetActive(true);
        Panel.gameObject.SetActive(false);
    }
}
