using UnityEditor;
using UnityEditor.UI;

namespace UIExtensions.Editor
{
    [CustomEditor(typeof(SkewedImage))]
    public class SkewedImageInspector : ImageEditor
    {
        private SkewedImage _skewedImage;
        private SerializedProperty _skewVector;

        protected override void OnEnable()
        {
            base.OnEnable();
            _skewedImage = (SkewedImage)target;
            _skewVector = serializedObject.FindProperty("SkewVector");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            EditorGUILayout.PropertyField(_skewVector);

            if (_skewVector.vector2Value != _skewedImage.SkewVector)
            {
                Undo.RecordObject(_skewedImage, "Changed Skew");
                _skewedImage.SkewVector = _skewVector.vector2Value;
                _skewedImage.OnRebuildRequested();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
