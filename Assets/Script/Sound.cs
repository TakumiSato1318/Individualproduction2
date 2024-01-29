using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip BGM;
    public AudioClip goSE;//ゲームオーバー

    private AudioSource bgmAudioSource;
    private AudioSource goSEAudioSource;

    public GameObject targetObject; // 存在を確認したいオブジェクト

    void Start()
    {
        // BGMが再生されるAudioSourceを取得
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.clip = BGM;

        goSEAudioSource = gameObject.AddComponent<AudioSource>();
        goSEAudioSource.clip = goSE;
    }

    void Update()
    {
        //プレイヤーが死んだら
        if (targetObject == null)
        {
            StopBGM(BGM);
            PlaySE(goSE);
        }
    }

    void StopBGM(AudioClip sound)
    {
        // BGMを停止
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
