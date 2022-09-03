using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal static class FolderColor
    {
        private static Texture2D m_imageCache;

        private static Texture2D Image
        {
            get
            {
                if ( m_imageCache != null ) return m_imageCache;

                var imagePath = AssetDatabase.GUIDToAssetPath( "d66445f0899e03442aba34473aee7242" );

                m_imageCache = AssetDatabase.LoadAssetAtPath<Texture2D>( imagePath );

                return m_imageCache;
            }
        }

        static FolderColor()
        {
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }

        private static void OnGUI( string guid, Rect selectionRect )
        {
            var assetPath = AssetDatabase.GUIDToAssetPath( guid );

            if ( string.IsNullOrWhiteSpace( assetPath ) ) return;
            if ( !AssetDatabase.IsValidFolder( assetPath ) ) return;

            var setting    = FolderColorSetting.instance;
            var folderName = Path.GetFileNameWithoutExtension( assetPath );

            var data = setting.FirstOrDefault( x => x.FolderName == folderName );

            if ( data == null ) return;

            // GUI.DrawTexture
            // (
            //     position: GetBackgroundRect( selectionRect ),
            //     image: EditorGUIUtility.whiteTexture,
            //     scaleMode: ScaleMode.StretchToFill,
            //     alphaBlend: true,
            //     imageAspect: 0,
            //     color: new Color32( 56, 56, 56, 255 ),
            //     borderWidth: 0,
            //     borderRadius: 0
            // );

            GUI.DrawTexture
            (
                position: GetImagePosition( selectionRect ),
                image: Image,
                scaleMode: ScaleMode.StretchToFill,
                alphaBlend: true,
                imageAspect: 0,
                color: data.Color,
                borderWidth: 0,
                borderRadius: 0
            );
        }

        private static Rect GetImagePosition( Rect selectionRect )
        {
            var position    = selectionRect;
            var isOneColumn = position.height < position.width;

            if ( isOneColumn )
            {
                position.width = position.height;
            }
            else
            {
                position.height = position.width;
            }

            return position;
        }
    }
}