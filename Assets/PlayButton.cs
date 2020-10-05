﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float PointerScale = 1;
    public AudioClip HoverClip;
    private AudioSource source;
    public Image image;
    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = HoverClip;
        source.playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.Play();
        transform.localScale *= PointerScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale /= PointerScale;
    }

    public void Click()
    {
        StartCoroutine(OnClickRoutine());
    }

    IEnumerator OnClickRoutine()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float currentProgress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            image.fillAmount = currentProgress;
            
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
                yield return new WaitForSeconds(0.1f);
            }

            yield return null;
        }
    }
}
