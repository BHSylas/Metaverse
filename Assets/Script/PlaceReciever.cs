using UnityEngine;

public class PlaceReceiver : MonoBehaviour
{
    public static string CurrentPlace;

    public void SetCountry(string place)
    {
        CurrentPlace = place;
        Debug.Log("웹에서 받은 place: " + place);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
