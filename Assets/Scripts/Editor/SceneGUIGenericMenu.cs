/*
 *	Created by:  Peter @sHTiF Stefcek
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Shtif;

namespace Editor
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;
 
    [InitializeOnLoad]
    public class SceneGUIGenericMenu : Editor {
 
        static SceneGUIGenericMenu () {
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
        }
 
        static void OnSceneGUI (SceneView sceneview) {
            if (Event.current.button == 1)
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    var menu = GetExampleMenu();
                    //menu.ShowAsContext();
                    GenericMenuPopup.Show(menu, "", Event.current.mousePosition);
                    
                    //var menu = GetTypeMenu(typeof(Component));
                    // var popup = GenericMenuPopup.Get(GetTypeMenu(typeof(Component)), "Unity Type");
                    // popup.width = 220;
                    // popup.showTooltip = true;
                    // popup.resizeToContent = true;
                    // popup.height = 60;
                    // popup.Show(Event.current.mousePosition);
                }
            }
        }
 
        static void Callback (object obj) {
            Debug.Log(obj);
        }

        static public GenericMenu GetExampleMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Edit"), false, Callback, 1);
            menu.AddItem(new GUIContent("File"), false, Callback, 2);
            menu.AddItem(new GUIContent("Submenu/Import"), false, Callback, 3);
            menu.AddItem(new GUIContent("Submenu/Export"), false, Callback, 4);
            menu.AddItem(new GUIContent("About"), false, Callback, 5);

            return menu;
        }
        
        static public GenericMenu GetTypeMenu(Type p_type)
        {
            GenericMenu menu = new GenericMenu();
            
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
    }
}