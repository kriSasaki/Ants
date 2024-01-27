using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SDKInitializer : MonoBehaviour
{
    private const string CurrentLevelKey = "CurrentLevel";
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
        if (PlayerPrefs.HasKey(CurrentLevelKey))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(CurrentLevelKey)+1);
        }
        else
        {
            SceneManager.LoadScene(Tutorial);
        }
    }

    private enum SceneName
    {
        Tutorial,
        Level1,
        Level2
    }
}
