using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FiveGranade))]
public class FiveGranadeInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FiveGranade myScript = (FiveGranade)target;
        if (GUILayout.Button("Line Positions"))
        {
            myScript.SetLinePositions();
        }
        if (GUILayout.Button("Parts Positions"))
        {
            myScript.SetPartsPositions();
        }
        if (GUILayout.Button("Collider"))
        {
            myScript.SetCollider();
        }
    }
}