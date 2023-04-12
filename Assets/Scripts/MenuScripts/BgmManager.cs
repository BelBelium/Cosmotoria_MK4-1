using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmManager : MonoBehaviour
{
    public AudioClip[] bgmClips;  // 각 씬 또는 장소마다 다른 배경음악 클립

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // 현재 씬의 이름을 가져옴
        string sceneName = SceneManager.GetActiveScene().name;

        // 현재 씬의 이름과 일치하는 배경음악 클립을 찾음
        AudioClip bgmClip = null;

        foreach (AudioClip clip in bgmClips)
        {
            if (clip.name == sceneName)
            {
                bgmClip = clip;
                break;
            }
        }

        // 해당 배경음악 클립을 재생
        if (bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.Play();
        }
    }
}
