using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Boton_UI : MonoBehaviour
{
    public ControladorVistaNivel controladorVista;
    public bool Cae = false;
    public float Segundos = 0.2f;
    public Rigidbody2D Cuerpo;
    public UnityAction AccionBoton;

    private void Start()
    {
        if (transform.parent.gameObject.name.Equals("Boton Jugar"))
            AccionBoton = controladorVista.controlador.CargarNiveles;
        else if (transform.parent.gameObject.name.Equals("Boton Restablecer"))
            AccionBoton = controladorVista.controlador.Restablecer;
        else if (transform.parent.gameObject.name.Equals("Boton Opciones"))
            AccionBoton = controladorVista.controlador.Opciones;
        else if (transform.parent.gameObject.name.Equals("Boton Creditos"))
            AccionBoton = controladorVista.controlador.Creditos;
        else if (transform.parent.gameObject.name.Equals("Boton Salir"))
            AccionBoton = controladorVista.controlador.Salir;
        else if (transform.parent.gameObject.name.Equals("Boton Nivel 1"))
            AccionBoton = controladorVista.controlador.Nivel1;
        else if (transform.parent.gameObject.name.Equals("Boton Nivel 2"))
            AccionBoton = controladorVista.controlador.Nivel2;
        else if (transform.parent.gameObject.name.Equals("Boton Volver Principal"))
            AccionBoton = controladorVista.controlador.MenuPrincipal;
        else if (transform.parent.gameObject.name.Equals("Boton Reiniciar"))
            AccionBoton = controladorVista.controlador.Reiniciar;
        else if (transform.parent.gameObject.name.Equals("Boton Reiniciar Inicio"))
            AccionBoton = controladorVista.controlador.ReiniciarInicio;

        else if (transform.parent.gameObject.name.Equals("Boton Pausa"))
            AccionBoton = controladorVista.controlador.Pausa;        
        else if (transform.parent.gameObject.name.Equals("Boton Volver Juego"))
            AccionBoton = controladorVista.controlador.Reanudar;
        else
            AccionBoton = null;

        if (AccionBoton != null)
            transform.parent.GetComponent<Button>().onClick.AddListener(AccionBoton);
        else
            print("Boton No Asignado, [" + transform.parent.gameObject.name + "]");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Player"))
        {
            if (Cae)
                Cuerpo.bodyType = RigidbodyType2D.Dynamic;

            StartCoroutine(Despues());

            IEnumerator Despues()
            {
                yield return new WaitForSecondsRealtime(1f);
                transform.parent.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
