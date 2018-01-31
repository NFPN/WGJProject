using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemValue : MonoBehaviour
{
    public int cheeseValue = 0;
    public Text cheeseText;

    void Start()
    {
        cheeseText = GameObject.Find("CheeseValueText").GetComponent<Text>();

        if(SceneManager.GetActiveScene().name == "3_Level1")
        {
            PlayerPrefs.SetInt("CheeseValue", 0);
            PlayerPrefs.Save();
            cheeseText.text = PlayerPrefs.GetInt("CheeseValue").ToString();
        }
        else if (PlayerPrefs.HasKey("CheeseValue"))
        {
            cheeseText.text = PlayerPrefs.GetInt("CheeseValue").ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("CheeseValue"))
        {
            cheeseText.text = PlayerPrefs.GetInt("CheeseValue").ToString();
        }
    }

    public static void AddCheese()
    {
        PlayerPrefs.SetInt("CheeseValue", PlayerPrefs.GetInt("CheeseValue")+1);
        PlayerPrefs.Save();
    }
}
