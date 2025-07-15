using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [Header("Tombol Pilihan")]
    public Button choiceButtonA;
    public Button choiceButtonB;

    [Header("Popup Feedback")]
    public GameObject feedbackPopupA;
    public GameObject feedbackPopupB;

    [Header("Tombol Navigasi Utama")]
  
    public GameObject mainNextButton;

    [Header("Audio")]
    public AudioClip choiceSound;

    private bool choiceHasBeenMade = false;

    // Fungsi ini berjalan setiap kali Halaman_Kuis menjadi aktif
    void OnEnable()
    {
        // 1. Reset kondisi kuis setiap kali halaman ini muncul
        choiceHasBeenMade = false;

        // 2. Aktifkan kembali tombol pilihan & sembunyikan pop-up
        choiceButtonA.interactable = true;
        choiceButtonB.interactable = true;
        feedbackPopupA.SetActive(false);
        feedbackPopupB.SetActive(false);

        // 3. Sembunyikan tombol Next utama, paksa pemain untuk memilih
        if (mainNextButton != null)
        {
            mainNextButton.SetActive(false);
        }
    }

   
    public void SelectChoiceA()
    {
        ProcessChoice(choiceButtonA, feedbackPopupA);
    }

    public void SelectChoiceB()
    {
        ProcessChoice(choiceButtonB, feedbackPopupB);
    }

    private void ProcessChoice(Button selectedButton, GameObject feedbackToShow)
    {
  
        if (choiceHasBeenMade) return;

        choiceHasBeenMade = true;

        // Mainkan suara
        if (SFXManager.instance != null && choiceSound != null)
        {
            SFXManager.instance.PlaySound(choiceSound);
        }

        // Tampilkan feedback yang sesuai
        feedbackToShow.SetActive(true);

        // Matikan interaksi kedua tombol pilihan
        choiceButtonA.interactable = false;
        choiceButtonB.interactable = false;
        
        // Tampilkan kembali tombol Next utama agar pemain bisa lanjut
        if (mainNextButton != null)
        {
            mainNextButton.SetActive(true);
        }
    }
}