using System.Linq;
using UnityEditor;
using UnityEngine;

public class SortChildHierarchyEditor : EditorWindow
{
    [MenuItem("Tools/Sort Child by Coordinates")]
    public static void SortChildByCoordinates()
    {
        // 현재 선택한 오브젝트 가져오기
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            Debug.LogError("오브젝트를 선택해주세요!");
            return;
        }

        Transform parentTransform = selectedObject.transform;

        // 자식 트랜스폼 가져오기
        Transform[] childTransforms = parentTransform.Cast<Transform>().ToArray();

        // 좌표 추출 및 정렬
        var sortedTransforms = childTransforms
            .OrderBy(child => ExtractCoordinate(child.name).y) // y를 기준으로 정렬
            .ThenBy(child => ExtractCoordinate(child.name).x) // 같은 y에서 x를 기준으로 정렬
            .ToArray();

        // 정렬된 순서대로 계층 구조 변경
        for (int i = 0; i < sortedTransforms.Length; i++)
        {
            sortedTransforms[i].SetSiblingIndex(i);
        }

        Debug.Log($"'{selectedObject.name}'의 자식 트랜스폼이 좌표 순으로 정렬되었습니다.");
    }

    // 이름에서 좌표 값을 추출하는 함수
    private static (int x, int y) ExtractCoordinate(string name)
    {
        // 이름에서 숫자 추출 (예: "(-1, 2)" 형식)
        string[] parts = name.Trim('(', ')').Split(',');
        if (parts.Length == 2 &&
            int.TryParse(parts[0].Trim(), out int x) &&
            int.TryParse(parts[1].Trim(), out int y))
        {
            return (x, y);
        }

        Debug.LogWarning($"'{name}'에서 좌표를 추출할 수 없습니다. 기본값 (0, 0) 사용.");
        return (0, 0); // 기본값 반환
    }
}
