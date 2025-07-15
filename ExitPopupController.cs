using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Wajib ada untuk menggunakan Coroutine

public class ExitPopupController : MonoBehaviour
{
    public GameObject exitPopupPanel;
    
    // VARIABEL BARU: Untuk menampung file suara klik
    public AudioClip clickSound;

    // Fungsi untuk menampilkan pop-up
    public void ShowExitPopup()
    {
        // Mainkan suara dulu
        PlayClickSound();

        if (exitPopupPanel != null)
        {
            exitPopupPanel.SetActive(true);
        }
    }

    // Fungsi untuk menyembunyikan pop-up
    public void HideExitPopup()
    {
        // Mainkan suara dulu
        PlayClickSound();
        
        if (exitPopupPanel != null)
        {
            exitPopupPanel.SetActive(false);
        }
    }

    // Fungsi untuk keluar aplikasi (sekarang memulai Coroutine)
    // Tombol "Iya" akan memanggil fungsi ini
    public void QuitApplicationWithSound()
    {
        StartCoroutine(QuitSequence());
    }

    // FUNGSI BARU: Coroutine untuk proses keluar dengan jeda
    private IEnumerator QuitSequence()
    {
        // 1. Mainkan suara klik
        PlayClickSound();

        // 2. Tunggu sejenak agar suara sempat terdengar
        float delay = clickSound != null ? clickSound.length : 0.1f;
        yield return new WaitForSecondsRealtime(delay); // Gunakan Realtime agar tidak terpengaruh Time.timeScale

        // 3. Lanjutkan proses keluar
        Debug.Log("Keluar dari aplikasi...");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    // FUNGSI BARU: Helper untuk memainkan suara
    private void PlayClickSound()
    {
        // Memanggil SFXManager yang sudah kita buat
        if (SFXManager.instance != null && clickSound != null)
        {
            SFXManager.instance.PlaySound(clickSound);
        }
    }
}