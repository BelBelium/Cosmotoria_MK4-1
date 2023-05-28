using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public Slider slider;

    static string nextScene;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        //yield return null;
        //AsyncOperation operation = SceneManager.LoadSceneAsync("InGameScene");
        //operation.allowSceneActivation = false;

        //while (!operation.isDone)
        //{
        //    yield return null;
        //    if (slider.value < 1f)
        //    {
        //        slider.value = Mathf.MoveTowards(slider.value, 1f, Time.deltaTime);
        //    }

        //    if (Input.GetKeyDown(KeyCode.Space) && slider.value >= 1f && operation.progress >= 0.9f)
        //    {
        //        operation.allowSceneActivation = true;
        //    }
        //}

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                slider.value = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                slider.value = Mathf.Lerp(0.9f, 1f, timer);
                if (slider.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
