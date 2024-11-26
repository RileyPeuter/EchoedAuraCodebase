using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuController : MonoBehaviour
{
    AudioSource musicController;

    GlobalGameController GGC;
    public void initialize(GlobalGameController nGGC)
    {
        GGC = nGGC;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void toggleAllSound()
    {
    
    }

    public void alterMasterVolume(float nVolume)
    {
        AudioListener.volume = nVolume; 
    }

    void toggleMusic()
    {
        GGC.gameObject.GetComponent<AudioSource>().Stop();
    }

    void close()
    {

    }

    void returnToMainMenu()
    {
        GGC.openMainMenu();
    }
}
