using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnStarGame : MonoBehaviour
{
    private Button BTN;

    // Start is called before the first frame update
    void Start()
    {
        BTN = GetComponent<Button>();
        BTN.onClick.AddListener(NextScene);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
