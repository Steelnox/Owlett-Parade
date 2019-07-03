using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerCamera))]
public class CameraCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerCamera script = (PlayerCamera)target;

        if (GUILayout.Button("SetCameraOffset"))
        {
            script.SetOffset();
        }

        if (GUILayout.Button("CenterCameraToPlayer"))
        {
            script.CenterToTarget();
        }
    }
}
