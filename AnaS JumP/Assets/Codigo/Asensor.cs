using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asensor : MonoBehaviour
{
    public float Up = 4;
    public float Down = -1;
    public float left = 0;
    public float Right = 0;
    public float maxSpeedVertical = 1;
    public float maxSpeedHorizontal = 0;

    Vector2 Sentido;

    Rigidbody2D r2d;
    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        Sentido.x = 1;
        Sentido.y = 1;
    }
    private void Update()
    {
        if(maxSpeedVertical != 0)
        {
            if (transform.localPosition.y > Up)
                Sentido.y = -1;
            else if (transform.localPosition.y < Down)
                Sentido.y = 1;
        }
        if (maxSpeedHorizontal != 0)
        {
            if (transform.localPosition.x > Right)
                Sentido.x = -1;
            else if (transform.localPosition.x < left)
                Sentido.x = 1;
        }

        r2d.velocity = new Vector2(maxSpeedHorizontal * Sentido.x, maxSpeedVertical * Sentido.y);
    }
}
