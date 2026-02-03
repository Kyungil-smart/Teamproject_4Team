using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

// 아래의 코드를 추가 해서 사운드 호출 가능
// Stage_Sound_Manager.instance.SettingSound("Wave"); 웨이브 시작
// Stage_Sound_Manager.instance.SettingSound("Boss"); 보스 등장 시
// Stage_Sound_Manager.instance.SettingSound("Waiting"); 대기 시간
// Stage_Sound_Manager.instance.SettingSound("Clear"); 클리어시
// Stage_Sound_Manager.instance.SettingSound("Fail"); 실패시

public class Stage_Sound_Manager : MonoBehaviour
{
    // 싱글톤 패턴
    public static Stage_Sound_Manager instance;

    // 인스펙터 생성
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
    // 게임 진행 상태에 따라 사운드 출력
    public void SettingSound(string state)
    {
        if (SoundPlayer == null || SfxPlayer == null)
            return;

        StopAllCoroutines();
        SoundPlayer.Stop();
        SfxPlayer.Stop();

        switch (state)
        {
            case "Waiting":
                // Debug.Log("준비시간");
                Sound(waitingBgm , waitingVolume, true);
                break;

            case "Wave":
                // Debug.Log("디펜스");
                StartCoroutine(SfxToBgm(waveSfx, waveSfxVolume, waveBgm, waveVolume));
                break;

            case "Boss":
                StartCoroutine(SfxToBgm(bossWaveSfx, bossSfxVolume, bossBgm, bossVolume));
                break;

            case "Clear":
                Sound(clearSfx, clearVolume, false);
                break;

            case "Fail":
                Sound(failSfx, failVolume, false);
                break;
        }
    }
    // 스테이지 사운드 조절 메서드
    private void Sound(AudioClip stageState, float stageVolume, bool isLoop)
    {
        SoundPlayer.clip = stageState;
        SoundPlayer.volume = stageVolume;
        SoundPlayer.loop = isLoop;
        SoundPlayer.Play();
    }

    // Sfx, BGM 딜레이 재생하는 메서드 (코루틴 사용)
    private IEnumerator SfxToBgm(AudioClip sfx, float sfxVol, AudioClip bgm, float bgmVol)
    {
        if (sfx != null)
        {
            SfxPlayer.PlayOneShot(sfx, sfxVol);
            yield return new WaitForSeconds(1.8f);
        }

        if (bgm != null)
        {
            SoundPlayer.clip = bgm;
            SoundPlayer.volume = bgmVol;
            SoundPlayer.loop = true;
            SoundPlayer.Play();
        }
    }
}

