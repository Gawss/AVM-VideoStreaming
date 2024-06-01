using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonCopyText : MonoBehaviour
{
    private Text targetText;
    private Button targetButton;

    private void OnEnable()
    {
        targetText = GetComponentInChildren<Text>();
        targetButton = GetComponent<Button>();

        targetButton.onClick.AddListener(Copy2Clipboard);
    }

    private void OnDisable()
    {
        targetButton.onClick.RemoveListener(Copy2Clipboard);
    }


    public void Copy2Clipboard()
    {
        GUIUtility.systemCopyBuffer = targetText.text;
    }
}
