using System;
using System.Collections;
using Agava.YandexGames;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;

public class SDKInitializer : MonoBehaviour
{
    private const string CurrentLevel = "CurrentLevel";
    private const string Tutorial = "Tutorial";

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnYandexSDKInitialize();
        yield break;
#endif

        yield return YandexGamesSdk.Initialize(OnYandexSDKInitialize);
        YandexGamesSdk.GameReady();
    }

    private void OnYandexSDKInitialize()
    {
        if (PlayerPrefs.HasKey(CurrentLevel))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(CurrentLevel));
        }
        else
        {
            SceneManager.LoadScene(Tutorial);
        }
    }
}
