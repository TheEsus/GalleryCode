using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Text LoadingPercentage;
    public Image LoadingProgressBar;
    private static SceneTransition instance;
    private AsyncOperation loadingSceneOperation;
    private Animator componentAnimator;

    public static void SwitchToScene(string sceneName){
        instance.componentAnimator.SetTrigger("SceneClosing");

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        //Запрет на автоматическую загрузку следующей сцены
        instance.loadingSceneOperation.allowSceneActivation = false;
    }
    void Start()
    {
        instance = this;
        componentAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(loadingSceneOperation != null){
            LoadingPercentage.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) +  "%";
            LoadingProgressBar.fillAmount = loadingSceneOperation.progress;
        }
    }
    
    public void OnAnimationOver(){
        instance.loadingSceneOperation.allowSceneActivation = true;
    }
}
