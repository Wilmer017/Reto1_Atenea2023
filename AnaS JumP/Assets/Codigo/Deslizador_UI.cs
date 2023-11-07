using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deslizador_UI : MonoBehaviour
{
    public Slider DeslizadorControl;

    public void Aumentar()
    {
        print("+");
        DeslizadorControl.value ++;
    }

    public void Disminuir()
    {
        print("-");
        DeslizadorControl.value --;
    }
}
