using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace PortalDefender.AavegotchiKit
{
    [CustomEditor(typeof(AppearanceFetcher))]
    public class AppearanceFetcherEditor : Editor
    {
        //old way
        /*
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AppearanceFetcher myComponent = (AppearanceFetcher)target;
            if(GUILayout.Button("Fetch"))
            {
                myComponent.Refresh();
            }
        }
        */

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            // Add the default UI
            var defaultInspector = new IMGUIContainer(() => { base.OnInspectorGUI(); });
            root.Add(defaultInspector);

            // Load the USS file from package
            string[] guids = AssetDatabase.FindAssets("AavegotchiKitEditorStyles", new[] { "Packages" });
            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                if (System.IO.Path.GetExtension(assetPath) == ".uss")
                {
                    StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(assetPath);
                    root.styleSheets.Add(styleSheet);
                }   
            }            

            //Add custom UI
            var fetchButton = new Button();
            fetchButton.name = "fetchButton";
            fetchButton.text = "Fetch";
            fetchButton.clicked += () =>
            {
                ((AppearanceFetcher)target).Refresh();
            };

            root.Add(fetchButton);

            return root;
        }

    }
}
