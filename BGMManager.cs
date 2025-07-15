using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Class kecil untuk pasangan Scene-Musik
[System.Serializable]
public class SceneMusic
{
    public string sceneName;
    public AudioClip musicClip;
}

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    private AudioSource bgmSource;

    // == PENGATURAN MUSIK PER SCENE ==
    public List<SceneMusic> sceneMusicList;
    public float fadeDuration = 1.0f;
    private AudioClip currentClip;

    // == PENGATURAN TOMBOL TOGGLE ==
    public Sprite musicOnIcon;
    public Sprite musicOffIcon;
    private Button musicButton;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            bgmSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Atur status mute dari data yang tersimpan saat game pertama kali dimulai
        bgmSource.mute = (PlayerPrefs.GetInt("IsMusicMuted", 0) == 1);
        
        // Mainkan musik untuk scene pertama kali
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    // Fungsi ini berjalan setiap kali scene baru dimuat
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // --- Bagian Logika Tombol ---
        // Cari tombol musik di scene baru menggunakan Tag
        GameObject buttonObj = GameObject.FindWithTag("MusicButton");
        if (buttonObj != null)
        {
            musicButton = buttonObj.GetComponent<Button>();
            if (musicButton != null)
            {
                // Hubungkan fungsi ToggleMusic ke tombol secara otomatis
                musicButton.onClick.RemoveAllListeners();
                musicButton.onClick.AddListener(ToggleMusic);
                UpdateMusicIcon(); // Langsung update ikonnya
            }
        }

        // --- Bagian Logika Ganti Musik ---
        AudioClip clipToPlay = FindMusicForScene(scene.name);
        
        // Jika ada musik yang cocok DAN musik itu berbeda dari yang sedang diputar
        if (clipToPlay != null && clipToPlay != currentClip)
        {
            StartCoroutine(FadeMusicTransition(clipToPlay));
        }
    }

    // Fungsi untuk tombol mute/unmute
    public void ToggleMusic()
    {
        bgmSource.mute = !bgmSource.mute;
        PlayerPrefs.SetInt("IsMusicMuted", bgmSource.mute ? 1 : 0);
        UpdateMusicIcon();
    }

    // Fungsi untuk update ikon tombol
    void UpdateMusicIcon()
    {
        if (musicButton != null)
        {
            if (bgmSource.mute)
            {
                musicButton.GetComponent<Image>().sprite = musicOffIcon;
            }
            else
            {
                musicButton.GetComponent<Image>().sprite = musicOnIcon;
            }
        }
    }

    // Fungsi untuk mencari musik di dalam daftar
    AudioClip FindMusicForScene(string sceneName)
    {
        foreach (var sceneMusic in sceneMusicList)
        {
            if (sceneMusic.sceneName == sceneName)
            {
                return sceneMusic.musicClip;
            }
        }
        return null; // Jika tidak ada musik spesifik, kembalikan null
    }

    // Coroutine untuk transisi musik dengan fade
    private IEnumerator FadeMusicTransition(AudioClip newClip)
    {
        float startVolume = 1.0f; // Asumsikan volume awal adalah 1

        // Jika sudah ada musik yang bermain, redupkan dulu
        if (bgmSource.isPlaying)
        {
            startVolume = bgmSource.volume;
            while (bgmSource.volume > 0)
            {
                bgmSource.volume -= startVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }
        }

        bgmSource.Stop();
        bgmSource.clip = newClip;
        currentClip = newClip;
        bgmSource.Play();

        // Munculkan musik baru secara perlahan
        bgmSource.volume = 0;
        while (bgmSource.volume < startVolume)
        {
            bgmSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        bgmSource.volume = startVolume;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}