using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : SingletonPersistent<LevelManager>
{
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private GameObject _progressBar;
    private float _target;

    public async void LoadScene(string sceneName)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _target = 0;
        _progressBar.GetComponent<Slider>().value = 0;
        _loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            _target = scene.progress;

        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }

    void Update()
    {
        _progressBar.GetComponent<Slider>().value = Mathf.MoveTowards(_progressBar.GetComponent<Slider>().value, _target, 3 * Time.deltaTime);
    }
}