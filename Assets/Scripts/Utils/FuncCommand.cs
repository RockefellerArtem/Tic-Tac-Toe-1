using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


	[AttributeUsage(AttributeTargets.Class)]
	public class FuncCommand : Attribute
	{
		public string InvokMethod{ get; set; }

		public FuncCommand(string action)
		{
			InvokMethod = action;
		}

	}

