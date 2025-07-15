using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ada untuk pindah scene
using System.Collections;         // Wajib ada untuk Coroutine

public class SceneChanger : MonoBehaviour
{

    public AudioManager audioManager;


    public void ChangeSceneWithSound(string sceneName)
    {
        // Memulai Coroutine untuk proses yang ada jedanya
        StartCoroutine(ChangeSceneCoroutine(sceneName));
    }

    private IEnumerator ChangeSceneCoroutine(string sceneName)
    {
        // 1. Panggil fungsi untuk memainkan suara dari AudioManager
        if (audioManager != null)
        {
            audioManager.PlayButtonClickSound();
        }
        
        // 2. Beri jeda sejenak agar suara sempat terdengar
        yield return new WaitForSeconds(0.3f); 

        // 3. Setelah jeda, pindah ke scene yang dituju
        SceneManager.LoadScene(sceneName);
    }
}