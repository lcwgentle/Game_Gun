using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System;

public class UserItem : MonoBehaviour
{
    public TMP_InputField InputUser;
    public TMP_InputField InputPassword;
    public GameObject SelectPanel;
    public void OnClick()
    {
        var texts = GetComponentsInChildren<TMP_Text>();
        foreach (var T in texts)
        {
            if(T.name=="ZhangHao")
            {
                InputUser.text = T.text;
                //break;
            }
            if (T.name == "Password")
            {
                InputPassword.text = T.text;
               // print(T.text);
                break;
            }
            
        }
        SelectPanel.SetActive(false);//关闭选择面板
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
