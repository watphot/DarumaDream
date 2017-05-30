/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 02/05/2017
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers:  Steven Mejía Sánchez
/// Description: Herramienta que permite canviar el path de animación de un objeto hijo a otro objeto hijo de un GameObject, AnimationClip o AnimatorController.
///

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum TypeGameObject { None, GameObject, AnimationClip, RuntimeAnimatorController};

/// <summary>
/// Herramienta que permite canviar el path de animación de un objeto hijo a otro objeto hijo de un GameObject, AnimationClip o AnimatorController.
/// </summary>
public class AnimationHierarchyEditor : EditorWindow
{

    #region Public Variables

    #endregion


    #region Private Variables

    private TypeGameObject _actualSelectionGameObject;

    private int index;

    private ArrayList _pathsKeys;

    private Animator _animatorObject;

    private Animator _animatorObject2;

    private AnimationClip _animationClips2;

    private Hashtable _paths;

    private GameObject _null;

    private Vector2 _scrollPos = Vector2.zero;

    private RuntimeAnimatorController _myruntime;

    private Dictionary<string, string> _tempPathOverrides;

    private List<AnimationClip> _animationClips;

    private List<AnimationClip> _myanimationClips;

    private List<string> clipNames = new List<string>();

    private string _sOriginalRoot = "Root";

    private string _sNewRoot = "SomeNewObject";

    string _sReplacementOldRoot;

    string _sReplacementNewRoot;

    [MenuItem("Window/Animation Hierarchy Editor")]

    #endregion


    #region Main Methods

    static void ShowWindow()
    {

        EditorWindow window = EditorWindow.GetWindow(typeof(AnimationHierarchyEditor), false, "Animation Hierarchy Editor", true);

        window.minSize = new Vector2(600, 250);

    }

    public AnimationHierarchyEditor()
    {

        _actualSelectionGameObject = new TypeGameObject();

        _actualSelectionGameObject = TypeGameObject.None;

        _animationClips = new List<AnimationClip>();

        _myanimationClips = new List<AnimationClip>();

        _tempPathOverrides = new Dictionary<string, string>();

    }

    void OnSelectionChange()
    {

        if (Selection.activeObject is AnimationClip)
        {

            _actualSelectionGameObject = TypeGameObject.AnimationClip;

        }

        else if (Selection.activeGameObject is GameObject)
        {

            _actualSelectionGameObject = TypeGameObject.GameObject;

        }

        else if (Selection.activeObject is RuntimeAnimatorController)
        {

            _actualSelectionGameObject = TypeGameObject.RuntimeAnimatorController;

        }

        else if (Selection.objects.Length == 0)
        {

            _actualSelectionGameObject = TypeGameObject.None;

        }

        switch (_actualSelectionGameObject)
        {

            case TypeGameObject.AnimationClip:

                _animationClips.Clear();

                _myanimationClips.Clear();

                clipNames.Clear();

                _animatorObject = null;

                _myruntime = null;

                _animatorObject2 = null;

                index = 0;

                _animationClips.Add((AnimationClip)Selection.activeObject);

                FillModel();

                break;

            case TypeGameObject.GameObject:

                if (Selection.activeGameObject.GetComponent<Animator>() == null)
                {

                    _actualSelectionGameObject = TypeGameObject.None;

                }

                else if (Selection.activeGameObject.GetComponent<Animator>() != null)
                {

                    _animationClips.Clear();

                    _myanimationClips.Clear();

                    clipNames.Clear();

                    _animatorObject = null;

                    _myruntime = null;

                    _animatorObject2 = null;

                    index = 0;

                    _animatorObject2 = Selection.activeGameObject.GetComponent<Animator>();

                    _animatorObject = _animatorObject2;

                    _myruntime = _animatorObject2.runtimeAnimatorController;

                    foreach (AnimationClip i in _myruntime.animationClips)
                    {

                        _myanimationClips.Add(i);

                    }

                    foreach (AnimationClip e in _myanimationClips)
                    {

                        clipNames.Add(e.name);

                    }

                    foreach (AnimationClip o in _myanimationClips)
                    {

                        _animationClips.Add(o);

                    }

                    FillModel();

                }

                break;

            case TypeGameObject.RuntimeAnimatorController:

                _animationClips.Clear();

                _myanimationClips.Clear();

                clipNames.Clear();

                _animatorObject = null;

                _myruntime = null;

                _animatorObject2 = null;

                index = 0;

                foreach (RuntimeAnimatorController o in Selection.objects)
                {

                    _myruntime = o;

                }

                foreach (AnimationClip i in _myruntime.animationClips)
                {

                    _myanimationClips.Add(i);

                }

                foreach (AnimationClip e in _myanimationClips)
                {

                    clipNames.Add(e.name);

                }

                foreach (AnimationClip o in _myanimationClips)
                {

                    _animationClips.Add(o);

                }

                FillModel();

                break;

            case TypeGameObject.None:

                _animationClips.Clear();

                _myanimationClips.Clear();

                clipNames.Clear();

                _animatorObject = null;

                _myruntime = null;

                _animatorObject2 = null;

                index = 0;

                break;

        }

        this.Repaint();

    }

