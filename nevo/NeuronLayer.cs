using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Ants
{
	//Neuron Layer organizes the neurons into distinct layers.  It aggregates the weights and output properties of the neurons.
	public class NeuronLayer : System.Collections.CollectionBase
	{
		//The weights per neuron is cached to make some calculations faster like the total number of weights in all the neurons of this layer.

		private int mWeightsPerNeuron;
		//Constructs a layer of neurons and initializes them with the number of weights and the activation function to be uses.
		public NeuronLayer(int Neurons, int WeightsPerNeuron, Neuron.ActivationFunction ActivationFunction)
		{
			int idx = 0;
			Neuron n = null;
			for (idx = 0; idx <= Neurons - 1; idx++) {
				n = new Neuron(WeightsPerNeuron, ActivationFunction);
				List.Add(n);
			}
			mWeightsPerNeuron = WeightsPerNeuron;
		}

		public int NumWeights {
			get { return mWeightsPerNeuron * List.Count; }
		}

		public int WeightsPerNeuron {
			get { return mWeightsPerNeuron; }
		}

		//All the weights for this layer of neurons
		public float[] Weights {
			get {
				//We'll make an array big enough to hold all of the weights in the layer and then loop through each 
				//neurons weights copying them into our ret value.
				float[] ret = null;
				ret = new float[(mWeightsPerNeuron * List.Count)];
				int idx = 0;
				Neuron n = null;
				foreach (Neuron n_loopVariable in List) {
					n = n_loopVariable;
					float[] tmpw = null;
					tmpw = n.Weights;
					int idx2 = 0;
					//Debug.Write("neuron" & list.IndexOf(n) & ": ")
					for (idx2 = 0; idx2 <= tmpw.Length - 1; idx2++) {
						ret[idx] = tmpw[idx2];
						//Debug.Write(tmpw(idx2).ToString & ",")
						idx += 1;
					}
					//Debug.Write("|")
				}
				return ret;
			}
			set {
				//Just the opposite of the get.  This distributes an array of weights to the neurons.
				int idx = 0;
				Neuron n = null;
				//Loops through each neuron
				foreach (Neuron n_loopVariable in List) {
					n = n_loopVariable;
					float[] tmpw = null;
					tmpw = new float[mWeightsPerNeuron];
					int idx2 = 0;
					for (idx2 = 0; idx2 <= tmpw.Length - 1; idx2++) {
						tmpw[idx2] = value[idx];
						idx += 1;
					}
					n.Weights = tmpw;
				}
			}
		}

		//Aggregates the outputs of each neuron into one big return array.  The inputs
		//array is sent to each neuron and the output is stored in an element of the array.
		public float[] Outputs(float[] Inputs) {
			
				int outidx = 0;
				Neuron n = null;
				float[] tmpout = null;
				tmpout = new float[List.Count];
				foreach (Neuron n_loopVariable in List) {
					n = n_loopVariable;
					float[] tmpins = null;
					tmpout[outidx] = n.Output(Inputs);
					outidx += 1;
				}
				return tmpout;
			
		}
	}
}
