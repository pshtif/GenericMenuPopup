using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BinaryEgo.UI;
using UnityEngine;

public class RuntimeMenuPopupTest : MonoBehaviour
{
    void OnGUI()
    {
        if (Event.current.isMouse && Event.current.button == 1)
        {
            popup.Show(Event.current.mousePosition);
        }
        
        PopupManager.OnGUI(new Rect(0,0,Screen.width, Screen.height));
    }

    private MenuPopup popup;

    void Start()
    {
        var menu = GetTypeMenu(typeof(Component));
        //var menu = GetExampleMenu();
        popup = MenuPopup.Get(menu, "RuntimeGenericMenu");
        popup.width = 220;
        popup.showSearch = true;
        popup.showTooltip = false;
        popup.resizeToContent = true;
    }

    void Update()
    {

    }
    
    static public RuntimeGenericMenu GetTypeMenu(Type p_type)
    {
        RuntimeGenericMenu menu = new RuntimeGenericMenu();
            
        Type[] types = GetAllTypes(p_type).ToArray();
        Array.Sort(types, (t1, t2) => t1.ToString().CompareTo(t2.ToString()));

        foreach (Type type in types)
        {
            string name = type.ToString();//.Substring(type.ToString().IndexOf(".") + 1);
            name = name.Replace('.', '/');
            //name = name.Substring(0, name.Length-4);
                
                
            menu.AddItem(new GUIContent(name, type.ToString()), false, null);
        }

        return menu;
    }
        
    public static List<Type> GetAllTypes(Type p_type)
    {
        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
            .Where(x => p_type.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();
    }
    
    static public RuntimeGenericMenu GetExampleMenu()
    {
        RuntimeGenericMenu menu = new RuntimeGenericMenu();
        menu.AddItem(new GUIContent("Edit"), false, Callback, 1);
        menu.AddItem(new GUIContent("File"), false, Callback, 2);
        menu.AddItem(new GUIContent("Submenu/Import"), true, Callback, 3);
        menu.AddItem(new GUIContent("Submenu/Export"), false, Callback, 4);
        menu.AddItem(new GUIContent("About"), false, Callback, 5);

        return menu;
    }
    
    static void Callback (object obj) {
        Debug.Log(obj);
    }
}
