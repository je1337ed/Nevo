using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Ants
{
	public class NeuralNetwork
	{
		private ArrayList mLayers;
		private NeuronLayer mInputLayer;

		private NeuronLayer mOutputLayer;
		//Creates a neural network.
		//Inputs: The number of neurons in the input layer
		//Outputs: The number of neurons in the output layer
		//HiddenLayers:  The number of Hidden layers in the network.  This can be 0 or greater.
		//NeuronsPerHiddenLayer:  The number of neurons in each hidden layer.
		//InputWeightSize:  The number of weights in the input layer.  The weights size for each layer above is the number of neurons in the previous layer.
		//ActivationFunction:  The activation function to use in the neuron.
		public NeuralNetwork(int Inputs, int Outputs, int HiddenLayers, int NeuronsPerHiddenLayer, int InputWeightSize, Neuron.ActivationFunction Activation)
		{
			mInputLayer = new NeuronLayer(Inputs, InputWeightSize + 1, Activation);
			mLayers = new ArrayList();
			int idx = 0;
			int LastLayerSize = Inputs;
			for (idx = 0; idx <= HiddenLayers - 1; idx++) {
				mLayers.Add(new NeuronLayer(NeuronsPerHiddenLayer, LastLayerSize + 1, Activation));
				LastLayerSize = NeuronsPerHiddenLayer;
			}
			mOutputLayer = new NeuronLayer(Outputs, LastLayerSize + 1, Activation);
		}

		//Returns the total number of weights in the neural network by adding up the number that comes back from each layer.
		public int WeightCount {
			get {
				int cnt = mInputLayer.NumWeights;
				NeuronLayer nl = null;
				foreach (NeuronLayer nl_loopVariable in mLayers) {
					nl = nl_loopVariable;
					cnt += nl.NumWeights;
				}
				cnt += mOutputLayer.NumWeights;
				return cnt;
			}
		}

		//Gets the output of the neural network based on the passed in inputs.
		public float[] Outputs(float[] Inputs) {
			
				float[] LastOutputs = null;
				NeuronLayer nl = null;
				//Get the output of the input layer and then pass that on to the next hidden layer.  The output from
				//the previous layer is then used as the input to the next layer until we reach the output layer.
				LastOutputs = mInputLayer.Outputs(Inputs);
				foreach (NeuronLayer nl_loopVariable in mLayers) {
					nl = nl_loopVariable;
					LastOutputs = nl.Outputs(LastOutputs);
				}
				LastOutputs = mOutputLayer.Outputs(LastOutputs);
				return LastOutputs;			
		}

		//Accessor method to get and assign weights to the whole neural network.  The weights also include the threshold values
		//so that it is easier to evolve the network with a GA.
		public float[] Weights {
			get {
				float[] retWeights = null;
				retWeights = new float[this.WeightCount];
				int idx = 0;
				int retidx = 0;
				float[] layerWeights = null;
				NeuronLayer nl = null;
				//Debug.Write("Input layer weights ")
				layerWeights = mInputLayer.Weights;
				//Debug.Write(vbCrLf)
				for (idx = 0; idx <= layerWeights.Length - 1; idx++) {
					retWeights[retidx] = layerWeights[idx];
					retidx += 1;
				}
				foreach (NeuronLayer nl_loopVariable in mLayers) {
					nl = nl_loopVariable;
					//Debug.Write("Inner layer weights ")
					layerWeights = nl.Weights;
					//Debug.Write(vbCrLf)
					for (idx = 0; idx <= layerWeights.Length - 1; idx++) {
						retWeights[retidx] = layerWeights[idx];
						retidx += 1;
					}
				}
				//Debug.Write("Output layer weights ")
				layerWeights = mOutputLayer.Weights;
				//Debug.Write(vbCrLf)
				for (idx = 0; idx <= layerWeights.Length - 1; idx++) {
					retWeights[retidx] = layerWeights[idx];
					retidx += 1;
				}
				return retWeights;
			}
			set {
				int vIdx = 0;
				NeuronLayer nl = null;
				int layerIdx = 0;
				float[] layerWeights = null;

				for (vIdx = 0; vIdx <= value.Length - 1; vIdx++) {
					layerWeights = new float[mInputLayer.NumWeights];
					for (layerIdx = 0; layerIdx <= layerWeights.Length - 1; layerIdx++) {
						layerWeights[layerIdx] = value[vIdx];
						vIdx += 1;
					}
					mInputLayer.Weights = layerWeights;
					foreach (NeuronLayer nl_loopVariable in mLayers) {
						nl = nl_loopVariable;
						layerWeights = new float[nl.NumWeights];
						for (layerIdx = 0; layerIdx <= layerWeights.Length - 1; layerIdx++) {
							layerWeights[layerIdx] = value[vIdx];
							vIdx += 1;
						}
						nl.Weights = layerWeights;
					}
					layerWeights = new float[mOutputLayer.NumWeights];
					for (layerIdx = 0; layerIdx <= layerWeights.Length - 1; layerIdx++) {
						layerWeights[layerIdx] = value[vIdx];
						vIdx += 1;
					}
					mOutputLayer.Weights = layerWeights;
				}
			}
		}
	}
}
