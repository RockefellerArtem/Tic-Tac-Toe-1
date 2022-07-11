using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


	[AttributeUsage(AttributeTargets.Method)]
	public class InvokAttribute : Attribute
	{
		public string NameMethod;

		public InvokAttribute(string name)
		{
			NameMethod = name;
		}
	}

