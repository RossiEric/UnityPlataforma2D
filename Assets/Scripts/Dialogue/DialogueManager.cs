using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

/// <summary>
/// https://www.youtube.com/watch?v=pxpS8i7awgM&list=PLLveWc5iDWNtpoJK_kmLmDEvHBjFRmBIa&index=3
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public GameObject[] buttons;
    public Text DisplayText;
    private string speacker;
    private JsonData Dialogo;
    private int JsonIndex;

    //
    public bool inDialogue;
    private JsonData currentLayer;

    //UI text box
    public GameObject TextBoxUI;
    public GameObject TextBoxUI_Icone;

    public bool LoadDialog(string Path)
    {
        if (!inDialogue)
        {
            JsonIndex = 0;
            var jsonTextFile = Resources.Load<TextAsset>("DialogSyst/textos/" + Path);
            Debug.Log(jsonTextFile.text);
            Dialogo = JsonMapper.ToObject(jsonTextFile.text);
            currentLayer = Dialogo;
            return true;
        }

        return false;
    }

    public bool PrintLine()
    {
        Debug.Log("linhas totais: " + Dialogo.Count.ToString());

        if (!inDialogue)
        {
            #region Start vars
            Time.timeScale = 0f;
            JsonData line = currentLayer[JsonIndex];
            //get name speacker
            foreach (var Key in line.Keys)
            {
                speacker = Key.ToString();
            }
            #endregion
            
            if (speacker == "Flag_END")
            {
                TextBoxUI_Icone.GetComponent<Gif_UI>().ActionOrText = false;
                TextBoxUI.SetActive(false);
                Time.timeScale = 1f;
                inDialogue = false;
                DisplayText.text = "";
                return false;
            }
            else if (speacker == "?")
            {
                TextBoxUI_Icone.GetComponent<Gif_UI>().ActionOrText = true;
                TextBoxUI.SetActive(true);
                JsonData options = line[0];
                DisplayText.text = "";

                for (int optionsNumber = 0; optionsNumber < options.Count; optionsNumber++)
                {
                    ativarBotao(buttons[optionsNumber], options[optionsNumber]);
                }
            }
            else
            {
                TextBoxUI_Icone.GetComponent<Gif_UI>().ActionOrText = false;
                TextBoxUI.SetActive(true);
                DisplayText.text = speacker + ": " + line[0].ToString();
                JsonIndex++;
            }
        }
        return true;
    }

    #region Buttons

    private void desativarBotoes()
    {
        foreach (var button in buttons)
        {
            button.SetActive(false);
            button.GetComponentInChildren<Text>().text = "";
            button.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    private void ativarBotao(GameObject button, JsonData choice)
    {
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text = choice[0][0].ToString();
        button.GetComponent<Button>().onClick.AddListener(delegate { toDoOnClick(choice); } );

        //deixa ultimo botão ativado, selecionado, para navegação com Axies
        button.GetComponent<Selectable>().Select();

    }

    private void toDoOnClick(JsonData choice)
    {
        currentLayer = choice[0];
        JsonIndex = 1;
        PrintLine();
        desativarBotoes();
    }

    #endregion

    private void Start()
    {
        TextBoxUI_Icone.GetComponent<Gif_UI>().ActionOrText = false;
        TextBoxUI.SetActive(false);

        desativarBotoes();
    }
}
