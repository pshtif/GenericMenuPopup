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
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Item 1"), false, Callback, 1);
                    menu.AddItem(new GUIContent("Item 2"), false, Callback, 2);
                    var popup = GenericMenuPopup.Get(GetTypeMenu(typeof(Component)), "");
                    popup.width = 220;
                    popup.resizeToContent = true;
                    //popup.showSearch = false;
                    popup.height = 60;
                    popup.Show(Event.current.mousePosition);
                    //menu.ShowAsContext();
                }
            }
        }
 
        static void Callback (object obj) {
            // Do something
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
                
                
                menu.AddItem(new GUIContent(name), false, null);
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