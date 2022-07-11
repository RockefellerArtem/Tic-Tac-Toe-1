using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
	public static class ActionHandlers
	{
		public static Dictionary<string, Dictionary<string, MethodInfo>> GetActions()
		{
			var Dictionary = new Dictionary<string, Dictionary<string, MethodInfo>>();
			Dictionary<string, IActionCommand> handlers = new Dictionary<string, IActionCommand>();
			IEnumerable<Type> typesWithMyAttribute =
			from type in Assembly.GetExecutingAssembly().GetTypes()
			where type.IsDefined(typeof(FuncCommand), false)
			select type;


			foreach (var typeFunc in typesWithMyAttribute)
			{
				Type type = typeFunc.GetInterface("IActionCommand");

				if (type == null) break;

				var methodsTemp = typeFunc.GetMethods();
				var invokMethods = new Dictionary<string, MethodInfo>();
				foreach (var method in methodsTemp)
				{
					var attribute = method.GetCustomAttribute<InvokAttribute>();
					if (attribute != null)
					{
						invokMethods.Add(attribute.NameMethod, method);
					}
				}
				string action = typeFunc.GetCustomAttribute<FuncCommand>().InvokMethod;
				var handler = (IActionCommand)Activator.CreateInstance(typeFunc);
				Dictionary.Add(action, invokMethods);
			}
			
			return Dictionary;
		}

		public static void ExecuteCommand(string type, string method)
		{
			var handlers = GetActions();
			if (handlers.ContainsKey(type))
			{
				var classTemp = handlers[type];
				if (classTemp.ContainsKey(method))
				{
					var MethodInvok = classTemp[method];
					var declaringType = Activator.CreateInstance(MethodInvok.DeclaringType);
					MethodInvok.Invoke(declaringType, null);
				}
			}
		}
	}
