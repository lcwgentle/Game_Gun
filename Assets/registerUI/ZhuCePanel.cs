using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using UnityEditor;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System;

public class ZhuCePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Dropdown AgeDrop;
    private string Name;
    private string Tel;
    private string ZhangHao;
    private string Passwords;
    private bool Gender;
    private string GenderInt;
    private string Age;
    public AudioClip buttonClip;
    public GameObject mer;
    public GameObject IsExistUserNameUI;
    public GameObject IsZhangHaoUI;
    public GameObject zhuCeUI;


    public void SubmitButtonClick()
    {
        if (Name!= null && Tel != null && ZhangHao != null && Passwords != null)
        {
            if (Gender)
            {
                GenderInt = "1";
            }
            else
            {
                GenderInt = "0";
            }
            ChunChuInfor();
            //Infor infor = new Infor(Name, Tel, ZhangHao, Passwords, Gender, Age);
            //GloberVariable.Users.Add(infor);//添加新节点入数组
            string sql = @"INSERT INTO UserInfoTable (
                              UserName,
                              Tel,
                              ZhangHao,
                              PassWord,
                              Age,
                              Gender
                          )
                          VALUES (
                              '"+Name+@"',
                              '"+Tel+@"',
                              '"+ZhangHao+@"',
                              '"+Passwords+@"',
                              '"+Age+@"',
                              '"+GenderInt+@"'
                          );";
            try
            {
                SqliteConnection connection = new SqliteConnection("Data source=UserInfo.db");//建立数据库
                connection.Open();
                SqliteCommand command = new SqliteCommand(connection);
                command.CommandText = sql;
                if(command.ExecuteNonQuery()>0)
                {
                    print("Success");
                }
                else
                {
                    print("faile");
                }
                connection.Close();
            }
            catch(SqliteException e)
            {
                print(e.Message);
            }


            StartCoroutine(zhuCeCour());

            
        }
    }

    //存储注册信息
    private void ChunChuInfor()
    {
        PlayerPrefs.SetString("UserNames", Name);
        PlayerPrefs.SetString("UserTel", Tel);
        PlayerPrefs.SetString("UserName", ZhangHao);
        PlayerPrefs.SetString("Passwords", Passwords);
        PlayerPrefs.SetString("UserAge", Age);
        if (Gender)
        {
            PlayerPrefs.SetInt("UserGendetr", 1);//存储性别，男为1，女为0
        }
        else
        {
            PlayerPrefs.SetInt("UserGendetr", 0);
        }
    }

    public void ReturnButtonClick()
    {
        SceneManager.LoadScene("Login");
    }
    public void SetName(string N)
    {
        if (PlayerPrefs.GetString("UserNames") == N)
        {
            //print("姓名已存在！");
            Name = null;
            return;
        }
        //foreach (var User in GloberVariable.Users)
        //{
        //    if (User.Name==N)
        //    {
        //        print("姓名已存在！");
        //        Name = null;
        //        return;
        //    }
        //}
        if (CheckInfoNoChongFu(N, "UserName"))
        {
            StartCoroutine(IsExistUserNameCour());
            print("姓名已存在！");
            Name = null;
            return;
        }
        Name = N;
    }
    public void SetTel(string T)
    {
        if (PlayerPrefs.GetString("UserTel") == T)
        {
            print("电话已存在！");
            Tel = null;
            return;
        }
        //foreach (var User in GloberVariable.Users)
        //{
        //    if (User.Tel == T )
        //    {
        //        print("电话已存在！");
        //        Tel = null;
        //        return;
        //    }
        //}
        if (CheckInfoNoChongFu(T, "Tel"))
        {
            print("电话已存在！");
            Tel = null;
            return;
        }
        Tel = T; 
    }
    public void SetZhangHao(string Z)
    {
        if (PlayerPrefs.GetString("UserName") == Z)
        {
            
            //print("账号已存在！");
            ZhangHao = null;
            return;
        }
        //foreach (var User in GloberVariable.Users)
        //{
        //    if (User.ZhangHao == Z)
        //    {
        //        print("账号已存在！");
        //        ZhangHao = null;
        //        return;
        //    }
        //}
        if (CheckInfoNoChongFu(Z, "ZhangHao"))
        {
            StartCoroutine(IsExistZhangHaoCour());
            print("账号已存在！");
            ZhangHao = null;
            return;
        }
        ZhangHao = Z;
        
    }
    public void SetPasswords(string P)
    {
        Passwords = P;
       
    }
    public void SetGenger(bool G)//男为True,女为false
    {
        Gender = G;
    }
    
    public void SetAge(int A)
    {
        var list = AgeDrop.options;
        Age = list[A].text;
    }
    IEnumerator IsExistUserNameCour()
    {
        IsExistUserNameUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        IsExistUserNameUI.SetActive(false);
    }
    IEnumerator IsExistZhangHaoCour()
    {
       // print("nihao");
        IsZhangHaoUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        IsZhangHaoUI.SetActive(false);
    }

    IEnumerator zhuCeCour()
    {
        zhuCeUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //跳转登录界面
        SceneManager.LoadScene("Login");
        zhuCeUI.SetActive(false);
    }

    //检查新建账号信息不重复
    public bool CheckInfoNoChongFu(string Info,string Which)
    {
        SqliteConnection Connection = new SqliteConnection("Data source=UserInfo.db");
        try
        {
            Connection.Open();
            string Sql = "Select Count("+Which+") from UserInfoTable Where "+Which+"='" + Info+"'";
            //string Sql= "Select Count(" + Which + ") from UserInfoTable Where " + Which + " = '" + Info + "'";
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
        catch (SqliteException e)
        {
            print(e.Message);
        }
        try
        {
            Connection.Close();
        }
        catch (Exception)
        {

        }
        return false;
    }
    public void audioButtonClick()
    {
        AudioSource.PlayClipAtPoint(buttonClip, mer.transform.position);
    }
}

//CREATE TABLE UserInfoTable (
//    UserName VARCHAR (20) NOT NULL,
//    Tel      VARCHAR (20) NOT NULL
//                          UNIQUE,
//    ZhangHao VARCHAR (20) PRIMARY KEY
//                          UNIQUE
//                          NOT NULL,
//    PassWord VARCHAR (20) NOT NULL,
//    Gender   INT (3)      NOT NULL,
//    Age      INT (3)      NOT NULL
//);
