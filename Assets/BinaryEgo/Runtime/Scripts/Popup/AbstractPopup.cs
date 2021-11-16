/*
 *	Created by:  Peter @sHTiF Stefcek
 */

using UnityEngine;

namespace BinaryEgo.UI
{
    public abstract class AbstractPopup
    {
        public Rect rect;

        public static void Show(Rect p_rect, AbstractPopup p_popup)
        {
            Vector2 size = p_popup.GetWindowSize();
            p_popup.rect = new Rect(p_rect.x, p_rect.y, size.x, size.y);
            PopupManager.AddPopup(p_popup);
        }

        public abstract Vector2 GetWindowSize();

        public abstract void OnGUI(Rect p_rect);

        public void Close()
        {
            
        }
    }
}