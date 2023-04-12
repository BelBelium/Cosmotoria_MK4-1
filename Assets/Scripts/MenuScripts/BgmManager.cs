using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmManager : MonoBehaviour
{
    public AudioClip[] bgmClips;  // �� �� �Ǵ� ��Ҹ��� �ٸ� ������� Ŭ��

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // ���� ���� �̸��� ������
        string sceneName = SceneManager.GetActiveScene().name;

        // ���� ���� �̸��� ��ġ�ϴ� ������� Ŭ���� ã��
        AudioClip bgmClip = null;

        foreach (AudioClip clip in bgmClips)
        {
            if (clip.name == sceneName)
            {
                bgmClip = clip;
                break;
            }
        }

        // �ش� ������� Ŭ���� ���
        if (bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.Play();
        }
    }
}
