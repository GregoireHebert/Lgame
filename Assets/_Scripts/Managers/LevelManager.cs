using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager: SingletonPersistent<LevelManager>
{
    [SerializeField] private GameObject LoaderCanvas;
    [SerializeField] private GameObject ProgressBar;

    private float Target;

    public async void LoadScene(string sceneName) {
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        Target = 0;
        ProgressBar.GetComponent<Slider>().value = 0;
        LoaderCanvas.SetActive(true);

        do {
            await Task.Delay(100);
            Target = scene.progress;
            
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        LoaderCanvas.SetActive(false);
    }   

    void Update() {
        ProgressBar.GetComponent<Slider>().value = Mathf.MoveTowards(ProgressBar.GetComponent<Slider>().value, Target, 3 * Time.deltaTime);
    }
}