using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startUI : MonoBehaviour
{
    public AudioClip buttonClip;
    public GameObject mer;
    public GameObject gameUI;
    public GameObject storyUI;
    public void starClick()
    {
        gameUI.SetActive(false);
        storyUI.SetActive(true);
    }
    public void exitClick()
    {
        Application.Quit();
    }
    public void loginClick()
    {
        SceneManager.LoadScene("Login");
    }
    public void audioButtonClick()
    {
        AudioSource.PlayClipAtPoint(buttonClip, mer.transform.position);
    }
}
