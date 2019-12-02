using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public Text one;
    public Text two;
    public Text three;
    public Text four;
    public Text five;


    // Start is called before the first frame update
    void Start()
    {
        one.text = PlayerPrefs.GetInt("first").ToString();
        two.text = PlayerPrefs.GetInt("second").ToString();
        three.text = PlayerPrefs.GetInt("third").ToString();
        four.text = PlayerPrefs.GetInt("fourth").ToString();
        five.text = PlayerPrefs.GetInt("fifth").ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}