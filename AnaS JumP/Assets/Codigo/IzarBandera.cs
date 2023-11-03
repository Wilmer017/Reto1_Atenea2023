using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IzarBandera : MonoBehaviour
{
    public ControladorEscena Controlador;
    public Animator AnimadorBandera;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.name.StartsWith("Player"))
            {
                AnimadorBandera.enabled = true;
                Controlador.HazGanado();
                StartCoroutine(DespuesDeGanarMenu());
            }
        }
    }

    IEnumerator DespuesDeGanarMenu()
    {
        yield return new WaitForSecondsRealtime(4f);
        Controlador.ComandoVolverPrincipal();
    }
}
