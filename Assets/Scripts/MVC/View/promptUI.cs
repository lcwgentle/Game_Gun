using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class promptUI : MonoBehaviour
{
    public AudioClip clipButton;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;//鼠标解锁并显示
        }
    }
    public void backClick()
    {
        AudioSource.PlayClipAtPoint(clipButton, player.transform.position);
        gameObject.SetActive(false);
    }
}
