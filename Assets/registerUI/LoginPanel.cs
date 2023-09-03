using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System;

public class LoginPanel : MonoBehaviour
{
    public GameObject SelectPanel;
    public TMP_InputField UserNameText;//文本框
    public TMP_InputField Passwards;//密码框
    public Toggle SaveUserName;//记住账号单选框
    public Toggle SavePasswards;//记住密码单选框
    public GameObject ErrorPanel;//报错面板
    public AudioClip buttonClip;
    public GameObject mer;

    public void Onlogin()
    {
        AudioSource.PlayClipAtPoint(buttonClip, mer.transform.position);
        if (VerifySqlInfo(UserNameText.text, Passwards.text))
        { 
            if(SaveUserName.isOn)
            {
                PlayerPrefs.SetInt("IsSaveUserName", 1);
                ISaveUserName();                   
                if(SavePasswards.isOn)
                {
                   PlayerPrefs.SetInt("IsPasswords", 1);
                   ISavePassward();
                }
            }
            SceneManager.LoadScene("Start");//添加场景名称，进入游戏场景（跳转场景）
        }
        else
        {
            //UserNameText.text = "";
            Passwards.text = "";
            ErrorPanel.SetActive(true);//显示提示页面
            //print(PlayerPrefs.GetString("UserName", ""));
            //print(PlayerPrefs.GetString("Passwords", ""));
        }
    }

    public void OnReset()
    {
        AudioSource.PlayClipAtPoint(buttonClip, mer.transform.position);
        UserNameText.text = "";
        Passwards.text = "";
        print("Reset Success");
    }

    public void ISaveUserName()
    {
        PlayerPrefs.SetString("UserName", UserNameText.text);
        //print(PlayerPrefs.GetString("SaveUserName", "Null"));
    }
    public void ISavePassward()
    { 
        PlayerPrefs.SetString("Passwords", Passwards.text);
    }

    public void LoadUserName()
    {
        UserNameText.text= PlayerPrefs.GetString("UserName", "");
    }

    //初始化显示保存的用户名
    public void LoadPassword()
    {
        Passwards.text = PlayerPrefs.GetString("Passwords", "");
    }

    //程序启动初执行一次
    private void Start()
    {
        StartCoroutine(printCour());
        int iSaveUserName = PlayerPrefs.GetInt("IsSaveUserName", 0);
        if(iSaveUserName==1)
        {
            SaveUserName.isOn = true;
            LoadUserName();
            int iSavePassword = PlayerPrefs.GetInt("IsPasswords", 0);
            if(iSavePassword==1)
            {
                SavePasswards.isOn = true;
                LoadPassword();
            }
            else
            {
                SavePasswards.isOn = false;
                Passwards.text = "";
            }
        }
        else
        {
            SaveUserName.isOn = false;
            UserNameText.text = "";
            Passwards.text = "";
        }
    }

    //选择保存名字后才能选择保护密码
    public void OnSaveUserNameChange(bool IsOn)
    {
        
        SavePasswards.isOn = IsOn;
        if (!IsOn)
        {
            SavePasswards.isOn = false;
            SavePasswards.enabled = false;
        }
        else
        {
            SavePasswards.enabled = true;
        }
    }

    //注册页面
    public void TurnToZhuCe()
    {
        AudioSource.PlayClipAtPoint(buttonClip, mer.transform.position);
        SceneManager.LoadScene("ZhuCe");
    }

    //遍历注册信息数组
    //bool VerifyInfo(string ZH,string P)
    //{
    //    //判断磁盘中是否存储匹配
    //    if(PlayerPrefs.GetString("UserName")== ZH && PlayerPrefs.GetString("Passwords")== P)
    //    {
    //        return true;
    //    }
    //    //判断链表中是否存储匹配
    //    foreach (var User in GloberVariable.Users)
    //    {
    //        if (User.ZhangHao == ZH && User.Passwords == P)
    //        {
    //            return true;
    //        }
    //        //print();
    //    }
    //    return false;
    //}

    private bool VerifySqlInfo(string ZH, string P)
    {
        
        SqliteConnection Connection = new SqliteConnection("Data source=UserInfo.db");
        try
        { 
            Connection.Open();
            string Sql = "Select Count(ZhangHao) from UserInfoTable Where ZhangHao='" + ZH + "' and PassWord='" + P + "'";
            SqliteCommand Command = new SqliteCommand(Connection);
            Command.CommandText = Sql;
            var Reader = Command.ExecuteReader();
            if (Reader.HasRows && Reader.Read())
            {
                int count = Reader.GetInt32(0);
                Connection.Close();
                if (count > 0)
                {
                    return true;
                }
            }
        }
        catch(SqliteException e)
        {
            print(e.Message);
        }
        try
        {
            Connection.Close();
        }
        catch(Exception)
        {

        }
        return false;
        //判断数据表是否建立，如果未建立。返回false
        //如果建立，获取数据表的账号，及密码
        //比对账号及密码是否正确；

    }

    IEnumerator printCour()
    {
        yield return new WaitForSeconds(0.2f);
        print("success!");
    }
    //修改Togger .Ison
    public void ChangeZhangHaoTogger(bool T)
    {
        if(T)
        {
            PlayerPrefs.SetInt("IsSaveUserName", 1);
        }
        else
        {
            PlayerPrefs.SetInt("IsSaveUserName", 0);
        }
    }
    public void ChangePasswordsTogger(bool T)
    {
        if (T)
        {
            PlayerPrefs.SetInt("IsPasswords", 1);
        }
        else
        {
            PlayerPrefs.SetInt("IsPasswords", 0);
        }
    }
   
    //打开账号选择面板
    public void SelectButtonCick()
    {
        AudioSource.PlayClipAtPoint(buttonClip, mer.transform.position);
        SelectPanel.SetActive(true);
    }
    public void errorButtonClick()
    {
        AudioSource.PlayClipAtPoint(buttonClip, mer.transform.position);
    }
}

