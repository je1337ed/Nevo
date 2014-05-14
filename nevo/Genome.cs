using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Ants
{
	//Simple genetic algorithm class for the ants.
	public class Genome
	{
		private Ant[] mAnts;
		private int mPopSize;

		private int mChromoLength;
		private float mTotalFitness;
		private float mBestFitness;
		private float mAvgFitness;
		private float mWorstFitness;

		private int mFittestGenome;
		private float mMutationRate;

		private float mCrossOverRate;

		private int mGeneration;
		//Setup the GA.  The Ant class is our actual chromosome while the weights in the ants neural network form the genes.
		public Genome(Ant[] Ants, int ChromoLength, float MutationRate, float CrossOverRate)
		{
			mAnts = Ants;
			mPopSize = mAnts.Length - 1;
			//FileSystem.Reset(); - jb check later
			mGeneration = 0;
			mMutationRate = MutationRate;
			mCrossOverRate = CrossOverRate;
			mChromoLength = ChromoLength;
		}

		//Simple single point cross over function.
		public void CrossOVer(ref float[] P1, ref float[] P2, ref float[] C1, ref float[] C2)
		{
			int idx = 0;
			if (Ants.Rand.NextDouble() > mCrossOverRate) {
				//If we shouldn't cross over then we'll just return the two children
				for (idx = 0; idx <= P1.Length - 1; idx++) {
					C1[idx] = P1[idx];
					C2[idx] = P2[idx];
				}
				return;
			}
			int XPoint = Ants.Rand.Next(0, P1.Length - 1);
			for (idx = 0; idx <= XPoint - 1; idx++) {
				C1[idx] = P1[idx];
				C2[idx] = P2[idx];
			}
			for (idx = XPoint; idx <= P1.Length - 1; idx++) {
				C1[idx] = P2[idx];
				C2[idx] = P1[idx];
			}
		}

		//Loops over each gene in the chromosome and decides whether to mutate it or not.  No max mutation rate
		//has been set for this function.
		public void Mutate(ref float[] C)
		{
			int idx = 0;
			for (idx = 0; idx <= C.Length - 1; idx++) {
				if (Ants.Rand.NextDouble() < mMutationRate) {
					C[idx] += (float)Ants.Rand.NextDouble();
				}
			}
		}

		//Use roulette selection to return an ant.
		public Ant RouletteSelection()
		{
			float Slice = (float)Ants.Rand.NextDouble() * mTotalFitness;
			Ant ChosenOne = null;
			float FitSoFar = 0;
			int idx = 0;

			for (idx = 0; idx <= mAnts.Length - 1; idx++) {
				FitSoFar += mAnts[idx].Fitness;
				if (FitSoFar > Slice) {
					ChosenOne = mAnts[idx];
					break; // TODO: might not be correct. Was : Exit For
				}
			}
			if (ChosenOne == null) {
				ChosenOne = mAnts[idx - 1];
			}
			return ChosenOne;
		}

		//The main work horse of a Genetic Algorithm
		public void Epoch()
		{
			//FileSystem.Reset(); - jb - check later
			//Sort it for elitism and scaling functions
			Array.Sort(mAnts);
			CalcStats();
			//Create an array to store the new population.
			float[][] NewPop = null;
			NewPop = new float[mAnts.Length][];
			int idx = (int) Math.Round(mAnts.Length * 0.1); //todo - jb - check the math
			//A bit of elitism here.  We'll grab the top 10% and put it into the new population.
			GrabNBest(idx, NewPop);
			//Now go and make the rest of the population
			idx += 1;
			while (idx < mAnts.Length) {
				float[] P1 = RouletteSelection().Weights;
				float[] P2 = RouletteSelection().Weights;
				float[] C1 = null;
				float[] C2 = null;
				C1 = new float[P1.Length];
				C2 = new float[P2.Length];
				CrossOVer(ref P1, ref P2, ref C1, ref C2);
				Mutate(ref C1);
				Mutate(ref C2);
				NewPop[idx] = C1;
				if (idx + 1 < mAnts.Length) {
					NewPop[idx + 1] = C2;
				}
				idx += 2;
			}
			//Put the new weights back into the population.
			for (idx = 0; idx <= mAnts.Length - 1; idx++) {
				mAnts[idx].Weights = NewPop[idx];
			}
			mGeneration += 1;
		}

		//Elitism function.  It grabs the first "num" entries in the ants array.  Its assumed that the
		//array has already been sorted.
		public void GrabNBest(int num, float[][] NewPop)
		{
			int idx = 0;
			for (idx = 0; idx <= num; idx++) {
				NewPop[idx] = mAnts[idx].Weights;
			}
		}

		//Just some statistics on the population.
		public void CalcStats()
		{
			int idx = 0;
			float highest = 0;
			float lowest = 999999;

			for (idx = 0; idx <= mAnts.Length - 1; idx++) {
				mTotalFitness += mAnts[idx].Fitness;
				if (mAnts[idx].Fitness > highest) {
					highest = mAnts[idx].Fitness;
					mFittestGenome = idx;
				}
				if (mAnts[idx].Fitness < lowest) {
					lowest = mAnts[idx].Fitness;
				}
			}
			mWorstFitness = lowest;
			mBestFitness = highest;
			mAvgFitness = mTotalFitness / mAnts.Length - 1;
		}

		//Reset the algorithms statistics.
		private void Reset()
		{
			mTotalFitness = 0;
			mBestFitness = 0;
			mAvgFitness = 0;
			mWorstFitness = 99999;
			mFittestGenome = 0;
		}

		//The following are just accessor properties to the statistics.
		public float TotalFitness {
			get { return mTotalFitness; }
		}
		public float BestFitness {
			get { return mBestFitness; }
		}
		public float AvgFitness {
			get { return mAvgFitness; }
		}
		public float WorstFitness {
			get { return mWorstFitness; }
		}
		public int Generations {
			get { return mGeneration; }
		}
	}
}
