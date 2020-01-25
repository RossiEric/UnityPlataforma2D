using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gif_UI : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteToText;
    [SerializeField] private Sprite[] spriteToAction;
    [SerializeField] private float fps = 10f;
    public bool ActionOrText;

    private Image spriteActive;

    void Start()
    {
        spriteActive = GetComponent<Image>();
    }

    void Update()
    {
        int index = (int)(Time.unscaledDeltaTime * fps);

        if (!ActionOrText)
        {
            //continuar texto
            index = index % spriteToText.Length;
            spriteActive.sprite = spriteToText[index]; // usar en planeObjects
        }
        else
        {
            //efetivar ação
            index = index % spriteToAction.Length;
            spriteActive.sprite = spriteToAction[index]; // usar en planeObjects
        }
    }
}
