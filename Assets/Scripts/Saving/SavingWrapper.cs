using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;
using UnityEngine.SceneManagement;
using RPG.SceneManagement;

public class SavingWrapper : MonoBehaviour
{
    const string defaultSaveFile = "save";
    [SerializeField] float _fadeInTime = 0.2f;

    IEnumerator Start()
    {   
        Fader fader = FindObjectOfType<Fader>();
        fader.FadeOutImmediate();
        yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        yield return fader.FadeIn(_fadeInTime);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }

    public void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }

    public void Load()
    {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }
}
