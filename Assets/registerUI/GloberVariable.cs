using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//自定义用户注册信息类
public class Infor
{
    public string Name;
    public string Tel;
    public string ZhangHao;
    public string Passwords;
    public bool Gender;
    public string Age;
    public Infor(string N, string T, string Z, string P, bool G ,string A)
    {
        Name = N;
        Tel = T;
        ZhangHao = Z;
        Passwords = P;
        Gender = G;
        Age = A;
    }
}

public class GloberVariable
{
    public static List<Infor> Users = new List<Infor>();
}
