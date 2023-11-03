using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class PiedrasCaen : MonoBehaviour
{
    public float Segundos = 2;
    public Rigidbody2D CuerpoPiedras;

    BoxCollider2D b2d;

    void Start()
    {
        b2d = GetComponent<BoxCollider2D>();
        b2d.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            StartCoroutine("TiemblanPiedras");
        }
    }

    IEnumerator TiemblanPiedras()
    {
        yield return new WaitForSecondsRealtime(Segundos);
        CaerPiedras();
    }
    void CaerPiedras()
    {
        CuerpoPiedras.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject);
    }
}
