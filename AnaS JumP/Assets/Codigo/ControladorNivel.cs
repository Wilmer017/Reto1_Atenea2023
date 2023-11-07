using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControladorNivel : MonoBehaviour
{
    public ControladorEscena controlador;
    public int VidasJugador = 5;
    public string CausaMuerte = "Has tocado Lava";
    public float SegundosNivel = 30;
    public float TiempoTrasncurrido = 30;
    Text TTime;

    private void Start()
    {
        TTime = controlador.CanvasEscena.Find("Panel Tiempo").Find("Texto del Panel").gameObject.GetComponent<Text>();
    }
    public void ActualizaCorazones()
    {
        VidasJugador = PlayerPrefs.GetInt("Vidas.Persite", 0);

        Transform Tcorazones = controlador.CanvasEscena.Find("Panel Corazones");
        if (Tcorazones != null)
        {
            for (int i = 0; i < Tcorazones.childCount; i++)
            {
                if (i < VidasJugador)
                    Tcorazones.GetChild(i).gameObject.SetActive(true);
                else
                    Tcorazones.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    public void DescontarVidas()
    {

        VidasJugador--;
        PlayerPrefs.SetInt("Vidas.Persite", VidasJugador);

        if (VidasJugador > 0)
        {
            StartCoroutine(MostrarVidas());
        }
        else
        {
            StartCoroutine(MostrarHazPerdido());
        }
    }
    public void Cronometro()
    {
        if (TiempoTrasncurrido > 0)
        {
            TiempoTrasncurrido -= Time.deltaTime;
            TimeSpan tiempoEnFormato = TimeSpan.FromSeconds(TiempoTrasncurrido);
            TTime.text = tiempoEnFormato.ToString("mm':'ss");
        }
        else
        {
            controlador.Herido("Tiempo Agotado");
            ReiniciarTiempo();
        }
    }

    public void NivelSuperado()
    {
        PlayerPrefs.SetInt("Vidas.Persite", 5);
        StartCoroutine(MostrarHazGanado());
    }
    public void ReiniciarTiempo()
    {
        TiempoTrasncurrido = SegundosNivel;
    }

    IEnumerator MostrarVidas()
    {
        ActualizaCorazones();

        Transform T = controlador.CanvasEscena.Find("Panel Muerto");
        controlador.JugadorControl.gameObject.name = "Eliminado";
        Time.timeScale = 0.1f;

        string inf = " Vidas Restantes";
        if (VidasJugador == 1)
            inf = " Vida Restante";

        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = CausaMuerte + "\n" + VidasJugador + inf + "\n\n   ";
        T.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1);
        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = CausaMuerte + "\n" + VidasJugador + inf + "\n\n Reiniciando 3 ";
        yield return new WaitForSecondsRealtime(1);
        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = CausaMuerte + "\n" + VidasJugador + inf + "\n\n Reiniciando 2 ";
        yield return new WaitForSecondsRealtime(1);
        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = CausaMuerte + "\n" + VidasJugador + inf + "\n\n Reiniciando 1 ";
        yield return new WaitForSecondsRealtime(1);
        controlador.Reiniciar();
    }

    IEnumerator MostrarHazPerdido()
    {
        ActualizaCorazones();

        Transform T = controlador.CanvasEscena.Find("Panel Muerto");
        controlador.JugadorControl.gameObject.name = "Eliminado";
        Time.timeScale = 0.1f;

        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = CausaMuerte + "\n Ya no tienes vidas " + "\n\n   ";
        T.Find("Boton Reiniciar").gameObject.SetActive(false);
        T.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1);
        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = CausaMuerte + "\n Ya no tienes vidas " + "\n\n 3 ";
        yield return new WaitForSecondsRealtime(1);
        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = CausaMuerte + "\n Ya no tienes vidas " + "\n\n 2 ";
        yield return new WaitForSecondsRealtime(1);
        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = CausaMuerte + "\n Ya no tienes vidas " + "\n\n 1 ";
        yield return new WaitForSecondsRealtime(1);
        controlador.CargarNiveles();
    }

    IEnumerator MostrarHazGanado()
    {
        ActualizaCorazones();

        Transform T = controlador.CanvasEscena.Find("Panel Muerto");
        controlador.JugadorControl.gameObject.name = "Eliminado";
        Time.timeScale = 0.1f;

        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = " Haz Superado el nivel " + "\n\n   ";
        T.Find("Boton Reiniciar").gameObject.SetActive(false);
        T.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1);
        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = " Haz Superado el nivel " + "\n\n 3 ";
        yield return new WaitForSecondsRealtime(1);
        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = " Haz Superado el nivel " + "\n\n 2 ";
        yield return new WaitForSecondsRealtime(1);
        T.Find("Texto del boton").gameObject.GetComponent<Text>().text = " Haz Superado el nivel " + "\n\n 1 ";
        yield return new WaitForSecondsRealtime(1);
        controlador.CargarNiveles();
    }
}
