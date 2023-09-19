using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���Ӱ����Լ� + Obj
public static partial class GFunc
{
    // Ư�� GameObject�� �ڽ� ������Ʈ�� ��ġ�ؼ� ã���ִ� �Լ�
    public static GameObject FindChildObj(this GameObject targetObj, string objName)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;

        // �ڽ� ������Ʈ���� �ݺ��ϸ鼭 �˻�
        for (int i = 0; i < targetObj.transform.childCount; i++)
        {
            searchTarget = targetObj.transform.GetChild(i).gameObject;

            // ��ġ�ϴ� ��� ��ȯ
            if (searchTarget.name.Equals(objName))
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                // ���;������ �ڽ� ������Ʈ �˻�
                searchResult = FindChildObj(searchTarget, objName);

                // ������ : �˻� ����� null�� ��츦 ó��
                if (searchResult == null || searchResult == default) 
                { 
                    
                }
                else 
                { 
                    return searchResult; 
                }
            }
        }

        return searchResult;
    }


    // ���� ��Ʈ ������Ʈ�� ��ġ�ؼ� ã���ִ� �Լ�
    public static GameObject GetRootObj(string objName)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObjs = activeScene_.GetRootGameObjects();

        GameObject targetObj = default;

        // ��Ʈ ������Ʈ���� �ݺ��Ͽ� �˻�
        foreach (GameObject rootObj in rootObjs)
        {
            // ��ġ�ϴ� ��� ��ȯ
            if (rootObj.name.Equals(objName))
            {
                targetObj = rootObj;
                return targetObj;
            }
            else 
            { 
                continue; 
            }
        }

        return targetObj;
    }


    // RectTransform ���� sizeDelta�� ã�Ƽ� �����ϴ� �Լ�
    public static Vector2 GetRectSizeDelta(this GameObject obj)
    {
        return obj.GetComponentMust<RectTransform>().sizeDelta;
    }


    // ���� Ȱ��ȭ �Ǿ� �ִ� ���� ã���ִ� �Լ�
    public static Scene GetActiveScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        return activeScene;
    }


    // GameObject�� ���� �������� �����ϴ� �Լ�
    public static void SetLocalPos(this GameObject obj, float x, float y, float z)
    {
        obj.transform.localPosition = new Vector3(x, y, z);
    }


    // GameObject�� ���� �������� �����ϴ� �Լ�
    public static void AddLocalPos(this GameObject obj, float x, float y, float z)
    {
        obj.transform.localPosition = obj.transform.localPosition + new Vector3(x, y, z);
    }


    // Transform�� ����ؼ� GameObject�� �̵��ϴ� �Լ�
    public static void Translate(this Transform transform, Vector2 moveVector)
    {
        transform.Translate(moveVector.x, moveVector.y, 0f);
    }


    // GameObject���� ������Ʈ�� �������� �Լ�
    public static T GetComponentMust<T>(this GameObject obj)
    {
        T component = obj.GetComponent<T>();

        GFunc.Assert(component.IsValid<T>() != false, string.Format("{0}���� {1}��(��) ã�� �� �����ϴ�.", obj.name, component.GetType().Name));

        return component;
    }


    // GameObject�� �ڽĿ�����Ʈ���� ������Ʈ�� ã�ƿ��� �����ε� �Լ�
    public static T GetComponentMust<T>(this GameObject obj, string objName)
    {
        T component = obj.FindChildObj(objName).GetComponent<T>();

        GFunc.Assert(component.IsValid<T>() != false, string.Format("{0}���� {1}��(��) ã�� �� �����ϴ�.", obj.name, component.GetType().Name));

        return component;
    }


    // GameObject���� RectTransform ������Ʈ�� �������� �Լ�
    public static RectTransform GetRect(this GameObject obj)
    {
        return obj.GetComponent<RectTransform>();
    }


    // GameObject�� ��Ŀ �������� �����ϴ� �Լ�
    public static void AddAnchoredPos(this GameObject obj, Vector2 position2D)
    {
        obj.GetRect().anchoredPosition += position2D;
    }


    // ���ο� GameObject�� �����ϰ� ������Ʈ�� �߰��Ͽ� �����ϴ� �Լ�
    public static T CreateObj<T>(string objName) where T : Component
    {
        GameObject newObj = new GameObject(objName);
        return newObj.AddComponent<T>();
    }

}