using UnityEngine;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;

public class RemoteCoinfig : MonoBehaviour
{
    struct userAttributes { } // Remote Config 요구사항
    struct appAttributes { }

    void Start()
    {
        InitRemoteConfig();
    }

    async void InitRemoteConfig()
    {
        await UnityServices.InitializeAsync();

        RemoteConfigService.Instance.FetchCompleted += OnRemoteConfigFetched;
        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    void OnRemoteConfigFetched(ConfigResponse response)
    {
        if (response.status == ConfigRequestStatus.Success)
        {
            string latestVersion = RemoteConfigService.Instance.appConfig.GetString("WeirdSpaceVersion");

            Debug.Log($"[RemoteConfig] 최신 버전: {latestVersion}");
            Debug.Log($"[RemoteConfig] 현재 버전: {Application.version}");

            if (IsVersionNewer(latestVersion, Application.version))
            {
                // 업데이트 필요 안내
                ShowUpdatePopup();
            }
        }
        else
        {
            Debug.LogWarning("Remote Config fetch 실패");
        }
    }

    bool IsVersionNewer(string server, string current)
    {
        System.Version vServer = new System.Version(server);
        System.Version vCurrent = new System.Version(current);
        return vServer > vCurrent;
    }

    void ShowUpdatePopup()
    {
        // 여기에 UI로 팝업 띄우거나 앱스토어 이동 코드 삽입
        Debug.LogWarning("업데이트가 필요합니다!");
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.DefaultCompany.WeirdSpace");
    }
}
