using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Helpers
{
    public static class ClipboardExtension
    {

        public static void CopyToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }

    }
}

