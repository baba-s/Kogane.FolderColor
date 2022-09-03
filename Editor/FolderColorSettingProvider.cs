using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    internal sealed class FolderColorSettingProvider : SettingsProvider
    {
        private sealed class PresetData
        {
            public string Name { get; }
            public string Guid { get; }

            public PresetData
            (
                string name,
                string guid
            )
            {
                Name = name;
                Guid = guid;
            }
        }

        private const string PATH = "Kogane/Folder Color";

        private static readonly PresetData[] m_presetArray =
        {
            new( "Tailwind 100", "862a03eb0a486a8458852da803694624" ),
            new( "Tailwind 200", "34e72f1b5d5099943a2bb87568c8e5f8" ),
            new( "Tailwind 300", "e2f51da6976c28e479bdd8bb7d6cc8c9" ),
            new( "Tailwind 400", "210b2283c9e284c4d8570f2b0246c41e" ),
            new( "Tailwind 500", "da2e807d4c9bcae42b7f3c86d8f9ff76" ),
            new( "Tailwind 600", "460ab1aed532bc449aaa19871b8f622c" ),
            new( "Tailwind 700", "db094a1e9ffff64488dc72db3ecb7d69" ),
            new( "Tailwind 800", "16452bd0eda515d48855c3623c098a5c" ),
            new( "Tailwind 900", "962ba1e51fda4634fa0adc113ef4e2a1" ),
        };

        private Editor m_editor;

        private FolderColorSettingProvider
        (
            string              path,
            SettingsScope       scopes,
            IEnumerable<string> keywords = null
        ) : base( path, scopes, keywords )
        {
        }

        public override void OnActivate( string searchContext, VisualElement rootElement )
        {
            var instance = FolderColorSetting.instance;

            instance.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            Editor.CreateCachedEditor( instance, null, ref m_editor );
        }

        public override void OnGUI( string searchContext )
        {
            using var changeCheckScope = new EditorGUI.ChangeCheckScope();

            var setting = FolderColorSetting.instance;

            if ( GUILayout.Button( "Load Preset" ) )
            {
                var menu = new GenericMenu();

                foreach ( var presetData in m_presetArray )
                {
                    menu.AddItem
                    (
                        content: new GUIContent( presetData.Name ),
                        on: false,
                        func: () => OnSelectedMenu( presetData )
                    );
                }

                menu.ShowAsContext();
            }

            using ( new EditorGUILayout.HorizontalScope() )
            {
                if ( GUILayout.Button( "Copy Json" ) )
                {
                    var json = JsonUtility.ToJson( setting, true );
                    EditorGUIUtility.systemCopyBuffer = json;
                    Debug.Log( json );
                }

                if ( GUILayout.Button( "Paste Json" ) )
                {
                    var json = EditorGUIUtility.systemCopyBuffer;
                    if ( string.IsNullOrWhiteSpace( json ) ) return;
                    Undo.RecordObject( setting, "Load" );
                    JsonUtility.FromJsonOverwrite( json, setting );
                }
            }

            m_editor.OnInspectorGUI();

            if ( !changeCheckScope.changed ) return;

            setting.Save();
        }

        private static void OnSelectedMenu( PresetData presetData )
        {
            var setting   = FolderColorSetting.instance;
            var path      = AssetDatabase.GUIDToAssetPath( presetData.Guid );
            var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>( path );
            var json      = textAsset.text;

            Undo.RecordObject( setting, "Load" );
            JsonUtility.FromJsonOverwrite( json, setting );
        }

        [SettingsProvider]
        private static SettingsProvider CreateSettingProvider()
        {
            return new FolderColorSettingProvider
            (
                path: PATH,
                scopes: SettingsScope.Project
            );
        }
    }
}