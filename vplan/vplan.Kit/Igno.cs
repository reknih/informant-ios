using System;

namespace vplan
{
	public class Igno
	{
		public Igno(string _Fach, string _Lehrer) {
			Fach = _Fach;
			Lehrer = _Lehrer;
		}
		public Igno(string input) {
			Fach = input.Substring(0, input.IndexOf("%"));
			Lehrer = input.Substring(input.IndexOf("%") + 1);
		}
		public string Fach { get; set; }

		public string Lehrer { get; set; }	
	}
}

