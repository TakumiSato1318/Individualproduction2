using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip BGM;
    public AudioClip goSE;//�Q�[���I�[�o�[

    private AudioSource bgmAudioSource;
    private AudioSource goSEAudioSource;

    public GameObject targetObject; // ���݂��m�F�������I�u�W�F�N�g

    void Start()
    {
        // BGM���Đ������AudioSource���擾
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.clip = BGM;

        goSEAudioSource = gameObject.AddComponent<AudioSource>();
        goSEAudioSource.clip = goSE;
    }

    void Update()
    {
        //�v���C���[�����񂾂�
        if (targetObject == null)
        {
            StopBGM(BGM);
            PlaySE(goSE);
        }
    }

    void StopBGM(AudioClip sound)
    {
        // BGM���~
        bgmAudioSource.Stop();
    }

    void PlaySE(AudioClip sound)
    {
        if (!goSEAudioSource.isPlaying)
        {
            goSEAudioSource.volume = 0.1f;
            goSEAudioSource.Play();
        }
    }
}
