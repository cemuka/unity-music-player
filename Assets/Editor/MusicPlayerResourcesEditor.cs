using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(MusicPlayerResources))]
public class MusicPlayerResourcesEditor : Editor
{
    private KeyCode _nextKeyShortcut;

    private SerializedProperty _nextKey;
    private SerializedProperty _previousKey;
    private SerializedProperty _trackList;
    private ReorderableList _list;

    private void OnEnable()
    {
        _nextKey = serializedObject.FindProperty("nextKey");
        _previousKey = serializedObject.FindProperty("previousKey");
        _trackList = serializedObject.FindProperty("trackList");
        _list = new ReorderableList(serializedObject, _trackList, true, true, true, true)
        {
            drawHeaderCallback = DrawListHeader,
            drawElementCallback = DrawListElement,
            elementHeightCallback = ElementHeightCallback,
            onAddCallback = OnAddCallback
        };
    }

    private void DrawListHeader(Rect rect)
    {
        GUI.Label(rect, "Track List");
    }

    private void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        //Get the element we want to draw from the list.
        SerializedProperty element = _list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;

        //We get the name property of our element so we can display this in our list.
        SerializedProperty elementName = element.FindPropertyRelative("name");
        string elementTitle = string.IsNullOrEmpty(elementName.stringValue) ? "New Track" : $"Track: {elementName.stringValue}";

        //Draw the list item as a property field, just like Unity does internally.
        EditorGUI.PropertyField(position:
        new Rect(rect.x += 10, rect.y, rect.width * .8f, EditorGUIUtility.singleLineHeight), property:
        element, new GUIContent(elementTitle), true);

    }

    private float ElementHeightCallback(int index)
    {
        //Gets the height of the element. This also accounts for properties that can be expanded, like structs.
        float propertyHeight = EditorGUI.GetPropertyHeight(_list.serializedProperty.GetArrayElementAtIndex(index), true);

        float spacing = EditorGUIUtility.singleLineHeight /2;

        return propertyHeight + spacing;
    }

    private void OnAddCallback(ReorderableList list)
    {
        //Insert an extra item add the end of our list.
        var index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;

        //If we want to do anything with the item we just added,
        //We can create reference by using this method
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_nextKey, new GUIContent("Next"));
        EditorGUILayout.PropertyField(_previousKey, new GUIContent("Previous"));
        EditorGUILayout.Space(80);
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}