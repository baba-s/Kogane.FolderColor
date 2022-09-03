using System;
using UnityEngine;

namespace Kogane.Internal
{
    [Serializable]
    internal sealed class FolderColorData
    {
        [SerializeField]                    private string m_folderName;
        [SerializeField][ColorHtmlProperty] private Color  m_color;

        public string FolderName => m_folderName;
        public Color  Color      => m_color;
    }
}