    void OnGUI()
    {

        switch (_actualSelectionGameObject)
        {

            case TypeGameObject.AnimationClip:

                EditorGUILayout.Space();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Selected Animator", GUILayout.Width(170));

                GUI.enabled = false;

                _animatorObject = ((Animator)EditorGUILayout.ObjectField(_animatorObject, typeof(Animator), true, GUILayout.Width(168)));

                EditorGUILayout.EndHorizontal();

                GUI.enabled = true;

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Selected Animator Controller", GUILayout.Width(170));

                GUI.enabled = false;

                _myruntime = ((RuntimeAnimatorController)EditorGUILayout.ObjectField(_myruntime, typeof(RuntimeAnimatorController), true, GUILayout.Width(168)));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();

                GUI.enabled = true;

                GUILayout.Label("Selected Animation Clip", GUILayout.Width(170));

                _animationClips[0] = ((AnimationClip)EditorGUILayout.ObjectField(_animationClips[0], typeof(AnimationClip), true, GUILayout.Width(168)));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.Space();

                GUILayout.BeginHorizontal("Toolbar");

                GUILayout.Label("Path");

                GUILayout.Space(-50);

                GUILayout.Label("Object");

                GUILayout.EndHorizontal();

                EditorGUILayout.Space();

                if (_paths != null)
                {

                    foreach (string path in _pathsKeys)
                    {

                        GUICreatePathItem(path);

                    }

                }

                FillModel();

                break;

            case TypeGameObject.GameObject:

                EditorGUILayout.Space();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Selected Animator", GUILayout.Width(170));

                GUI.enabled = false;

                _animatorObject = ((Animator)EditorGUILayout.ObjectField(_animatorObject, typeof(Animator), true, GUILayout.Width(168)));

                EditorGUILayout.EndHorizontal();

                GUI.enabled = true;

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Selected Animator Controller", GUILayout.Width(170));

                GUI.enabled = false;

                _myruntime = ((RuntimeAnimatorController)EditorGUILayout.ObjectField(_myruntime, typeof(RuntimeAnimatorController), true, GUILayout.Width(168)));

                EditorGUILayout.EndHorizontal();

                GUI.enabled = true;

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Selected Animation Clip", GUILayout.Width(170));

                Rect positionRect = new Rect(new Vector2(177, 49), new Vector2(168, 10));

                index = EditorGUI.Popup(positionRect, index, clipNames.ToArray());

                _animationClips[0] = _myanimationClips[index];

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.Space();

                GUILayout.BeginHorizontal("Toolbar");

                GUILayout.Label("Path");

                GUILayout.Space(-50);

                GUILayout.Label("Object");

                GUILayout.EndHorizontal();

                EditorGUILayout.Space();

                if (_paths != null)
                {

                    foreach (string path in _pathsKeys)
                    {

                        GUICreatePathItem(path);

                    }

                }

                FillModel();

                break;

            case TypeGameObject.RuntimeAnimatorController:

                EditorGUILayout.Space();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Selected Animator", GUILayout.Width(170));

                GUI.enabled = false;

                _animatorObject = ((Animator)EditorGUILayout.ObjectField(_animatorObject, typeof(Animator), true, GUILayout.Width(168)));

                EditorGUILayout.EndHorizontal();

                GUI.enabled = true;

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Selected Animator Controller", GUILayout.Width(170));

                GUI.enabled = false;

                _myruntime = ((RuntimeAnimatorController)EditorGUILayout.ObjectField(_myruntime, typeof(RuntimeAnimatorController), true, GUILayout.Width(168)));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();

                GUI.enabled = true;

                GUILayout.Label("Selected Animation Clip", GUILayout.Width(170));

                Rect positionRect2 = new Rect(new Vector2(177, 49), new Vector2(168, 10));

                index = EditorGUI.Popup(positionRect2, index, clipNames.ToArray());

                _animationClips[0] = _myanimationClips[index];

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.Space();

                GUILayout.BeginHorizontal("Toolbar");

                GUILayout.Label("Path");

                GUILayout.Space(-50);

                GUILayout.Label("Object");

                GUILayout.EndHorizontal();

                EditorGUILayout.Space();

                if (_paths != null)
                {

                    foreach (string path in _pathsKeys)
                    {

                        GUICreatePathItem(path);

                    }

                }

                FillModel();

                break;

            case TypeGameObject.None:

                EditorGUILayout.Space();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Selected Animator", GUILayout.Width(170));

                GUI.enabled = false;

                _animatorObject = ((Animator)EditorGUILayout.ObjectField(_animatorObject, typeof(Animator), true, GUILayout.Width(168)));

                EditorGUILayout.EndHorizontal();

                GUI.enabled = true;

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Selected Animator Controller", GUILayout.Width(170));

                GUI.enabled = false;

                _myruntime = ((RuntimeAnimatorController)EditorGUILayout.ObjectField(_myruntime, typeof(RuntimeAnimatorController), true, GUILayout.Width(168)));

                EditorGUILayout.EndHorizontal();

                GUI.enabled = true;

                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Selected Animation Clip", GUILayout.Width(170));

                GUI.enabled = false;

                _animationClips2 = ((AnimationClip)EditorGUILayout.ObjectField(_animationClips2, typeof(AnimationClip), true, GUILayout.Width(168)));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.Space();

                GUI.enabled = true;

                GUILayout.BeginHorizontal("Toolbar");

                GUILayout.Label("Path");

                GUILayout.Space(-50);

                GUILayout.Label("Object");

                GUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();

                GUI.enabled = false;

                string pathOverride = _sNewRoot;

                _sNewRoot = EditorGUILayout.TextField(pathOverride);

                GameObject obj = null;

                GameObject newObj;

                newObj = (GameObject)EditorGUILayout.ObjectField(obj, typeof(GameObject), true);

                GUILayout.Button("Change", GUILayout.Width(60));

                GUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();

                GUI.enabled = true;

                GUILayout.FlexibleSpace();

                EditorGUILayout.HelpBox("Please select a GameObject with Animator, Animation Clip or an Animator Controller", MessageType.Warning);

                GUILayout.FlexibleSpace();

                GUILayout.EndHorizontal();

                break;

        }

    }

