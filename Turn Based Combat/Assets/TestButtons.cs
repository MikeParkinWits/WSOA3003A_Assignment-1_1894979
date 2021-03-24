using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestButtons : MonoBehaviour
{

    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnShowRules()
    {
        image.SetActive(true);
    }

    public void OnCloseRules()
    {
        image.SetActive(false);
    }
}
