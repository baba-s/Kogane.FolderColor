using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [CustomPropertyDrawer( typeof( FolderColorData ) )]
    internal sealed class FolderColorDataDrawer : PropertyDrawer
    {
        public override void OnGUI
        (
            Rect               position,
            SerializedProperty property,
            GUIContent         label
        )
        {
            using ( new EditorGUI.PropertyScope( position, label, property ) )
            {
                position.height = EditorGUIUtility.singleLineHeight;

                const float folderNameWidth = 1f / 3;
                const float colorWidth      = 1 - folderNameWidth;

                var folderNameRect = new Rect( position )
                {
                    width = position.width * folderNameWidth,
                };

                var colorRect = new Rect( position )
                {
                    x     = folderNameRect.xMax,
                    width = position.width * colorWidth,
                };

                var folderNameProperty = property.FindPropertyRelative( "m_folderName" );
                var colorProperty      = property.FindPropertyRelative( "m_color" );

                PropertyField( folderNameRect, folderNameProperty );
                PropertyField( colorRect, colorProperty );
            }
        }

        private static void PropertyField
        (
            Rect               position,
            SerializedProperty property
        )
        {
            EditorGUI.PropertyField
            (
                position: position,
                property: property,
                label: new GUIContent( string.Empty )
            );
        }
    }
}