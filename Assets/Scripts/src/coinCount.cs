using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinCount : MonoBehaviour
{
    public Text coin;
    // Start is called before the first frame update
    void Start()
    {
        coin.text = PlayerHealth.coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        coin.text = PlayerHealth.coin.ToString();
    }
}