    void GUICreatePathItem(string path)
    {

        string newPath = path;

        GameObject obj = FindObjectInRoot(path);

        GameObject newObj;

        ArrayList properties = (ArrayList)_paths[path];

        string pathOverride = path;

        if (_tempPathOverrides.ContainsKey(path))
        {

            pathOverride = _tempPathOverrides[path];

        }

        EditorGUILayout.BeginHorizontal();

        pathOverride = EditorGUILayout.TextField(pathOverride);

        if (pathOverride != path) _tempPathOverrides[path] = pathOverride;

        Color standardColor = GUI.color;

        if (obj != null)
        {

            GUI.color = Color.white;

        }

        if (obj == null)
        {

            GUI.color = Color.yellow;

            if (_actualSelectionGameObject == TypeGameObject.AnimationClip)
            {
                GUI.enabled = false;
                GUI.color = Color.white;

            }

            else if (_actualSelectionGameObject == TypeGameObject.RuntimeAnimatorController)
            {
                GUI.enabled = false;
                GUI.color = Color.white;

            }

        }

        newObj = (GameObject)EditorGUILayout.ObjectField(obj, typeof(GameObject), true);

        GUI.color = standardColor;

        GUI.enabled = true;

        if (GUILayout.Button("Change", GUILayout.Width(60)))
        {

            newPath = pathOverride;

            _tempPathOverrides.Remove(path);

        }

        EditorGUILayout.EndHorizontal();

        try
        {

            if (obj != newObj)
            {

                UpdatePath(path, ChildPath(newObj));

            }

            if (newPath != path)
            {

                UpdatePath(path, newPath);

            }

        }

        catch (UnityException ex)
        {

            Debug.LogError(ex.Message);

        }

    }

