using UnityEngine;

public class AirportSceneController : MonoBehaviour
{
    public PortalDestination[] destinations;
    private PortalDestination current;
    private GateMove gateMove;
    private CountryType currentCountry;

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
            gateMove.SetTargetScene(current.sceneName);
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
    }

    void Update()
    {
    }
}
