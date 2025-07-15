using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        // Ini adalah pola Singleton, memastikan hanya ada satu SFXManager
        if (instance == null)
        {
            instance = this;
            // Jangan hancurkan objek ini saat pindah scene
            DontDestroyOnLoad(gameObject); 
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            // Jika sudah ada, hancurkan duplikatnya
            Destroy(gameObject);
        }
    }

    // Fungsi untuk memainkan suara
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}