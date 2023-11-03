using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaControler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.name.StartsWith("Piedra"))
            {
                transform.parent.parent.transform.position += Vector3.up * 0.15f;
            }
        }
    }
}
