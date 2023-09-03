using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winGame : MonoBehaviour
{
    public GameObject winUI;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            winUI.SetActive(true);
        }
    }
    private void Update()
    {
        if (winUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;//鼠标解锁并显示
        }
    }
}
