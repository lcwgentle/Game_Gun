using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    GameObject ga;
    private void Awake()
    {
        ga = transform.gameObject;
    }
    public void statClick()
    {
        SceneManager.LoadScene("Start");
        PlayerHealth.playerHealth = 100;
    }
    public void exitClick()
    {
        Application.Quit();
    }
    private void Update()
    {
        if(ga.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;//鼠标解锁并显示
        }
    }
}
