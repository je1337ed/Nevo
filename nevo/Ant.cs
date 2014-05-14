using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Ants
{
	//The Ant class is the container for the neural network and main object in the ant farm.
	public class Ant : IComparable
	{
		//Used for sorting the array of ants.

		private NeuralNetwork mBrain;
		//Chromosome stuff
		private float mFitness;
		//Outputs from the NN
		private float mRTrack;

		private float mLTrack;
		private PointF mPosition;
		//Inputs for the NN
		//Index of the closest food
		private int mLastFoundFood;

		private PointF mLookAt;
		private float mRotation;

		private float mSpeed;

		private float mMaxTurnRate = 0.9F;
		//Scale to draw the Ant at.

		public float mScale;

		public Ant(NeuralNetwork nn, float x, float y)
		{
			mBrain = nn;
			mPosition = new PointF(x, y);
			mRTrack = (float)Ants.Rand.NextDouble();
			mLTrack = (float)Ants.Rand.NextDouble();
			//Debug.WriteLine(mLTrack.ToString & " " & mRTrack.ToString)
		}

		//Called for each tick in the program.  The update function gets inputs for the NN and then sets the track values according to the output.
		public object Update(ref PointF[] food)
		{
			float[] inputs = null;
			inputs = new float[4];
			PointF closestfood = GetClosestObject(ref food);
			VectorNormalize(ref closestfood);
			inputs[0] = closestfood.X;
			inputs[1] = closestfood.Y;
			inputs[2] = mLookAt.X;
			inputs[3] = mLookAt.Y;

			float[] output = mBrain.Outputs(inputs);
			mLTrack = output[0];
			mRTrack = output[1];


			float RotForce = 0;
			RotForce = mLTrack - mRTrack;
			Clamp(ref RotForce, -1 * mMaxTurnRate, mMaxTurnRate);
			mRotation += RotForce;
			mSpeed = mLTrack + mRTrack;

			mLookAt.X = (float)Math.Sin(mRotation) * -1;
			mLookAt.Y = (float)Math.Cos(mRotation);
			
			mPosition = AddVectors(ref mPosition, MultVect(ref mLookAt, mSpeed));
			
			return null;
		}

		//Returns a vector to the closest food/object
		public PointF GetClosestObject(ref PointF[] food)
		{
			int ClosestSoFar = 9999;
			PointF ClosestObject = new PointF(0, 0);
			int idx = 0;
			for (idx = 0; idx <= food.Length - 1; idx++) {
				float lenToObject = VectorLength(VectDistance(food[idx], mPosition));
				if (lenToObject < ClosestSoFar) {
					ClosestSoFar = (int)lenToObject; //may need to round - JB
					ClosestObject = VectDistance(mPosition, food[idx]);
					mLastFoundFood = idx;
				}
			}
			return ClosestObject;
		}

		//Checks to see if the Ant has found food and returns the index of the food or -1 if not found.
		public int CheckForFood(ref PointF[] food, float size)
		{
			PointF distance = VectDistance(mPosition, food[mLastFoundFood]);
			if (VectorLength(distance) < size + 5) {
				return mLastFoundFood;
			} else {
				return -1;
			}
		}

		//Called after each epoch.  This just resets the fitness score and randomly places the ant on the board.
		public void Reset(int width, int height)
		{
			//Debug.WriteLine("Reseting ant")
			this.Fitness = 0;
			mPosition = new PointF((float)Ants.Rand.NextDouble() * width, (float)Ants.Rand.NextDouble() * height);
			//Debug.WriteLine("Ant reseting to position " & mPosition.ToString)
			mRotation = (float)(Ants.Rand.NextDouble() * Math.PI * 2);
		}

		//Called when the ant finds a food
		public void IncFitness()
		{
			this.Fitness += 1;
		}

		//The next set of properties are simple accessors for the module leve variables of the ant.
		public PointF Position {
			get { return mPosition; }
			set { mPosition = value; }
		}

		public float Fitness {
			get { return mFitness; }
			set { mFitness = value; }
		}

		public int NumWeights {
			get { return mBrain.WeightCount; }
		}

		public float[] Weights {
			get { return mBrain.Weights; }
			set { mBrain.Weights = value; }
		}

		public NeuralNetwork Brain {
			get { return mBrain; }
			set { mBrain = value; }
		}

		public int LastFoundFood {
			get { return mLastFoundFood; }
			set { mLastFoundFood = value; }
		}

		//The next set of subs and functions are utility functions for the vectoring of the ant.

		//Uses pythagorean theory to get the length of a vector
		private float VectorLength(PointF p)
		{
			return (float)Math.Sqrt(p.X * p.X + p.Y * p.Y);
		}

		//Normalizes the vector to values between -1 and 1
		private void VectorNormalize(ref PointF p)
		{
			float length = this.VectorLength(p);
			p.X = p.X / length;
			p.Y = p.Y / length;
		}

		//Returns the distance between two points.
		private PointF VectDistance(PointF p, PointF p2)
		{
			PointF ret = default(PointF);
			ret = new PointF(p.X, p.Y);
			ret.X -= p2.X;
			ret.Y -= p2.Y;
			return ret;
		}

		private PointF MultVect(ref PointF p, float value)
		{
			PointF ret = new PointF(p.X, p.Y);
			ret.X *= value;
			ret.Y *= value;
			return ret;
		}

		private PointF AddVectors(ref PointF p, PointF p2)
		{
			p.X += p2.X;
			p.Y += p2.Y;
			return p;
		}

		//Just ensures that a value is between the min and max.
		private void Clamp(ref float arg, float min, float max)
		{
			if (arg < min) {
				arg = min;
			}
			if (arg > max) {
				arg = max;
			}
		}

		//Used for sorting the array of ants.  The sort value is based on the other ants fitness compared to this one.
		public int CompareTo(object obj) //todo - JB implement the interface later
		{
			Ant a = null;
			a = (Ant)obj;
			if (a.Fitness < this.Fitness)
				return -1;
			else if (a.Fitness == this.Fitness)
				return 0;
			else //if (a.Fitness > this.Fitness)
				return 1;
		}
	}
}
