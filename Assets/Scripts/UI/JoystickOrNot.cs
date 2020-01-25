using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickOrNot : MonoBehaviour
{
    private bool PrimeiraVerificacao;
    public bool TemControllerativo;
    public string[] JoystickNamesList;

    // Start is called before the first frame update
    void Start()
    {
        //Controleativo();
    }

    void Update()
    {
        //Controleativo();
    }

    public void Controleativo()
    {
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Debug.Log("GetJoystickNames ::: " + temp.Length.ToString());

        if (JoystickNamesList.Length != temp.Length || PrimeiraVerificacao == false)
        {
            Debug.Log("GetJoystickNames ::: " + temp.Length.ToString());

            PrimeiraVerificacao = true;

            Debug.Log("GetJoystickNames MUDOU");

            JoystickNamesList = temp;

            //Check whether array contains anything
            if (JoystickNamesList.Length > 0)
            {
                TemControllerativo = true;
            }
            else
            {
                TemControllerativo = false;
            }

        }
    }
}
