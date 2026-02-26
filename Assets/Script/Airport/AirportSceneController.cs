using UnityEngine;

public class AirportSceneController : MonoBehaviour
{
    public PortalDestination[] destinations;
    private PortalDestination current;
    private GateMove gateMove;
    private CountryType currentCountry;
    #if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string OnAirportLoadedJS();
    #endif

    /* Toggle verbose controller logs for country/web bridge diagnostics. */
    private const bool VerboseLogging = true;

    void Awake()
    {
        gateMove = GetComponent<GateMove>();

        if (gateMove == null)
        {
            Debug.LogError("GateMove component not found!");
        }
    }

    public void SetCountry(CountryType country)
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

        gameObject.SetActive(current != null);

        if (current != null)
        {
            gateMove.SetTargetScene(CountryToScene(country));
            Debug.Log($"국가 설정 완료: {country} -> {current.sceneName}");

            // Send selected country to the Web layer after country is confirmed.
            JSBridge.NotifyCountrySelected(country.ToString());
        }
        else
        {
            Debug.LogWarning($"국가 미설정: {country}");
        }
    }

    public void SetCountryFromWeb(string countryStr)
    {
        if (VerboseLogging)
        {
            Debug.Log("웹에서 받은 Country = " + countryStr);
        }

        if (!System.Enum.TryParse(countryStr, out CountryType country))
        {
            Debug.LogWarning("파싱 실패, ALL로 처리");
            country = CountryType.ALL;
        }

        SetCountry(country);
    }

    void Start()
    {
        if (VerboseLogging)
        {
            Debug.Log("SetCountry 초기 상태: " + currentCountry);
        }
        SetCountry(currentCountry);
#if UNITY_WEBGL && !UNITY_EDITOR
        OnAirportLoadedJS();
        if (VerboseLogging)
        {
            Debug.Log("Initialized; called OnAirportLoadedJS to notify Web layer.");
            // 공항 신 로드가 완료된 후에 OnAirportLoadedJS를 호출해야 React에서 정상적으로 국가를 주입할 수 있습니다.
            // 만약 신이 로드되기 전에 주입을 시도한다면 주입하는 상태가 의도와 다르게 적용될 가능성이(주로 주입이 안 될 겁니다) 발생할 수 있습니다.
            // 이에 타 객체에서도 React에 이벤트를 전달해야 한다면 initialize 완료 후에 전달하여야 불필요한 버그를 줄일 수 있습니다.
        }
#endif
    }

    private string CountryToScene(CountryType country)
    {
        switch (country)
        {
            case CountryType.US: return "City1";
            case CountryType.JP: return "City2";
            case CountryType.GR: return "City3";
            case CountryType.CN:
            case CountryType.IT:
            case CountryType.ALL:
            default: return "?";
        }
    }
}
