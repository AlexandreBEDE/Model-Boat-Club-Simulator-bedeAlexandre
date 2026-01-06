using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(BoatAutoPilot))] //Précision de quel éditeur on veut modifier
public class EditorBoat : Editor
{
    private Editor SO_BoatManager = null; //Variable pour stocker l'éditeur du scriptable object
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); //Affiche l'inspecteur par défaut
        SerializedProperty serializedData = serializedObject.FindProperty("BoatPara"); //Récupère la propriété sérialisée du scriptable object

        Object data = serializedData.objectReferenceValue; //Récupère la valeur de la propriété sérialisée (le scriptable object lui-même)

        Editor.CreateCachedEditor(data, null, ref SO_BoatManager); //Crée un éditeur pour le scriptable object et le stocke dans la variable SO_BoatManager
        SO_BoatManager.OnInspectorGUI(); //Affiche l'inspecteur du scriptable object

    }
}
