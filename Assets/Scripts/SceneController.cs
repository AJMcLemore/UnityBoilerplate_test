using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private float _sceneFadeDuration;

    private SceneFade _sceneFade;

    private void Awake()
    {
        _sceneFade = GetComponentInChildren<SceneFade>();
    }

    private IEnumerator Start()
    {
        yield return _sceneFade.FadeInCoroutine(_sceneFadeDuration);
    }

    private void LoadScene(string NumberPuzzle)
    {
        StartCoroutine(LoadSceneCoroutine(NumberPuzzle));
    }

    private IEnumerator LoadSceneCoroutine(string NumberPuzzle)
    {
        yield return _sceneFade.FadeOutCoroutine(_sceneFadeDuration);
        yield return SceneManager.LoadSceneAsync(NumberPuzzle);
    }
}
