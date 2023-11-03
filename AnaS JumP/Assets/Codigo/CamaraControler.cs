using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControler : MonoBehaviour
{
    public PlayerController Player;
    public Transform Lava;

    void Start()
    {
        transform.position = Vector2.zero;
    }
}
