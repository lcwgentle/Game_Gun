using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class storyUI : MonoBehaviour
{

    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject button;

    private void Start()
    {
        StartCoroutine(text1Cour());
    }

    IEnumerator text1Cour()
    {
        text1.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(text2Cour());
    }
    IEnumerator text2Cour()
    {
        text2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(text3Cour());
    }
    IEnumerator text3Cour()
    {
        text3.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(text4Cour());
    }
    IEnumerator text4Cour()
    {
        text4.SetActive(true);
        yield return new WaitForSeconds(2f);
        button.SetActive(true);
    }
    public void buttonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
