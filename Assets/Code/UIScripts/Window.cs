using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An abstract class. 
//Note: this should be abstracted more out. Elements such as "ability" and "attack attempt" are specific
// to certain implmemetnations. 
public abstract class Window : MonoBehaviour
{
    //###MemberVeriables###
    protected bool affectdByFocus = true;


    List<Window> childrenWindows;
    Window parentWindow;

    //###Getters###
    public Window getParent()
    {
        return parentWindow;
    }

    public bool getAffectedByFocus()
    {
        return affectdByFocus;
    }


    //###Utilities###
    //It needs to be recursive, because the childen may not necissarily be children in unityland
    public virtual void closeWindow()
    {
        try
        {
            GameObject.Destroy(this.gameObject);
        }
        catch (MissingReferenceException e)
        {

        }
        if (!(childrenWindows is null))
        {
            if (childrenWindows.Count > 0)
            {
                foreach (Window child in childrenWindows)
                {
                    child.closeWindow();
                }
            }
        }
        UIController.checkForDeleted();
    }

    //###Initializers###
    public void initialize(Window parent)
    {
        parentWindow = parent;
        childrenWindows = new List<Window>();
    }
}