using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아래의 코드를 추가 해서 사운드 호출 가능
// Stage_Sound_Manager.instance.ChangeBGM("Wave"); 웨이브 시작
// Stage_Sound_Manager.instance.ChangeBGM("Boss"); 보스 등장 시
// Stage_Sound_Manager.instance.ChangeBGM("Waiting"); 대기 시간
// Stage_Sound_Manager.instance.ChangeBGM("Clear"); 클리어시
// Stage_Sound_Manager.instance.ChangeBGM("Fail"); 실패시

public class Stage_Sound_Manager : MonoBehaviour
{
    public static Stage_Sound_Manager instance;

    [Header("Sound Player")]
    public AudioSource SoundPlayer;
    public AudioSource SfxPlayer;

    [Header("Sound Volume Settings")]
    [Range(0f, 1f)] public float waitingVolume = 0.3f;
    [Range(0f, 1f)] public float waveVolume = 0.3f;
    [Range(0f, 1f)] public float bossVolume = 0.3f;
    [Range(0f, 1f)] public float clearVolume = 0.25f;
    [Range(0f, 1f)] public float failVolume = 0.25f;
    [Range(0f, 1f)] public float waveSfxVolume = 0.3f;
    [Range(0f, 1f)] public float bossSfxVolume = 0.1f;

    [Header("Sound Clip")]
    public AudioClip waitingBgm;
    public AudioClip waveBgm;
    public AudioClip bossBgm;
    public AudioClip waveSfx;
    public AudioClip bossWaveSfx;
    public AudioClip clearSfx;
    public AudioClip failSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SettingSound("Waiting");
            Debug.Log("대기 BGM");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SettingSound("Wave");
            Debug.Log("웨이브 BGM");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SettingSound("Boss");
            Debug.Log("보스 BGM");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SettingSound("Clear");
            Debug.Log("클리어 SFX");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SettingSound("Fail");
            Debug.Log("패배 SFX");
        }
    }
    public void SettingSound(string state)
    {
        if (SoundPlayer == null || SfxPlayer == null)
            return;

        SoundPlayer.Stop();

        switch (state)
        {
            case "Waiting":
                SoundPlayer.clip = waitingBgm;
                SoundPlayer.volume = waitingVolume;
                SoundPlayer.loop = true;
                SoundPlayer.Play();
                break;

            case "Wave":
                SoundPlayer.clip = waveBgm;
                SoundPlayer.volume = waveVolume;
                SoundPlayer.loop = true;
                break;

            case "Boss":
                SoundPlayer.clip = bossBgm;
                SoundPlayer.volume = bossVolume;
                SoundPlayer.loop = true;
                break;

            case "Clear":
                SoundPlayer.clip = clearSfx;
                SoundPlayer.volume = clearVolume;
                SoundPlayer.loop = false;
                SoundPlayer.Play();
                break;

            case "Fail":
                SoundPlayer.clip = failSfx;
                SoundPlayer.volume = failVolume;
                SoundPlayer.loop = false;
                SoundPlayer.Play();
                break;
        }
    }
    private IEnumerator SfxToBgm(AudioClip sfx, float sfxVol, AudioClip bgm, float bgmVol)
    {
        if (sfx != null)
        {
            SfxPlayer.PlayOneShot(sfx, sfxVol);
            yield return new WaitForSeconds(1.0f);
        }

        if (bgm != null)
        {
            SoundPlayer.clip = bgm;
            SoundPlayer.volume = bgmVol;
            SoundPlayer.loop = true;
            SoundPlayer.Play();
        }
    }

    /*switch (state)
    {
        case "Wave":
            SfxPlayer.PlayOneShot(waveSfx, waveSfxVolume);
            break;

        case "Boss":
            SfxPlayer.PlayOneShot(bossWaveSfx, bossSfxVolume);
            break;
    }*/
}

