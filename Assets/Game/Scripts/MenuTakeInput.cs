using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class MenuTakeInput : MonoBehaviour
{
    private GameObject inputPanel; //GameObject that contains an InputField 
    private InputField inputField;
    private bool edit = false;
    private Color32 defaultColor = new Color32(70, 70, 70, 134);
    private Color32 editColor = new Color32(40, 150, 40, 210);
    private Button thisbutton;
    private string spwanKey;
    
    void Start()
    {
        thisbutton = this.GetComponent<Button>();
        thisbutton.onClick.AddListener(TaskOnClick);

        spwanKey = this.transform.Find("Button").GetComponentInChildren<TMP_Text>().text;

        inputPanel = this.transform.Find("input").gameObject;

        inputField = inputPanel.GetComponentInChildren<InputField>(); 
        inputField.text = spwanKey; 
        inputField.characterLimit = 1;
        inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    void TaskOnClick()
    {
        //Debug.Log(spwanKey + " / " + this.name);
        if (!edit)
        {
            edit = true;
            this.GetComponent<Image>().color = editColor;
            inputPanel.SetActive(true);
        }
        else
        {
            edit = false;
            this.GetComponent<Image>().color = defaultColor;
            inputPanel.SetActive(false);
        }
    }

    public void ValueChangeCheck()
    {
        //Debug.Log("Value Changed -- " + inputField.text);
        if(inputField.text != "")
        {
            spwanKey = inputField.text.ToUpper();
            this.transform.Find("Button").GetComponentInChildren<TMP_Text>().text = spwanKey; 
        }
    }

}
