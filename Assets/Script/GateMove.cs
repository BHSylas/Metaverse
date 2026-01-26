using UnityEngine;
using UnityEngine.SceneManagement;

public class GateMove : MonoBehaviour
{

    public string TargetScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("무언가 닿음: " + collision.name);


        if (!collision.CompareTag("Player"))
            return;


        Debug.Log("플레이어 감지됨!");

        PlayerPrefs.SetString("NextScene", TargetScene);

        SceneManager.LoadScene("LoadingScene");
    }


    // Start i
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
