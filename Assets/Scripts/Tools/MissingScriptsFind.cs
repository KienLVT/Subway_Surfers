using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MissingScriptsTool
{
    const string k_missingScriptsMenuFolder = "Tools/MissingTools";

    [MenuItem(k_missingScriptsMenuFolder + "Find")]

    static void FindMissingScriptsMenuItem()
    {
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            foreach (Component component in gameObject.GetComponentsInChildren<Component>())
            {
                if (component == null)
                {
                    Debug.Log($"GameObject found with missing scripts{gameObject.name}", gameObject);
                    break;
                }
            }
        }
    }

    [MenuItem(k_missingScriptsMenuFolder + "Delete")]

    static void DeleteMissingScriptsMenuItem()
    {
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            foreach (Component component in gameObject.GetComponentsInChildren<Component>())
            {
                if (component == null)
                {
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
                    break;
                }
            }
        }
    }

}
