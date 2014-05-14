using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Ants
{
	public class Neuron
	{
		//Enumerator for the Activation Function.  Currently only Step and Sigmoid are implemented
		public enum ActivationFunction
		{
			AFStep = 0,
			AFSigmoid = 1
		}

		//Some variables to hold our property values and such.
		private float mOutput;

		private float[] mWeights;

		private ActivationFunction mActivation;
		//Constructor for the neuron.  It will setup an array for the weights and tell us what activation function to use.
		public Neuron(int NumWeights, ActivationFunction activation)
		{
			mWeights = new float[NumWeights];
			// -1 b/c VB uses this number as the upper bound and not the number of elements. :-|
			int idx = 0;
			for (idx = 0; idx <= mWeights.Length - 1; idx++) {
				mWeights[idx] = (float)Ants.Rand.NextDouble();
				//Debug.Write(mWeights(idx).ToString & " ")
			}
			//Debug.WriteLine("")

			this.Activation = activation;
		}

		//Change the activation function
		public ActivationFunction Activation {
			get { return mActivation; }
				// Store the value in a local variable.
			set { mActivation = value; }
		}

		//The input weights and threshold value
		public float[] Weights {
			get { return mWeights; }
			set { mWeights = value; }
		}

		//Just about the most important function in a neuron.  It takes an array of singles and calculates the output.
		//Usage is neuron.Output(inputs())
		public float Output(float[] inputs) {
			
				float total = 0;
				int idx = 0;
				float threshold = 0;

				//inputs should be 1 less than the number of weights since the last weight is the threshold for activation.
				for (idx = 0; idx <= inputs.Length - 1; idx++) {
					total += inputs[idx] * mWeights[idx];
				}
				//idx += 1
				threshold = mWeights[idx];
				if (mActivation == ActivationFunction.AFStep) {
					if (threshold < total) {
						return 1;
					} else {
						return 0;
					}
				} else {
					return (float) (1 / (1 + Math.Exp((-total) / threshold)));
				}
			}
		
	}
}
