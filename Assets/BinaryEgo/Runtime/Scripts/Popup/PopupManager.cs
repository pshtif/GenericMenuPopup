/*
 *	Created by:  Peter @sHTiF Stefcek
 */

using System.Collections.Generic;
using UnityEngine;

namespace BinaryEgo.UI
{
    public class PopupManager
    {
        public static List<AbstractPopup> Popups { get; } = new List<AbstractPopup>();

        public static void AddPopup(AbstractPopup p_popup)
        {
            if (!Popups.Contains(p_popup))
            {
                Popups.Add(p_popup);
            }
        }
        
        public static void RemovePopup(AbstractPopup p_popup)
        {
            if (Popups.Contains(p_popup))
            {
                Popups.Remove(p_popup);
            }
        }

        public static void OnGUI(Rect p_rect)
        {
            Popups.ForEach(p =>
            {
                p.OnGUI(p_rect);
            });
        }
    }
}