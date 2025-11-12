using UnityEngine;
using UnityEditor;

public class UniGLTF_UniUnlitReplacer : EditorWindow
{
    private GameObject targetObject;

    [MenuItem("Tools/UniGLTF/Replace Shader to UniUnlit")]
    public static void ShowWindow()
    {
        GetWindow<UniGLTF_UniUnlitReplacer>("Replace to UniUnlit");
    }

    private void OnGUI()
    {
        GUILayout.Label(" Replace All Shaders to UniGLTF/UniUnlit", EditorStyles.boldLabel);
        targetObject = (GameObject)EditorGUILayout.ObjectField("Target Root Object", targetObject, typeof(GameObject), true);

        if (GUILayout.Button("Replace Shaders"))
        {
            if (targetObject == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign the target GameObject.", "OK");
                return;
            }

            ReplaceShadersToUniUnlit(targetObject);
        }
    }

    private void ReplaceShadersToUniUnlit(GameObject root)
    {
        Shader uniUnlitShader = Shader.Find("UniGLTF/UniUnlit");

        if (uniUnlitShader == null)
        {
            Debug.LogError(" Shader 'UniGLTF/UniUnlit' not found. Please make sure UniGLTF is installed.");
            return;
        }

        int changedCount = 0;
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.sharedMaterials)
            {
                if (mat != null && mat.shader != uniUnlitShader)
                {
                    Undo.RecordObject(mat, "Change Shader to UniUnlit");
                    mat.shader = uniUnlitShader;
                    changedCount++;
                }
            }
        }

        Debug.Log($" {changedCount} material(s) converted to UniGLTF/UniUnlit shader under: {root.name}");
        AssetDatabase.SaveAssets();
    }
}
