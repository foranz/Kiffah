using UnityEngine;

public class StoryManager : MonoBehaviour
{
    // === Bagian yang sudah ada ===
    public GameObject[] storyPages;
    public AudioClip pageTurnSound;
    private int currentPageIndex = 0;

    public GameObject nextButton;
    public GameObject backButton;


    void Start()
    {
        // Pastikan hanya halaman pertama yang aktif
        for (int i = 0; i < storyPages.Length; i++)
        {
            storyPages[i].SetActive(i == currentPageIndex);
        }

        // Panggil fungsi baru untuk mengatur visibilitas tombol saat game dimulai
        UpdateButtonVisibility();
    }

    public void GoToNextPage()
    {
        PlayClickSound();

        if (currentPageIndex < storyPages.Length - 1)
        {
            storyPages[currentPageIndex].SetActive(false);
            currentPageIndex++;
            storyPages[currentPageIndex].SetActive(true);

            // Panggil fungsi baru untuk update tombol setelah pindah halaman
            UpdateButtonVisibility();
        }
    }

    public void GoToPreviousPage()
    {
        PlayClickSound();

        if (currentPageIndex > 0)
        {
            storyPages[currentPageIndex].SetActive(false);
            currentPageIndex--;
            storyPages[currentPageIndex].SetActive(true);

            // Panggil fungsi baru untuk update tombol setelah pindah halaman
            UpdateButtonVisibility();
        }
    }

    private void PlayClickSound()
    {
        if (SFXManager.instance != null && pageTurnSound != null)
        {
            SFXManager.instance.PlaySound(pageTurnSound);
        }
    }

    // === Fungsi BARU untuk mengatur visibilitas tombol ===
    void UpdateButtonVisibility()
    {
        // Cek apakah referensi tombol sudah diatur di Inspector
        if (backButton != null)
        {
            // Tampilkan tombol Back HANYA JIKA currentPageIndex lebih besar dari 0
            backButton.SetActive(currentPageIndex > 0);
        }

        if (nextButton != null)
        {
            // Tampilkan tombol Next HANYA JIKA currentPageIndex lebih kecil dari halaman terakhir
            nextButton.SetActive(currentPageIndex < storyPages.Length - 1);
        }
    }
}