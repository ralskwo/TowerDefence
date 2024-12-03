using System.Linq;
using UnityEditor;
using UnityEngine;

public class SortChildHierarchyEditor : EditorWindow
{
    [MenuItem("Tools/Sort Child by Coordinates")]
    public static void SortChildByCoordinates()
    {
        // ���� ������ ������Ʈ ��������
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            Debug.LogError("������Ʈ�� �������ּ���!");
            return;
        }

        Transform parentTransform = selectedObject.transform;

        // �ڽ� Ʈ������ ��������
        Transform[] childTransforms = parentTransform.Cast<Transform>().ToArray();

        // ��ǥ ���� �� ����
        var sortedTransforms = childTransforms
            .OrderBy(child => ExtractCoordinate(child.name).y) // y�� �������� ����
            .ThenBy(child => ExtractCoordinate(child.name).x) // ���� y���� x�� �������� ����
            .ToArray();

        // ���ĵ� ������� ���� ���� ����
        for (int i = 0; i < sortedTransforms.Length; i++)
        {
            sortedTransforms[i].SetSiblingIndex(i);
        }

        Debug.Log($"'{selectedObject.name}'�� �ڽ� Ʈ�������� ��ǥ ������ ���ĵǾ����ϴ�.");
    }

    // �̸����� ��ǥ ���� �����ϴ� �Լ�
    private static (int x, int y) ExtractCoordinate(string name)
    {
        // �̸����� ���� ���� (��: "(-1, 2)" ����)
        string[] parts = name.Trim('(', ')').Split(',');
        if (parts.Length == 2 &&
            int.TryParse(parts[0].Trim(), out int x) &&
            int.TryParse(parts[1].Trim(), out int y))
        {
            return (x, y);
        }

        Debug.LogWarning($"'{name}'���� ��ǥ�� ������ �� �����ϴ�. �⺻�� (0, 0) ���.");
        return (0, 0); // �⺻�� ��ȯ
    }
}
