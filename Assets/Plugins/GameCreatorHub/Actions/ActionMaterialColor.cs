namespace GameCreator.Core
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
    using GameCreator.Variables;

	[AddComponentMenu("")]
	public class ActionMaterialColor : IAction
	{
        public Material material;

        [Space]
        public string property = "_Color";
        public ColorProperty color = new ColorProperty(Color.white);

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            this.material.SetColor(this.property, this.color.GetValue());
            return true;
        }

		#if UNITY_EDITOR
        public static new string NAME = "Material/Material Color";
		#endif
	}
}
