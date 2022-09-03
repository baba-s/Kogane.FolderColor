using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [CustomPropertyDrawer( typeof( ColorHtmlPropertyAttribute ) )]
    internal sealed class ColorHtmlPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
        {
            var htmlField  = new Rect( position.x, position.y, position.width / 2, position.height );
            var colorField = new Rect( position.x + htmlField.width, position.y, position.width - htmlField.width, position.height );
            var htmlValue  = EditorGUI.TextField( htmlField, label, "#" + ColorUtility.ToHtmlStringRGB( property.colorValue ) );

            if ( ColorUtility.TryParseHtmlString( htmlValue, out var newColor ) )
            {
                property.colorValue = newColor;
            }

            property.colorValue = EditorGUI.ColorField( colorField, property.colorValue );
        }
    }
}