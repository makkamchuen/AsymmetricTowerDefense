using System;
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
    [SerializeField] private DefaultKeyStroke defaultKeyStroke;
    [SerializeField] private PlayerPrefKey playerPrefKey;
    private string currentKeyStroke;
    private static bool activated = false;
    
    void Start()
    {
        thisbutton = this.GetComponent<Button>();
        thisbutton.onClick.AddListener(TaskOnClick);
        currentKeyStroke = KeyStrokeUtil.GetKeyStrokeChar(playerPrefKey, defaultKeyStroke).ToString();
        this.transform.Find("Button").GetComponentInChildren<TMP_Text>().text = currentKeyStroke;
            
        inputPanel = this.transform.Find("input").gameObject;
        inputField = inputPanel.GetComponentInChildren<InputField>(); 
        inputField.characterLimit = 1;
        inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    void TaskOnClick()
    {
        //Debug.Log(spwanKey + " / " + this.name);
        if (!edit)
        {
            if (!activated)
            {
                inputField.text = currentKeyStroke; 
                edit = true;
                this.GetComponent<Image>().color = editColor;
                activated = true;
                inputPanel.SetActive(true);
                inputField.ActivateInputField();
            }
        }
        else
        {
            Deactivate();
        }
    }

    public void ValueChangeCheck()
    {
        //Debug.Log("Value Changed -- " + inputField.text);
        if(inputField.text != "")
        {
            var input = inputField.text.ToLower();
            this.transform.Find("Button").GetComponentInChildren<TMP_Text>().text = input;
            PlayerPrefs.SetInt(playerPrefKey.ToString(), Convert.ToInt32(input[0]));
            Deactivate();
        }

    }

    private void Deactivate()
    {
        edit = false;
        this.GetComponent<Image>().color = defaultColor;
        inputPanel.SetActive(false);
        activated = false;
    }



}
