﻿namespace WindowsFormsApplication1.Network
{
	public class Synapse
	{
		public Neuron InputNeuron { get; set; }
		public Neuron OutputNeuron { get; set; }
		public double Weight { get; set; }
		public double WeightDelta { get; set; }

		public Synapse(Neuron inputNeuron, Neuron outputNeuron)
		{
			InputNeuron = inputNeuron;
			OutputNeuron = outputNeuron;
			Weight = Network.GetRandom();
		}
	}
}