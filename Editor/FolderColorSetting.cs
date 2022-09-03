using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [FilePath( "ProjectSettings/Kogane/FolderColor.asset", FilePathAttribute.Location.ProjectFolder )]
    internal sealed class FolderColorSetting
        : ScriptableSingleton<FolderColorSetting>,
          IEnumerable<FolderColorData>
    {
        [SerializeField] private List<FolderColorData> m_list = new();

        public void Save()
        {
            Save( true );
        }

        public IEnumerator<FolderColorData> GetEnumerator()
        {
            return ( ( IEnumerable<FolderColorData> )m_list ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}