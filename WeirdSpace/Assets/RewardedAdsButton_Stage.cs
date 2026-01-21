using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class RewardedAdsButton_Stage : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    string _adUnitId = null; // This will remain null for unsupported platforms    
    private bool isRewardGranted = false; // 보상이 이미 지급되었는지 확인하는 플래그    

    void Awake()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
    }

    private void Start()
    {
        LoadAd();
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            _showAdButton.onClick.RemoveAllListeners();
            _showAdButton.onClick.AddListener(ShowAd);
        }
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"광고 완료됨: {adUnitId}, 상태: {showCompletionState}, 보상 여부: {isRewardGranted}");

        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            if (!isRewardGranted)
            {
                Debug.Log("보상 지급 조건 통과");
                GrantReward();
                isRewardGranted = true;
            }
        }
    }

    private void GrantReward()
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoin(50);
            SceneManager.LoadScene("Choice"); // 광고 시청 후 "Choice" 씬으로 이동
        }
        else
        {
            Debug.LogWarning("CoinManager 인스턴스가 존재하지 않습니다.");
        }
        LoadAd(); // 광고를 다시 로드하여 다음 광고를 준비합니다.
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        // 광고가 시작되면 보상 지급 플래그를 초기화
        isRewardGranted = false;
    }

    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }
}
