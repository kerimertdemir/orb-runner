using TMPro;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public GameObject finishMenuUI; // Canvas'taki paneli buraya sürükle
    public TextMeshProUGUI winText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // oyuncu finish'e çarpınca
        {
            Time.timeScale = 0f;               // oyun dursun
            finishMenuUI.SetActive(true);      // panel açılsın

            if (winText != null)
            {
                winText.gameObject.SetActive(true); // Hata veren satırı böyle düzelt
            }
        }
    }
}
