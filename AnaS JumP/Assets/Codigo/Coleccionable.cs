using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
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
            if (SceneManager.GetActiveScene().name.StartsWith("Nivel"))
                controlador.FrutaNivelRecogida = FrutaID;
            else
                controlador.RecogerFruta(FrutaID);

            Destroy(gameObject);
        }
    }
}
