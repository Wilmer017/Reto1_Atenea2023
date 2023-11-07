using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEscena : MonoBehaviour
{
    public string NombreEscena;
    public PlayerController JugadorControl;
    public CamaraControler CamaraControl;
    public LavaControler Lavacontrol;
    public Transform CanvasEscena;
    public ControladorNivel controladorNivel;

    public int[] FrutasRecogidas;
    public Transform[] PuntosControl;

    public int Parte = 0;
    public int EstrellasRecolectadas = 0;
    public int FrutaNivelRecogida = 0;


    public float X = 15;
    public float Y = 9;

    private void Start()
    {
        if(controladorNivel == null)
            PlayerPrefs.SetInt("Vidas.Persite", 1);
        else
        {
            int Vida = PlayerPrefs.GetInt("Vidas.Persite", 0);
            if (Vida == 0)
                PlayerPrefs.SetInt("Vidas.Persite", controladorNivel.VidasJugador);
        }


        FrutasRecogidas = new int[20];
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

        ActualizaFrutas();

        if(controladorNivel != null)
            controladorNivel.ActualizaCorazones();
    }
    void Update()
    {
        Desplazar();

        if(controladorNivel != null)
            controladorNivel.Cronometro();
    }


    void Desplazar()
    {
        bool SeMovio = false;
        Vector3 PosicionJugador = JugadorControl.transform.position;
        Vector3 PosicionCamara = CamaraControl.transform.position;

        if (PosicionJugador.x > PosicionCamara.x + X - 1)
        {
            SeMovio = true;
            CamaraControl.transform.position += Vector3.right * (X * 2);
        }
        else if (PosicionJugador.x < PosicionCamara.x - X - 1)
        {
            SeMovio = true;
            CamaraControl.transform.position += Vector3.left * (X * 2);
        }
        if (PosicionJugador.y > PosicionCamara.y + Y - 1)
        {
            SeMovio = true;
            CamaraControl.transform.position += Vector3.up * (Y * 2);
        }
        else if (PosicionJugador.y < PosicionCamara.y - Y - 1)
        {
            SeMovio = true;
            CamaraControl.transform.position += Vector3.down * (Y * 2);
        }

        if(SeMovio)
        {
            Lavacontrol.transform.position = new Vector3(CamaraControl.transform.position.x, 0, 0);
        }
    }

    public void RecogerFruta(int FrutaRecogida)
    {
        PlayerPrefs.SetInt("Fruta" + FrutaRecogida + ".Persite", 1);
        ActualizaFrutas();
    }
    public void ActualizaFrutas()
    {
        for (int i = 0; i < FrutasRecogidas.Length; i++)
        {
            FrutasRecogidas[i] = PlayerPrefs.GetInt("Fruta" + i + ".Persite", 0);
        }

        Transform TFrutas = CanvasEscena.Find("Frutas Coleccion");
        if (TFrutas!= null)
        {
            for (int i = 0; i < FrutasRecogidas.Length; i++)
            {
                if (FrutasRecogidas[i] == 1)
                    TFrutas.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                else
                    TFrutas.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    public void ComandoVolverPrincipal()
    {
        SceneManager.LoadScene("Menu_Principal");
    }

    public void Herido(string CausaMuerte)
    {
        JugadorControl.Vivo = false;

        if (controladorNivel != null)
        {
            controladorNivel.CausaMuerte = CausaMuerte;
            controladorNivel.DescontarVidas();
        }
        else
            Reiniciar();
    }
    public void HazGanado()
    {
        if (controladorNivel != null && JugadorControl.Vivo == true)
        {
            controladorNivel.NivelSuperado();
        }
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
    public void CargarNiveles()
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
        PlayerPrefs.SetInt("Parte.Persite", Parte);
        if (controladorNivel != null)
            controladorNivel.ReiniciarTiempo();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReiniciarInicio()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Vidas.Persite", 0);
        PlayerPrefs.SetInt("Parte.Persite", 0);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Pausa()
    {
        Time.timeScale = 0;
        Transform BotonPausa = CanvasEscena.Find("Boton Pausa");
        Transform Panel = CanvasEscena.Find("Panel Pausa");
        BotonPausa.gameObject.SetActive(false);
        Panel.gameObject.SetActive(true);
    }
    public void Reanudar()
    {
        Time.timeScale = 1;
        Transform BotonPausa = CanvasEscena.Find("Boton Pausa");
        Transform Panel = CanvasEscena.Find("Panel Pausa");
        BotonPausa.gameObject.SetActive(true);
        Panel.gameObject.SetActive(false);
    }
}
