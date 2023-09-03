using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mushList : MonoBehaviour
{
    public Text red;
    public Text blue;
    public AudioClip recoverCli;
    public AudioClip buttonCli;
    public GameObject player;
    public GameObject textUI;
    // Start is called before the first frame update
    void Start()
    {
        red.text = PlayerHealth.redCount.ToString();
        blue.text = PlayerHealth.blueCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;//鼠标解锁并显示
        }
        red.text = PlayerHealth.redCount.ToString();
        blue.text = PlayerHealth.blueCount.ToString();
    }
    public void redClcik()
    {
        AudioSource.PlayClipAtPoint(buttonCli, player.transform.position);
        if (PlayerHealth.redCount > 0)
        {
            PlayerHealth.playerHealth += 20;
            if(PlayerHealth.playerHealth>=100)
            {
                PlayerHealth.playerHealth = 100;
            }
            PlayerHealth.redCount -= 1;
            AudioSource.PlayClipAtPoint(recoverCli, player.transform.position);
        }
        else
        {
            StartCoroutine(textCour());
        }
    }
    public void blueClcik()
    {
        AudioSource.PlayClipAtPoint(buttonCli, player.transform.position);
        if (PlayerHealth.blueCount > 0)
        {
            PlayerHealth.runTime += 4;
            PlayerHealth.blueCount -= 1;
            AudioSource.PlayClipAtPoint(recoverCli, player.transform.position);
        }
        else
        {
            StartCoroutine(textCour());
        }
    }
    IEnumerator textCour()
    {
        textUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        textUI.SetActive(false);
    }
}
