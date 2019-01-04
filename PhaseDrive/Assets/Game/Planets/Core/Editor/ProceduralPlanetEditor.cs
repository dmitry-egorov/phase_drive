using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProceduralPlanet))]
public class ProceduralPlanetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                _planet.GeneratePlanet();
            }
        }

        if (GUILayout.Button("Generate Planet"))
        {
            _planet.GeneratePlanet();
        }
    }

    private void OnEnable()
    {
        _planet = (ProceduralPlanet) target;
    }

    private ProceduralPlanet _planet;
}