using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsFormsApplication1.Network;

namespace WindowsFormsApplication1
{
	internal class NN
	{
		//Stałe i Zmienne
		private const int MaxEpochs = 5000;
		private const double MinimumError = 0.01;
		private const TrainingType TrainingType = Network.TrainingType.MinimumError;

		private static int _numInputParameters;
		private static int _numHiddenLayerNeurons;
		private static int _numOutputParameters;
		private static Network.Network _network;
		private static List<DataSet> _dataSets; 

        //Funkcja główna
		private static void Run()
		{
			SetupNetwork();
			TrainNetwork();
			VerifyTraining();
		}

        //Trenowanie Sieci
		private static void TrainNetwork()
		{
			Train();
		}

        //Sprawdzenie Trenowania
		private static void VerifyTraining()
		{
			while (true)
			{
				PrintUnderline(50);
				var values = GetInputData(string.Format("Type {0} inputs: ", _numInputParameters));
				var results = _network.Compute(values);
				PrintNewLine();

				foreach (var result in results)
				{
					Console.WriteLine("Output: {0}", result);
				}

				PrintNewLine();

				var convertedResults = new double[results.Length];
				for (var i = 0; i < results.Length; i++) { convertedResults[i] = results[i] > 0.5 ? 1 : 0; }

					Train();
			}
		}

		private static void Train()
		{
			_network.Train(_dataSets, TrainingType == TrainingType.Epoch ? MaxEpochs : MinimumError);
		}

		private static void SetupNetwork()
		{
			SetNumInputParameters();
			SetNumNeuronsInHiddenLayer();
			SetNumOutputParameters();
			GetTrainingData();

			_network = new Network.Network(_numInputParameters, _numHiddenLayerNeurons, _numOutputParameters);
		}

		private static void SetNumInputParameters()
		{
			_numInputParameters = 2;
		}

		private static void SetNumNeuronsInHiddenLayer()
		{
            _numHiddenLayerNeurons = 2;
		}

		private static void SetNumOutputParameters()
		{
            _numOutputParameters = 1;
		}
        public static string Verdict(bool a, bool b)
        {
            if (a == true && b == true)
            {
                return "Przedmiot został podniesiony \r\n";
            }
            else {
                return "Przedmiot nie został podniesiony \r\n";
                    }
        }

		private static void GetTrainingData()
		{
			
			_dataSets = new List<DataSet>();
			for (var i = 0; i < 4; i++)
			{
				var values = GetInputData(String.Format("Data Set {0}", i + 1));
				var expectedResult = GetExpectedResult(String.Format(""));
				_dataSets.Add(new DataSet(values, expectedResult));
			}
		}

		private static double[] GetInputData(string message)
		{
			var line = message;

			while (line == null || line.Split(' ').Count() != _numInputParameters)
			{
				Console.WriteLine("{0}", _numInputParameters);
				PrintNewLine();
				line = message;
			}

			var values = new double[_numInputParameters];
			var lineNums = line.Split(' ');
			for(var i = 0; i < lineNums.Length; i++)
			{
				double num;
				if (Double.TryParse(lineNums[i], out num))
				{
					values[i] = num;
				}
				else
				{
					return GetInputData(message);
				}
			}

			return values;
		}

		private static double[] GetExpectedResult(string message)
		{
			var line = message;

			var values = new double[_numOutputParameters];
			var lineNums = line.Split(' ');
			for (var i = 0; i < lineNums.Length; i++)
			{
				int num;
				if (int.TryParse(lineNums[i], out num) && (num == 0 || num == 1))
				{
					values[i] = num;
				}
				else
				{
					Console.WriteLine("You must enter 1s and 0s!");
					PrintNewLine(2);
					return GetExpectedResult(message);
				}
			}

			return values;
		}

		private static int GetInput(string message, int min)
		{
			var num = GetNumber(message);

			while (num < min)
			{
				num = GetNumber(message);
			}
			return num;
		}

		private static int GetNumber(string message)
		{
			int num;
			var line = message;
			return line != null && int.TryParse(line, out num) ? num : 0;
		}


		private static void PrintNewLine(int numNewLines = 1)
		{
			for (var i = 0; i < numNewLines; i++)
				Console.WriteLine();
		}

		private static void PrintUnderline(int numUnderlines)
		{
			for (var i = 0; i < numUnderlines; i++)
				Console.Write('-');
			PrintNewLine(2);
		}

		private static void WriteError(string error)
		{
			Console.WriteLine(error);
			Console.ReadLine();
			Environment.Exit(0);
		}
	}
}