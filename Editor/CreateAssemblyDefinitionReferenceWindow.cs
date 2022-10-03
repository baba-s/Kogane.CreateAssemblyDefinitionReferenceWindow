using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Kogane.Internal
{
    internal sealed class CreateAssemblyDefinitionReferenceWindow : EditorWindow
    {
        private const float WINDOW_HEIGHT = 96;
        private const float LABEL_WIDTH   = 144;

        private string                  m_directory = string.Empty;
        private string                  m_name      = "NewAssemblyReference";
        private AssemblyDefinitionAsset m_assemblyDefinition;
        private bool                    m_isInitialized;

        private bool CanCreate => !string.IsNullOrWhiteSpace( m_name );

        [MenuItem( "Assets/Kogane/Create Assembly Definition Reference", priority = 1156162170 )]
        private static void Open()
        {
            var asset     = Selection.activeObject;
            var assetPath = AssetDatabase.GetAssetPath( asset );
            var directory = string.IsNullOrWhiteSpace( assetPath ) ? "Assets" : assetPath;

            if ( !AssetDatabase.IsValidFolder( directory ) )
            {
                directory = Path
                        .GetDirectoryName( directory )
                        ?.Replace( "\\", "/" )
                    ;
            }

            var window = GetWindow<CreateAssemblyDefinitionReferenceWindow>
            (
                utility: true,
                title: "Create Assembly Definition Reference",
                focus: true
            );

            window.m_directory = directory;

            var minSize = window.minSize;
            var maxSize = window.maxSize;

            minSize.y = WINDOW_HEIGHT;
            maxSize.y = WINDOW_HEIGHT;

            window.minSize = minSize;
            window.maxSize = maxSize;
        }

        private void OnGUI()
        {
            var current = Event.current;

            if ( current.keyCode == KeyCode.Return )
            {
                if ( CanCreate )
                {
                    OnCreate();
                    current.Use();
                    return;
                }
            }

            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = LABEL_WIDTH;

            EditorGUILayout.LabelField( "Path", m_directory );

            GUI.SetNextControlName( "Name" );
            m_name = EditorGUILayout.TextField( "Name", m_name );

            m_assemblyDefinition = ( AssemblyDefinitionAsset )EditorGUILayout.ObjectField
            (
                label: "Assembly Definition",
                obj: m_assemblyDefinition,
                objType: typeof( AssemblyDefinitionAsset ),
                allowSceneObjects: false
            );

            EditorGUIUtility.labelWidth = oldLabelWidth;

            using ( new EditorGUI.DisabledScope( !CanCreate ) )
            {
                if ( GUILayout.Button( "Create" ) )
                {
                    OnCreate();
                }
            }

            if ( !m_isInitialized )
            {
                m_isInitialized = true;

                EditorGUI.FocusTextInControl( "Name" );
            }
        }

        private void OnCreate()
        {
            var path       = $"{m_directory}/{m_name}.asmref";
            var uniquePath = AssetDatabase.GenerateUniqueAssetPath( path );
            var assetPath  = AssetDatabase.GetAssetPath( m_assemblyDefinition );
            var guid       = AssetDatabase.AssetPathToGUID( assetPath );

            var data = new JsonAssemblyDefinitionReference
            {
                reference = $"GUID:{guid}",
            };

            var json = JsonUtility.ToJson( data, true );

            File.WriteAllText( uniquePath, json, Encoding.UTF8 );

            AssetDatabase.Refresh();

            Close();
        }
    }
}