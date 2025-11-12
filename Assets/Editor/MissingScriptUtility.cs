#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public static class RemoveMissingScriptsTool
{
    [MenuItem("Tools/Remove Missing Scripts In Children")]
    private static void RemoveMissingScriptsInChildren()
    {
        var selectedObjects = Selection.gameObjects;
        if (selectedObjects == null || selectedObjects.Length == 0)
        {
            Debug.LogWarning("선택된 오브젝트가 없습니다.");
            return;
        }

        int removedCount = 0;

        foreach (var root in selectedObjects)
        {
            foreach (var t in root.GetComponentsInChildren<Transform>(true))
            {
                removedCount += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(t.gameObject);
            }
        }

        Debug.Log($"Missing (Mono Script) 컴포넌트 {removedCount}개 제거 완료");
    }
}
#endif
