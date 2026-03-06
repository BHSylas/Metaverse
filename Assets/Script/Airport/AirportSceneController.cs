using UnityEngine;

public class AirportSceneController : MonoBehaviour
{
    public PortalDestination[] destinations;

    private PortalDestination current;
    private GateMove gateMove;
    private CountryType currentCountry;

    private bool countryInitialized = false;

#if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void OnAirportLoadedJS();
#endif

    private const bool VerboseLogging = true;

    void Awake()
    {
        gateMove = GetComponent<GateMove>();

        if (gateMove == null)
        {
            Debug.LogError("GateMove component not found!");
        }
    }

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OnAirportLoadedJS();
#endif

        if (VerboseLogging)
        {
            Debug.Log("AirportSceneController initialized. Waiting for country from Web...");
        }
    }

    // ✅ Web에서만 호출되도록
    public void SetCountryFromWeb(string countryStr)
    {
        if (countryInitialized)
        {
            if (VerboseLogging)
                Debug.Log("Country already initialized. Ignoring duplicate call.");
            return;
        }

        if (VerboseLogging)
        {
            Debug.Log("웹에서 받은 Country = " + countryStr);
        }

        if (!System.Enum.TryParse(countryStr, out CountryType country))
        {
            Debug.LogWarning("Country 파싱 실패. 로드 중단.");
            return;
        }

        countryInitialized = true;
        SetCountry(country);
    }

    private void SetCountry(CountryType country)
    {
        currentCountry = country;
        current = null;

        foreach (var d in destinations)
        {
            if (d.country == country)
            {
                current = d;
                break;
            }
        }

        if (current == null)
        {
            Debug.LogWarning($"해당 국가에 매칭되는 PortalDestination 없음: {country}");
            return;
        }

        string sceneName = CountryToScene(country);

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("씬 이름이 유효하지 않음. 로드 중단.");
            return;
        }

        gateMove.SetTargetScene(sceneName);

        if (VerboseLogging)
        {
            Debug.Log($"국가 설정 완료: {country} -> {sceneName}");
        }

        JSBridge.NotifyCountrySelected(country.ToString());
    }

    private string CountryToScene(CountryType country)
    {
        switch (country)
        {
            case CountryType.US: return "City1";
            case CountryType.JP: return "City2";
            case CountryType.CN: return "City3";
            case CountryType.GR: return "City4";
            case CountryType.IT: return "City5";
            default: return null;   //
        }
    }
}