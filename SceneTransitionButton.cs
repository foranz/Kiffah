using UnityEngine;
using UnityEngine.SceneManagement;

// AudioSource tidak lagi dibutuhkan di tombol ini
public class SceneTransitionButton : MonoBehaviour
{
    public AudioClip clickSound;
    public string sceneNameToLoad;

    // Fungsi yang dipanggil dari OnClick()
    public void OnButtonClick()
    {
        // 1. Suruh SFXManager untuk memainkan suara
        // Dia akan hidup terus dan menyelesaikan suaranya
        if (SFXManager.instance != null && clickSound != null)
        {
            SFXManager.instance.PlaySound(clickSound);
        }

        // 2. Langsung pindah scene
        if (!string.IsNullOrEmpty(sceneNameToLoad))
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
        else
        {
            Debug.LogError("Nama Scene belum diatur pada tombol!");
        }
    }
}