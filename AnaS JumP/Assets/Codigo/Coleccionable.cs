using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    public ControladorEscena controlador;

    public int FrutaRecogida = 0;
    public int FrutaID;

    public void Start()
    {
        FrutaRecogida = PlayerPrefs.GetInt("Fruta" + FrutaID + ".Persite", 0);

        if (FrutaRecogida == 1)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            controlador.RecogerFruta(FrutaID);
            Destroy(gameObject);
        }
    }
}
