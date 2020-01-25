using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quitgame : MonoBehaviour
{
    private Button BTN;

    // Start is called before the first frame update
    void Start()
    {
        BTN = GetComponent<Button>();
        BTN.onClick.AddListener(QuitGameAct);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitGameAct()
    {
        Application.Quit();
    }
}
