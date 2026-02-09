using UnityEngine;
using UnityEngine.SceneManagement;

public class AirportSceneController : MonoBehaviour
{
    public PortalDestination[] destinations;
    private PortalDestination current;
    private GateMove gateMove;
    private CountryType currentCountry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Awake()
    {
        gateMove = GetComponent<GateMove>();

        if(gateMove == null)
        {
            Debug.LogError("GateMove component not found!");
        }
    }

    public void SetCountry(CountryType country)
    {

        currentCountry = country;

        current = null;

        foreach(var d in destinations)
        {
            if(d.country == country)
            {
                current = d;
                break;
            }
        }

        gameObject.SetActive(current != null);

        if(current!=null)
        {
            gateMove.SetTargetScene(current.sceneName);
            Debug.Log($"목적지 설정 완료 : {country} {current.sceneName}");
        }
        else
        {
            Debug.LogWarning($"목적지 없음 : {country}");
        }

    }

    public void SetCountryFromWeb(string countryStr)
    {
        Debug.Log("웹에서 받은 Country = " + countryStr);

        if (!System.Enum.TryParse(countryStr, out CountryType country))
        {
            Debug.LogWarning("파싱 실패, ALL로 처리");
            country = CountryType.ALL;
        }

        SetCountry(country);
    }

    void Start()
    {
        Debug.Log("SetCountry 호출됨: " + currentCountry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
