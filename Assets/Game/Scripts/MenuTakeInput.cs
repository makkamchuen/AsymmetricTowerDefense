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
    private Color32 defaultColor = new Color32(70, 70, 70, 134);
    private Color32 editColor = new Color32(40, 150, 40, 210);
    private Button thisbutton;
    [SerializeField] private DefaultKeyStroke defaultKeyStroke;
    [SerializeField] private PlayerPrefKey playerPrefKey;
    private string currentKeyStroke;
    private static bool activated = false;
    [SerializeField] private TMP_Text diffFromKey1;
    [SerializeField] private TMP_Text diffFromKey2;

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
        if (!activated)
        {
            inputField.text = currentKeyStroke;
            this.GetComponent<Image>().color = editColor;
            activated = true;
            inputPanel.SetActive(true);
            inputField.ActivateInputField();
        }
        else
        {
            Deactivate();
        }
    }

    public void ValueChangeCheck()
    {
        //Debug.Log("Value Changed -- " + inputField.text);
        if(inputField.text != "" & inputField.text != diffFromKey1.text & inputField.text != diffFromKey2.text)
        {
            var input = inputField.text.ToLower();
            this.transform.Find("Button").GetComponentInChildren<TMP_Text>().text = input;
            PlayerPrefs.SetInt(playerPrefKey.ToString(), Convert.ToInt32(input[0]));
            Deactivate();
        }

    }

    private void Deactivate()
    {
        this.GetComponent<Image>().color = defaultColor;
        inputPanel.SetActive(false);
        activated = false;
    }



}
