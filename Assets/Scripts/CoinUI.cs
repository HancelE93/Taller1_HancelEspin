
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [Header("Texto del contador")]
    public TMP_Text coinText;

    private int coins = 0;

    void Start()
    {
        UpdateCoinText();
    }

    public void AddCoin()
    {
        coins++;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Monedas: " + coins;
        }
    }
}
