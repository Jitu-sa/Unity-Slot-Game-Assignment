using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown Dropdown;
    [SerializeField] TMP_Text BalanceText;

    int Balance=500;
    int reward;

    void Update()
    {
        BalanceText.text = "Balance:" + Balance;
    }
    public void Result(bool Win)
    {
        if (Win)
        {
            reward = ((Dropdown.value + 1) * 10) * 5;
            Balance += reward;
        }
        if (!Win) 
        {
            Balance -= (Dropdown.value + 1) * 10;
        }

        Debug.Log(Balance);
    }
}
