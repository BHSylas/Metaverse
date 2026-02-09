using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GateMove : MonoBehaviour
{

    public string TargetScene;

    public void SetTargetScene(string sceneName)
    {
        TargetScene = sceneName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) 
            return;

        if (string.IsNullOrEmpty(TargetScene))
        {
            Debug.LogWarning("TargetScene X");
            return;
        }

        Debug.Log("이동: " + TargetScene);

        PlayerPrefs.SetString("NextScene", TargetScene);

        PlayerPrefs.Save();


        StartCoroutine(LoadWithDelay());

        

    }


    private IEnumerator LoadWithDelay()
    {
        yield return null; // ⭐ 한 프레임 대기
        SceneManager.LoadScene("LoadingScene");
    }





}
