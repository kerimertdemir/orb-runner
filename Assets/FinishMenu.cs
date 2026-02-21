using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;   // oyunu yeniden başlat
        SceneManager.LoadScene("MainMenu");  // kendi ana menü sahnenin adını buraya yaz
    }

    public void QuitGame()
    {
        Debug.Log("Oyun kapatılıyor...");
        Application.Quit();   // sadece build alınca çalışır
    }
}
