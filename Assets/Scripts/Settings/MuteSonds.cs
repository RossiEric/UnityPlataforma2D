using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MuteSonds : MonoBehaviour
{
    public bool toggle = true;
    private Button BTN;

    // Start is called before the first frame update
    void Start()
    {
        BTN = GetComponent<Button>();
        BTN.onClick.AddListener(ToggleSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSound()
    {
        toggle = !toggle;

        if (toggle) {
            SetTransparency(BTN.image, 1f);
            AudioListener.volume = 1f;
        }
        else {
            SetTransparency(BTN.image, 0.3f);
            AudioListener.volume = 0f;
        }          
    }

    /// <summary>
    /// Set transparece in images UI
    /// </summary>
    /// <param name="p_image"></param>
    /// <param name="p_transparency"></param>
    public static void SetTransparency(Image p_image, float p_transparency)
    {
        if (p_image != null)
        {
            Color __alpha = p_image.color;
            __alpha.a = p_transparency;
            p_image.color = __alpha;
        }
    }
}