    void OnInspectorUpdate()
    {

        this.Repaint();

    }

    void FillModel()
    {

        _paths = new Hashtable();

        _pathsKeys = new ArrayList();

        FillModelWithCurves(AnimationUtility.GetCurveBindings(_animationClips[0]));

        FillModelWithCurves(AnimationUtility.GetObjectReferenceCurveBindings(_animationClips[0]));

    }

    private void FillModelWithCurves(EditorCurveBinding[] curves)
    {

        foreach (EditorCurveBinding curveData in curves)
        {

            string key = curveData.path;

            if (_paths.ContainsKey(key))
            {

                ((ArrayList)_paths[key]).Add(curveData);

            }

            else
            {

                ArrayList newProperties = new ArrayList();

                newProperties.Add(curveData);

                _paths.Add(key, newProperties);

                _pathsKeys.Add(key);

            }

        }

    }

    void UpdatePath(string oldPath, string newPath)
    {

        if (_paths[newPath] != null)
        {

            Debug.Log("Path alredy exist!");

        }

        AssetDatabase.StartAssetEditing();

        AnimationClip animationClip = _animationClips[0];

        Undo.RecordObject(animationClip, "Animation Hierarchy Change");

        for (int iCurrentPath = 0; iCurrentPath < _pathsKeys.Count; iCurrentPath++)
        {

            string path = _pathsKeys[iCurrentPath] as string;

            ArrayList curves = (ArrayList)_paths[path];

            for (int i = 0; i < curves.Count; i++)
            {

                EditorCurveBinding binding = (EditorCurveBinding)curves[i];

                AnimationCurve curve = AnimationUtility.GetEditorCurve(animationClip, binding);

                ObjectReferenceKeyframe[] objectReferenceCurve = AnimationUtility.GetObjectReferenceCurve(animationClip, binding);

                if (curve != null) AnimationUtility.SetEditorCurve(animationClip, binding, null);

                else AnimationUtility.SetObjectReferenceCurve(animationClip, binding, null);

                if (path == oldPath) binding.path = newPath;

                if (curve != null) AnimationUtility.SetEditorCurve(animationClip, binding, curve);

                else AnimationUtility.SetObjectReferenceCurve(animationClip, binding, objectReferenceCurve);

                float fChunk = 1f / _animationClips.Count;

                float fProgress = (animationClip.length * fChunk) + fChunk * ((float)iCurrentPath / (float)_pathsKeys.Count);

                EditorUtility.DisplayProgressBar("Animation Hierarchy Progress", "How far along the animation editing has progressed.", fProgress);

            }

        }

        AssetDatabase.StopAssetEditing();

        EditorUtility.ClearProgressBar();

        FillModel();

        this.Repaint();

    }

    GameObject FindObjectInRoot(string path)
    {

        if (_animatorObject == null)
        {

            return null;

        }

        Transform child = _animatorObject.transform.Find(path);

        if (child != null)
        {

            return child.gameObject;

        }

        else
        {

            return null;

        }
    }

    string ChildPath(GameObject obj, bool sep = false)
    {

        if (_animatorObject == null)
        {

            throw new UnityException("Please assign Referenced Animator (Root) first!");

        }

        if (obj == _animatorObject.gameObject)
        {

            return "";

        }

        else
        {

            if (obj.transform.parent == null)
            {

                throw new UnityException("Object must belong to " + _animatorObject.ToString() + "!");

            }

            else
            {

                return ChildPath(obj.transform.parent.gameObject, true) + obj.name + (sep ? "/" : "");

            }

        }

    }

    #endregion


    #region Utility Methods

    #endregion

}

