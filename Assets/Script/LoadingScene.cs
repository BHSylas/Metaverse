using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{

    public float waitTime = 3f;
    [SerializeField] private bool VerboseLogging = true;
    [SerializeField] private DialogInjectionReceiver receiver;
    public string NextScene;
    //wrong naming, but I want to avoid confusion with the existing VerboseLog in the project.
    //This is specifically for the loading scene to control whether to log the initialization process of the Web layer.
    #if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string OnAirportLoadedJS();
    #endif

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadNextScene());
        #if UNITY_WEBGL && !UNITY_EDITOR
        OnAirportLoadedJS();
        if (VerboseLogging)
        {
            Debug.Log("Initialized; called OnAirportLoadedJS to notify Web layer.");
            // 공항 신 로드가 완료된 후에 OnAirportLoadedJS를 호출해야 React에서 정상적으로 국가를 주입할 수 있습니다.
            // 만약 신이 로드되기 전에 주입을 시도한다면 주입하는 상태가 의도와 다르게 적용될 가능성이(보통 반영이 안 될 겁니다) 발생할 수 있습니다.
            // 이에 타 객체에서도 React에 이벤트를 전달해야 한다면 initialize 완료 후에 전달하여야 불필요한 버그를 줄일 수 있습니다.
        }
        #endif
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(waitTime);

        var nextScene = NextScene;

        if(string.IsNullOrEmpty(nextScene))
        {
            Debug.LogError($"NextScene({nextScene}, raw {NextScene}) null or empty. Cannot load next scene.");
            yield break;
        }

        SceneManager.LoadScene(nextScene);
    }
    public void SetCountryFromWeb(string countryStr)
    {
        // React에서 SendMessage로 호출하는 메서드입니다. 
        // AirportSceneController의 SetCountryFromWeb과는 별개입니다.
        // AirportSceneController는 더 이상 사용되지 않기 때문에 LoadingScene에서 국가 정보를 받아옵니다.
        // 이렇게 받는 국가 정보는 다음 신으로 넘기기 위해 필요합니다: City1, City2, City3...
        if (VerboseLogging)
        {
            Debug.Log("웹에서 받은 Country = " + countryStr);
        }

        if (!System.Enum.TryParse(countryStr, out CountryType country))
        {
            Debug.LogWarning("파싱 실패, ALL로 처리");
            country = CountryType.ALL;
        }

        NextScene = CountryToScene(country);
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

    public void InjectDialogsJson(string json)
    {
        // React에서 SendMessage로 호출하는 메서드입니다.
        // DialogInjectionReceiver의 InjectDialogsJson 메서드를 호출하여 React에서 전달된 JSON 데이터를 주입합니다.
        // 해당 메서드를 React에서 직접 호출을 시도한 결과 정상적으로 작동하지 않았기 때문에, LoadingScene에서 해당 메서드를 받아서 DialogInjectionReceiver로 전달하는 형태로 구현하였습니다.
        // DialogInjectionReceiver는 LoadingScene에만 존재합니다.
        // JSON 데이터는 React에서 전달되며, DialogInjectionReceiver에서 해당 데이터를 활용하여 대화 시스템에 적용합니다.
        if (VerboseLogging)
        {
            Debug.Log($"InjectDialogsJson called. raw={json}, payloadLength={(json ?? string.Empty).Length}");
        }
        if(receiver is not null) 
        {
            receiver.InjectDialogsJson(json);
        }
        else
        {
            Debug.LogError("DialogInjectionReceiver reference is null. Cannot inject dialogs JSON.");
        }
    }
}
