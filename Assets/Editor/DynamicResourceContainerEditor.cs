using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceContainerController))]
public class DynamicResourceContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ResourceContainerController resourceContainerScript = (ResourceContainerController)target;
        
        EditorGUILayout.LabelField("String value", resourceContainerScript.stringValue);
        
        GUILayout.Label("Sprite Preview", EditorStyles.boldLabel);
        Sprite sprite = resourceContainerScript.SpriteToDisplay;
        if (sprite != null)
        {
            float maxPreviewHeight = 100f;
            Rect rect = GUILayoutUtility.GetRect(128, maxPreviewHeight);
            EditorGUI.DrawPreviewTexture(rect, sprite.texture, null, ScaleMode.ScaleToFit);
        }
        else
        {
            EditorGUILayout.HelpBox("No Sprite assigned", MessageType.Info);
        }
    }
}
