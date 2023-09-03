using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mushroomUI : MonoBehaviour
{
    public bool isCatch = false;
    public void catchMushroomClick()
    {
        isCatch = true;
    }
    public void notCatchMushroomClick()
    {
        isCatch = false;
    }
    private void Update()
    {
        //if(isCatch&&transform.GetComponent<mushroom>().isStay)
        //{
        //    timeText.GetComponentInChildren<Text>().text = ((int)timer + 1).ToString();
        //    timer -= Time.deltaTime;
        //    if(timer<=0)
        //    {
        //        timeText.SetActive(false);
        //        gameObject.SetActive(false);
        //    }
        //}
        //else
        //{
        //    timer = 3;
        //    timeText.SetActive(false);
        //}
    }
    public bool IsCatch()
    {
        return isCatch;
    }
}
