using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl:MonoBehaviour
{

    public void SelectDir(InputField input) {
        string s = AlasCheck.selectDir();
        if (s != "")
            input.text = s;
    }
}