namespace GameCreator.Core
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using GameCreator.Characters;
    using GameCreator.Variables;
	using GameCreator.Camera;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [AddComponentMenu("")]
	public class CustomCrouch : IAction
	{
        public TargetCharacter target = new TargetCharacter(TargetCharacter.Target.Player);
        public NumberProperty runSpeed = new NumberProperty(4.0f);
        public NumberProperty colliderHeight = new NumberProperty(1.8f);

        public bool changeFPMotorOffset = false;
        public CameraMotorTypeFirstPerson FPCameraMotor;
        public Vector3 cameraOffset = new Vector3(0, 1.8f, 0);

        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            Character charTarget = this.target.GetCharacter(target);

            if (charTarget != null) {
                charTarget.characterLocomotion.runSpeed = this.runSpeed.GetValue(target);
                charTarget.characterLocomotion.ChangeHeight(this.colliderHeight.GetValue(target));
			}

			if (FPCameraMotor != null && changeFPMotorOffset)
			{
                FPCameraMotor.positionOffset = this.cameraOffset;
			}
			
            return true;
        }

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public static new string NAME = "Custom/CustomCrouch";
        private const string NODE_TITLE = "{0} Crouch, Run Speed to {1}, Height to {2}";

        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spTarget;
        private SerializedProperty spRunSpeed;
        private SerializedProperty spColliderHeight;
        private SerializedProperty spChangeFPMotorOffset;
        private SerializedProperty spFPCameraMotor;
        private SerializedProperty spCameraOffset;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {
            return string.Format(NODE_TITLE, this.target, this.runSpeed, this.colliderHeight);
        }

        protected override void OnEnableEditorChild()
        {
            this.spTarget = this.serializedObject.FindProperty("target");
            this.spRunSpeed = this.serializedObject.FindProperty("runSpeed");
            this.spColliderHeight = this.serializedObject.FindProperty("colliderHeight");
            this.spChangeFPMotorOffset = this.serializedObject.FindProperty("changeFPMotorOffset");
            this.spFPCameraMotor = this.serializedObject.FindProperty("FPCameraMotor");
            this.spCameraOffset = this.serializedObject.FindProperty("cameraOffset");
        }

        protected override void OnDisableEditorChild()
        {
            this.spTarget = null;
            this.spRunSpeed = null;
            this.spColliderHeight = null;
            this.spChangeFPMotorOffset = null;
            this.spFPCameraMotor = null;
            this.spCameraOffset = null;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

            EditorGUILayout.PropertyField(this.spTarget);
            EditorGUILayout.PropertyField(this.spRunSpeed);
            EditorGUILayout.PropertyField(this.spColliderHeight);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spChangeFPMotorOffset);
            if (this.spChangeFPMotorOffset.boolValue)
            {
                EditorGUILayout.PropertyField(this.spFPCameraMotor);
                EditorGUILayout.PropertyField(this.spCameraOffset);
            }

            this.serializedObject.ApplyModifiedProperties();
        }

#endif
    }
}