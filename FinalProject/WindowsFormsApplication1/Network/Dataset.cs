﻿namespace WindowsFormsApplication1.Network
{
	public class DataSet
	{
		public double[] Values { get; set; }
		public double[] Targets { get; set; }

		public DataSet(double[] values, double[] targets)
		{
			Values = values;
			Targets = targets;
		}
	}
}