using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public TextMeshProUGUI text;
    int amount;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeAmount(int num)
    {
        amount += num;
        text.text = amount.ToString();
    }

}
