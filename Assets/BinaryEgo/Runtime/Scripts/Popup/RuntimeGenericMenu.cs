/*
 *	Created by:  Peter @sHTiF Stefcek
 */

using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BinaryEgo.UI
{
    public class RuntimeGenericMenuItem
    {
        public GUIContent content;
        public bool separator;
        public bool state;
        public Action callback1;
        public Action<object> callback2;
        public object data;

        public RuntimeGenericMenuItem(GUIContent p_content, bool p_separator, bool p_state, Action p_callback)
        {
            content = p_content;
            separator = p_separator;
            state = p_state;
            callback1 = p_callback;
        }
        
        public RuntimeGenericMenuItem(GUIContent p_content, bool p_separator, bool p_state, Action<object> p_callback, object p_data)
        {
            content = p_content;
            separator = p_separator;
            state = p_state;
            callback2 = p_callback;
            data = p_data;

        }
    }
    
    public class RuntimeGenericMenu
    {
        public List<RuntimeGenericMenuItem> Items { get; private set; } = new List<RuntimeGenericMenuItem>();
        
        public void AddItem(GUIContent p_content, bool p_state, Action p_callback)
        {
            Items.Add(new RuntimeGenericMenuItem(p_content, false, p_state, p_callback));
        }

        public void AddItem(GUIContent p_content, bool p_state, Action<object> p_callback, object p_data)
        {
            Items.Add(new RuntimeGenericMenuItem(p_content, false, p_state, p_callback, p_data));
        }

        public void AddSeparator(string p_path)
        {
            Items.Add(new RuntimeGenericMenuItem(new GUIContent(p_path), true, false, null));   
        }
        
#if UNITY_EDITOR
        public void ShowAsEditorMenu()
        {
            GenericMenu editorMenu = new GenericMenu();

            foreach (var item in Items)
            {
                if (!item.separator)
                {
                    if (item.callback2 != null)
                    {
                        editorMenu.AddItem(item.content, item.state, (data) => item.callback2.Invoke(data), item.data);   
                    }
                    else
                    {
                        editorMenu.AddItem(item.content, item.state, () => item.callback1.Invoke());
                    }
                }
                else
                {
                    editorMenu.AddSeparator(item.content.text);
                }
            }
            
            editorMenu.ShowAsContext();
        } 
#endif
    }
}