using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
{
    public static List<Window> openWindows;
    //Transient Windows are windows that will close should a significant event happen
    public static List<Window> transientWindows;
    public static List<BackButtonController> backButtonControllers;

    public static float UIBreatheTicker;
    public static bool breathDirection;

    //A dummy object
    public static Window HighestWindow;

    //###MemberVariables###
    
    //Prefab of the back button
    GameObject backButton;
    BattleController BC;
    Window focusedWindow;

    //###Utlitlies###
    public void checkBackButtons()
    {
        if (backButtonControllers.Count > 0) { 
            if (backButtonControllers[backButtonControllers.Count - 1].checkBack())
            {
                backButtonControllers.Remove(backButtonControllers[backButtonControllers.Count - 1]);
            } 
        }

        BackButtonController BBC = null;

        foreach(BackButtonController controller in backButtonControllers)
        {
            if (controller.getButtonPressed())
            {
                BBC = controller;
                BBC.Back();
            } 
        }
        if (BBC != null)
        {
            backButtonControllers.Remove(BBC);
        }
    }

    void incrimentBreathe()
    {
        if (breathDirection)
        {
            UIBreatheTicker = UIBreatheTicker + Time.deltaTime * 0.5f;
        }
        else {
            UIBreatheTicker = UIBreatheTicker - Time.deltaTime * 0.5f;
        }

        if(UIBreatheTicker > 1)
        {
            breathDirection = false;
        }
        if(UIBreatheTicker < 0)
        {
            breathDirection = true;
        }
    }

    public void closeWindow(GameObject item)
    {
        GameObject.Destroy(item);
    }

    public void closeAllTransient()
    {
        checkForDeleted();
        foreach (Window window in transientWindows)
        {   //This checks if it's null, please don't be dumb and remove this, you will regret it
            if (window)
                window.closeWindow();
        }

        backButtonControllers.Clear();
    }

    public GameObject openWindow(string windowName, bool transient = true, string parentName = "Canvas", bool hasBackButton = true)
    {
        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/" + windowName), GameObject.Find(parentName).transform);
        if (transient)
        {
            BattleUIController.transientWindows.Add(go.GetComponent<Window>());
        }

        openWindows.Add(go.GetComponent<Window>());

        if (hasBackButton)
        {
            BackButtonController BBC = GameObject.Instantiate(backButton, go.transform).GetComponentInChildren<BackButtonController>();
            BBC.initialize(this);
            backButtonControllers.Add(BBC);
        }

        return go;
    }

    public void makeActive(bool toggle)
    {
        checkForDeleted();
        foreach (Window window in openWindows)
        {
            window.enabled = toggle;
        }
    }

    //Makes it so that only one window is interactable
    public void lockFocus(Window focusedWindow)
    {
        checkForDeleted();
        foreach (Window window in openWindows)
        {
            if (window.getAffectedByFocus() && focusedWindow != window)
            {
                foreach (Button button in window.GetComponentsInChildren<Button>())
                {
                    button.interactable = false;
                }
            }
        }
        foreach (Button button in focusedWindow.GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }
    }

    //Inverse of lock focus
    public void releaseFocus()
    {
        foreach (Window window in openWindows.ToArray())
        {
            foreach (Button button in window.GetComponentsInChildren<Button>())
            {
                button.interactable = true;
            }
        }
    }

    //This is to just cleanup the lists incase windows get closed
    public static void checkForDeleted()
    {
        foreach (Window window in openWindows.ToArray())
        {
            if (window == null)
            {
                openWindows.Remove(window);
            }
        }
        foreach (Window window in transientWindows.ToArray())
        {
            if (window == null)
            {
                transientWindows.Remove(window);
            }
        }
    }

    public void closeWindow(Window window)
    {
        lockFocus(window.getParent());
    }

    //###Initializer###
    public void initialize(BattleController BaCo)
    {
        BC = BaCo;

        BattleUIController.openWindows = new List<Window>();
        BattleUIController.transientWindows = new List<Window>();
        BattleUIController.backButtonControllers = new List<BackButtonController>();

        backButton = Resources.Load<GameObject>("UIElements/uI_BackButton_Panel");
    }

    //###UnityMessages###
    private void Update()
    {
        incrimentBreathe();
    }
}

