// This example shows a custom inspector for an
// object "MyPlayer", which has a variable speed.
using UnityEditor;
using Assets.ECS;
using UnityEngine;

[CustomEditor(typeof(MultiSystem), true)]
public class MultiSystemEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var t = (MultiSystem)target;
        showComponents = EditorGUILayout.Foldout(showComponents, $"Components[{t.GetComponentsCount()}]");
        if (showComponents)
        {
            GUI.enabled = false;
            foreach (var mb in t.GetComponents())
            {
                EditorGUILayout.ObjectField(mb, typeof(MonoBehaviour), true);
            }
            GUI.enabled = true;
        }

    }

    private bool showComponents;
}