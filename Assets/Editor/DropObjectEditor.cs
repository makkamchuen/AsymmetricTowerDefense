using UnityEditor;
using UnityEngine;

public class DropObjectsEditor : EditorWindow
{
    RaycastHit hit;
    float yOffset;
    int savedLayer;
    bool AlignNormals;
    Vector3 UpVector = new Vector3(0, 90, 0);
    [MenuItem("Window/Drop Object")] // add menu item    

    static void Awake()
    {
        EditorWindow.GetWindow<DropObjectsEditor>().Show(); // Get existing open window or if none, make a new one
    }

    void OnGUI()
    {
        GUILayout.Label("Drop using: ", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Bottom"))
        {
            DropObjects("Bottom");
        }

        if (GUILayout.Button("Origin"))
        {
            DropObjects("Origin");
        }

        if (GUILayout.Button("Center"))
        {
            DropObjects("Center");
        }

        EditorGUILayout.EndHorizontal();
        AlignNormals = EditorGUILayout.ToggleLeft("Align Normals", AlignNormals); // toggle to align the object with the normal direction of the surface
        if (AlignNormals)
        {
            EditorGUILayout.BeginHorizontal();
            UpVector = EditorGUILayout.Vector3Field("Up Vector", UpVector); // Vector3 helping to specify the Up vector of the object
            // default has 90° on the Y axis, this is because by default
            // the objects I import have a rotation.
            // If anyone has a better way to do this I'd be happy
            // to see a better solution!
            GUILayout.EndHorizontal();
        }
    }

    void DropObjects(string Method)
    {
        Undo.RecordObjects(Selection.transforms, "Drop Objects");

        for (int i = 0; i < Selection.transforms.Length; i++) // drop multi-selected objects using the selected method
        {
            GameObject go = Selection.transforms[i].gameObject; // get the gameobject
            if (!go) { continue; } // if there's no gameobject, skip the step — probably unnecessary but hey…

            Bounds bounds = go.GetComponent<Renderer>().bounds; // get the renderer's bounds
            savedLayer = go.layer; // save the gameobject's initial layer
            go.layer = 2; // set the gameobject's layer to ignore raycast

            if (Physics.Raycast(go.transform.position, -Vector3.up, out hit)) // check if raycast hits something
            {
                switch (Method) // determine how the y position will need to be adjusted
                {
                    case "Bottom":
                        yOffset = go.transform.position.y - bounds.min.y;
                        break;
                    case "Origin":
                        yOffset = 0f;
                        break;
                    case "Center":
                        yOffset = bounds.center.y - go.transform.position.y;
                        break;
                }
                if (AlignNormals) // if "Align Normals" is checked, set the gameobject's rotation
                // to match the raycast's hit position's normal, and add the specified offset.
                {
                    go.transform.up = hit.normal + UpVector;
                }
                go.transform.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
            }
            go.layer = savedLayer; // restore the gameobject's layer to it's initial layer
        }
    }
}