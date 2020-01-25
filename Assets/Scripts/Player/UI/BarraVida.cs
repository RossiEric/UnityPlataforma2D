using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    Image Barra;
    float Resultado;
    //BarraVida.Vida -= 10f;

    // Start is called before the first frame update
    void Start()
    {
        Barra = GetComponent<Image>();
    }

    public void BarInicialAjuste(float MaxVida, float damage)
    {

        Resultado = damage / MaxVida;

        Debug.Log(Resultado.ToString());
        Barra.fillAmount = (Resultado);
    }

    public void BarDamage(float MaxVida, float damage) {

        Resultado = damage / MaxVida;

        Debug.Log(Resultado.ToString());
        Barra.fillAmount -= (Resultado);
    }
}
