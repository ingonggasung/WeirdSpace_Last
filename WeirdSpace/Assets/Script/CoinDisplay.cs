using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Start()
    {        
    }

    private void Update()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        coinText.text = coin.ToString();
    }
}
