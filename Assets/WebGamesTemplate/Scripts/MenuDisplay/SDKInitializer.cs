using System;
using System.Collections;
using Agava.YandexGames;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;

public class SDKInitializer : MonoBehaviour
{
    private const string Tutorial = "Tutorial";

    public event Action Initialized;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        SceneManager.LoadScene("Tutorial");
        yield break;
#endif

        yield return YandexGamesSdk.Initialize(OnYandexSDKInitialize);
        YandexGamesSdk.GameReady();
    }

    private void OnYandexSDKInitialize()
    {
        Initialized?.Invoke();
        Debug.Log("OnYandexSDKInitialize");
        SceneManager.LoadScene("Tutorial");
    }

    private void OnVKSDKInitialize()
    {
        Initialized?.Invoke();
    }
}
