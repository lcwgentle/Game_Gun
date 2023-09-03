using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserListPanel : MonoBehaviour
{
    public TMP_InputField InputUserName;
    public TMP_InputField InputPassword;
    public GameObject itemPrefab;
    public Transform contentPanel;
    public GameObject SelectPanel;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        SqliteConnection Connection = new SqliteConnection("Data source=UserInfo.db");
        try
        {
            Connection.Open();
            string Sql = @"SELECT 
                            UserName,
                            Tel,
                            ZhangHao,
                            PassWord,
                            Gender,
                            Age
                            FROM UserInfoTable";
            SqliteCommand Command = new SqliteCommand(Connection);
            Command.CommandText = Sql;
            var Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                
                while(Reader.Read())
                {
                    //print("SuCe");
                    GameObject Item = Instantiate(itemPrefab);//创建Item
                    var texts = Item.GetComponentsInChildren<TMP_Text>();
                    foreach(var T in texts)
                    {
                        switch(T.name)
                        {
                            case "UserName":
                                T.text = Reader.GetString(0);
                                break;
                            case "Tel":
                                T.text = Reader.GetString(1);
                                break;
                            case "ZhangHao":
                                T.text = Reader.GetString(2);
                                break;
                            case "Password":
                                T.text = Reader.GetString(3);
                                break;
                            case "Age":
                               T.text = Reader.GetInt32(5).ToString();
                                break;
                            case "Gender":
                                if (Reader.GetInt32(4) == 1)
                                {
                                    T.text = "男";
                                }
                                else
                                {
                                    T.text = "女";
                                }
                                break;
                        }
                    }
                    Item.GetComponent<UserItem>().InputUser = InputUserName;
                    Item.GetComponent<UserItem>().InputPassword = InputPassword;
                    Item.GetComponent<UserItem>().SelectPanel = SelectPanel;
                    Item.GetComponent<Image>().sprite = sprite;
                    Item.transform.SetParent(contentPanel);
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
        
    }
    //public void delete()
    //{
    //    SqliteConnection Connection = new SqliteConnection("Data source=UserInfo.db");
    //    try
    //    {
    //        Connection.Open();
    //        string Sql = "delete from UserInfoTable Where ZhangHao='" + InputUserName.text.ToString() + "'";
    //        SqliteCommand Command = new SqliteCommand(Connection);
    //        Command.CommandText = Sql;
    //        var Reader = Command.ExecuteReader();
    //        if (Reader.HasRows && Reader.Read())
    //        {
    //            int count = Reader.GetInt32(0);
    //            Connection.Close();
    //            if (count > 0)
    //            {
    //                print("删除成功");
    //            }
    //        }
    //    }
    //    catch (SqliteException e)
    //    {
    //        print(e.Message);
    //    }
        //try
        //{
        //    string Sql = @"SELECT 
        //                    UserName,
        //                    Tel,
        //                    ZhangHao,
        //                    PassWord,
        //                    Gender,
        //                    Age
        //                    FROM UserInfoTable";
        //    SqliteCommand Command = new SqliteCommand(Connection);
        //    Command.CommandText = Sql;
        //    var Reader = Command.ExecuteReader();
        //    if (Reader.HasRows)
        //    {

        //        while (Reader.Read())
        //        {
        //            //print("SuCe");
        //            GameObject Item = Instantiate(itemPrefab);//创建Item
        //            var texts = Item.GetComponentsInChildren<TMP_Text>();
        //            foreach (var T in texts)
        //            {
        //                switch (T.name)
        //                {
        //                    case "UserName":
        //                        T.text = Reader.GetString(0);
        //                        break;
        //                    case "Tel":
        //                        T.text = Reader.GetString(1);
        //                        break;
        //                    case "ZhangHao":
        //                        T.text = Reader.GetString(2);
        //                        break;
        //                    case "Password":
        //                        T.text = Reader.GetString(3);
        //                        break;
        //                    case "Age":
        //                        T.text = Reader.GetInt32(5).ToString();
        //                        break;
        //                    case "Gender":
        //                        if (Reader.GetInt32(4) == 1)
        //                        {
        //                            T.text = "男";
        //                        }
        //                        else
        //                        {
        //                            T.text = "女";
        //                        }
        //                        break;
        //                }
        //            }
        //            Item.GetComponent<UserItem>().InputUser = InputUserName;
        //            Item.GetComponent<UserItem>().InputPassword = InputPassword;
        //            Item.GetComponent<UserItem>().SelectPanel = SelectPanel;
        //            Item.GetComponent<Image>().sprite = sprite;
        //            Item.transform.SetParent(contentPanel);
        //        }
        //    }
        //}
        //catch
        //{

        //}
    //    try
    //    {
    //        Connection.Close();
    //    }
    //    catch (Exception)
    //    {

    //    }

    //}
    //选择账号面板消失
    public void ReSetButtonClick()
    {
        SelectPanel.SetActive(false);
    }
}
