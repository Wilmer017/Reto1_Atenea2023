using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Boton_UI : MonoBehaviour
{
    public ControladorEscena controlador;
    public bool Cae = false;
    public float Segundos = 0.2f;
    public Rigidbody2D Cuerpo;
    public UnityAction AccionBoton;

    private void Start()
    {
        if (transform.parent.gameObject.name.Equals("Boton Jugar"))
            AccionBoton = controlador.Jugar;
        else if (transform.parent.gameObject.name.Equals("Boton Restablecer"))
            AccionBoton = controlador.Restablecer;
        else if (transform.parent.gameObject.name.Equals("Boton Opciones"))
            AccionBoton = controlador.Opciones;
        else if (transform.parent.gameObject.name.Equals("Boton Creditos"))
            AccionBoton = controlador.Creditos;
        else if (transform.parent.gameObject.name.Equals("Boton Salir"))
            AccionBoton = controlador.Salir;
        else if (transform.parent.gameObject.name.Equals("Boton Nivel 1"))
            AccionBoton = controlador.Nivel1;
        else if (transform.parent.gameObject.name.Equals("Boton Nivel 2"))
            AccionBoton = controlador.Nivel2;
        else if (transform.parent.gameObject.name.Equals("Boton Volver Principal"))
            AccionBoton = controlador.MenuPrincipal;
        else if (transform.parent.gameObject.name.Equals("Boton Reiniciar"))
            AccionBoton = controlador.Reiniciar;
        else if (transform.parent.gameObject.name.Equals("Boton Reiniciar Inicio"))
            AccionBoton = controlador.ReiniciarInicio;

        else if (transform.parent.gameObject.name.Equals("Boton Pausa"))
            AccionBoton = controlador.Pausa;        
        else if (transform.parent.gameObject.name.Equals("Boton Volver Juego"))
            AccionBoton = controlador.Reanudar;
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

            if (AccionBoton != null)
                StartCoroutine(Despues(AccionBoton));

            IEnumerator Despues(UnityAction Accion)
            {
                yield return new WaitForSecondsRealtime(1f);
                Accion.Invoke();
            }
        }
    }
}
