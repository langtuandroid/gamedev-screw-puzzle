using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WS.Script.GameManagers;

public class AskingNameUI : MonoBehaviour {
    public InputField inputField;       //get the input field component
    public GameObject inputFieldObj;        //get the input field object for turning on - off

	// Use this for initialization
	void Start () {
        inputFieldObj.SetActive(ValueStorage.PlayerName == "xxx");       //enable the input field if the the user no input the name before
	}
	
	public void FinishInput()
    {
        //SoundManager.PlaySfx(SoundManager.Instance.soundClick);
        //check if name < 3 keyword then input again
        if (inputField.text.Length < 3)
        {
            return;
        }

        string newString = inputField.text.Replace(" ", "_");
        Debug.LogError("Your name: " + newString);

        ValueStorage.PlayerName = newString;       //save the user name
        inputFieldObj.SetActive(false);     //disable the input field
    }
}
