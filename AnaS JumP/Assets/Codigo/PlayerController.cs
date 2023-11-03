using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    public string[] Mandos;

    public int JugadorId = 0;
    public ControladorEscena controlador;
    public float gravityScale = 2f;
    public float maxSpeed = 5f;
    public float jumpHeight = 11f;

    public float Corriendo = 0;
    public bool Saltando = false;
    public bool Transformando = false;

    public bool isGrounded;
    public bool isWall;

    public int PielActual = 1;
    public RuntimeAnimatorController Piel1;
    public RuntimeAnimatorController Piel2;

    public Vector2 PuntoAparicion = new Vector2(-12.5f , 5);

    Rigidbody2D r2d;
    SpriteRenderer e2d;
    Animator a2d;
    CapsuleCollider2D c2d;

    public enum OpcionControles{
        WASD = 0,
        Flechas = 1,
        Mando = 2
    }
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        e2d = GetComponent<SpriteRenderer>();
        a2d = GetComponent<Animator>();
        c2d = GetComponent<CapsuleCollider2D>();

        PielActual = 1;

        r2d.gravityScale = gravityScale;

        ActualizarMandos();
    }

    void ActualizarMandos()
    {
        Mandos = Input.GetJoystickNames();
        if (Mandos != null)
            if (Mandos.Length == 1)
                if (Mandos[0] == "")
                    print("Mando no Detectado");
                else
                    print("Mando : " + Mandos[0]);
    }
    void CambioPersonaje()
    {
        switch (PielActual)
        {
            default:
                a2d.runtimeAnimatorController = Piel1;
                maxSpeed = 5f;
                jumpHeight = 10f;

                PielActual = 1;
                break;
            case 1:
                a2d.runtimeAnimatorController = Piel2;
                maxSpeed = 3f;
                jumpHeight = 12f;

                PielActual ++;
                break;
        }
    }
    private void Update()
    {
        Transformando = Input.GetButtonDown("Boton Transformar " + JugadorId);

        if (Corriendo < -0.1f || 0.1f < Corriendo)
        {
            if (Corriendo < -0.1f) e2d.flipX = true;
            else if (Corriendo > 0.1f) e2d.flipX = false;

            if (!isWall)
            {
                r2d.velocity = new Vector2((Corriendo) * maxSpeed, r2d.velocity.y);
                a2d.SetBool("Correr", true);
            }
            else
                a2d.SetBool("Correr", false);

        }
        else
        {
            a2d.SetBool("Correr", false);
        }

        if (isGrounded)
        {
            if (Saltando)
                r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);

            a2d.SetBool("Saltar", false);
        }
        else
        {
            a2d.SetBool("Saltar", true);
        }

        if (Transformando)
        {
            CambioPersonaje();
        }
    }

    void FixedUpdate()
    {
        Corriendo = Input.GetAxis("Eje Correr " + JugadorId);
        Saltando = Input.GetButton("Boton Salto " + JugadorId);

        Collider2D PieDerecho = Physics2D.Raycast(transform.position + Vector3.right * 0.3f + Vector3.down * 0.88f , Vector2.down, 0.12f).collider;
        Collider2D PieIzquierdo = Physics2D.Raycast(transform.position + Vector3.left * 0.3f + Vector3.down * 0.88f, Vector2.down, 0.12f).collider;
        //Debug.DrawRay(transform.position + Vector3.right * 0.3f + Vector3.down * 0.88f, Vector2.down * 0.12f);
        //Debug.DrawRay(transform.position + Vector3.left * 0.3f + Vector3.down * 0.88f, Vector2.down * 0.12f);
        if (PieDerecho != null && PieIzquierdo != null)
        {
            if (PieDerecho.gameObject.name != "Detector" && PieDerecho.gameObject.name != "PuntoControl" ||
                PieIzquierdo.gameObject.name != "Detector" && PieIzquierdo.gameObject.name != "PuntoControl")
                isGrounded = true;
            if (PieDerecho.gameObject.name == "Magma" || PieIzquierdo.gameObject.name == "Magma")
                controlador.Muerto();
        }
        else if (PieDerecho != null && PieIzquierdo == null)
        {
            if (PieDerecho.gameObject.name != "Detector" && PieDerecho.gameObject.name != "PuntoControl")
                isGrounded = true;
            if (PieDerecho.gameObject.name == "Magma")
                controlador.Muerto();
        }
        else if (PieDerecho == null && PieIzquierdo != null)
        {
            if (PieIzquierdo.gameObject.name != "Detector" && PieIzquierdo.gameObject.name != "PuntoControl")
                isGrounded = true;
            if (PieIzquierdo.gameObject.name == "Magma")
                controlador.Muerto();
        }
        else
        {
            isGrounded = false;
        }

        Collider2D LadoArriba = Physics2D.Raycast(transform.position + Vector3.right * Corriendo * 0.43f + Vector3.up * 0.25f, Vector2.right, 0.12f * Corriendo).collider;
        Collider2D LadoAbajo = Physics2D.Raycast(transform.position + Vector3.right * Corriendo * 0.43f + Vector3.down * 0.7f, Vector2.right, 0.12f * Corriendo).collider;
        //Debug.DrawRay(transform.position + Vector3.right * Corriendo * 0.43f + Vector3.up * 0.25f, Vector2.right * 0.12f * Corriendo);
        //Debug.DrawRay(transform.position + Vector3.right * Corriendo * 0.43f + Vector3.down * 0.7f, Vector2.right * 0.12f * Corriendo);
        if (LadoArriba != null && LadoAbajo != null)
        {
            if (LadoArriba.gameObject.name != "Detector" && LadoArriba.gameObject.name != "PuntoControl" && !LadoArriba.gameObject.name.StartsWith("Player") ||
                LadoAbajo.gameObject.name != "Detector" && LadoAbajo.gameObject.name != "PuntoControl" && !LadoAbajo.gameObject.name.StartsWith("Player"))
                isWall = true;
        }
        else if (LadoArriba != null && LadoAbajo == null)
        {
            if (LadoArriba.gameObject.name != "Detector" && LadoArriba.gameObject.name != "PuntoControl" && !LadoArriba.gameObject.name.StartsWith("Player"))
                isWall = true;
        }
        else if (LadoArriba == null && LadoAbajo != null)
        {
            if (LadoAbajo.gameObject.name != "Detector" && LadoAbajo.gameObject.name != "PuntoControl" && !LadoAbajo.gameObject.name.StartsWith("Player"))
                isWall = true;
        }
        else
        {
            isWall = false;
        }
    }
}

