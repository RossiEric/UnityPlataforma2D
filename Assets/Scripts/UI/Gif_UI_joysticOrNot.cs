using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gif_UI_joysticOrNot : MonoBehaviour
{
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private Sprite[] spriteJoystick;
    [SerializeField] private float fps = 10f;

    private JoystickOrNot temController;
    private Image spriteActive;

    void Start()
    {
        spriteActive = GetComponent<Image>();
        temController = GameObject.Find("MainGame").GetComponent<JoystickOrNot>();
    }

    void Update()
    {
        int index = (int)(Time.time * fps);
        index = index % sprite.Length;
        
        //usa icone controle ou teclado
        if (temController.TemControllerativo)
        {
            spriteActive.sprite = spriteJoystick[index]; // usar en planeObjects
        }
        else
        {
            spriteActive.sprite = sprite[index]; // usar en planeObjects
        }

    }
}
