using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Ants
{
	//Main storage class for the ant farm.  It runs in its own thread continually calling the update method. 
	//After mMaxticks the epoch is called in the genome class to start a new generation.
	public class AntFarm
	{

		private Ant[] mAnts;
		private PointF[] mFood;
		private int mNumAnts;

		private int mNumFood;
		private bool mFastRender;
		private int mNumTicks;
		private int mMaxTicks = 1000;
		private int mWidth;

		private int mHeight;

		private Random rand = new Random();

		private bool mShouldRun;
		private Genome mGenome;
		private int mBestAnt;

		private int mBestScore;
		public AntFarm(int AntCnt, int FoodCnt, int Width, int Height)
		{
			int idx = 0;

			mNumAnts = AntCnt;
			mNumFood = FoodCnt;
			mWidth = Width;
			mHeight = Height;

			mAnts = new Ant[AntCnt];
			mFood = new PointF[FoodCnt];
			//Add ants to the world
			for (idx = 0; idx <= mAnts.Length - 1; idx++) {
				mAnts[idx] = new Ant(new NeuralNetwork(4, 2, 1, 4, 4, Neuron.ActivationFunction.AFSigmoid), (float)rand.NextDouble() * Width, (float)rand.NextDouble() * Height);
			}

			//Add food to the world
			for (idx = 0; idx <= mFood.Length - 1; idx++) {
				mFood[idx] = new PointF((float)rand.NextDouble() * Width, (float)rand.NextDouble() * Height);
			}

			mGenome = new Genome(mAnts, mAnts[1].NumWeights, 0.01F, 0.05F);

		}

		public void Start()
		{
			mShouldRun = true;
			while ((mShouldRun)) {
				Update();
				if (mFastRender == false) {
					System.Threading.Thread.Sleep(25);
					//System.Threading.Thread.CurrentThread.Sleep(25); //Ants.SleepInterval
				}
			}
			//Debug.WriteLine("Stopped AntFarm")
		}

		public void Suspend()
		{
			//Debug.WriteLine("Stopping AntFarm")
			mShouldRun = false;
		}

		public void Update()
		{
			int idx = 0;
			if (mNumTicks < mMaxTicks) {
				mBestAnt = -1;
				mBestScore = 0;
				for (idx = 0; idx <= mAnts.Length - 1; idx++) {
					mAnts[idx].Update(ref mFood);
					int hit = mAnts[idx].CheckForFood(ref mFood, 5);
					if (hit >= 0) {
						//Debug.WriteLine("Ant" & idx.ToString & " got food " & hit.ToString)
						mAnts[idx].IncFitness();
						mFood[hit] = new PointF((float)rand.NextDouble() * mWidth, (float)rand.NextDouble() * mHeight);
					}
					PointF p = default(PointF);
					p = mAnts[idx].Position;
					//Wrap around code to make sure the ant stays on the board.
					if (p.X > mWidth)
						p.X = 0;
					if (p.X < 0)
						p.X = mWidth;
					if (p.Y > mHeight)
						p.Y = 1;
					if (p.Y < 0)
						p.Y = mHeight;
					mAnts[idx].Position = p;
					if (mAnts[idx].Fitness > mBestScore) {
						mBestAnt = idx;
						mBestScore = (int)Math.Round(mAnts[idx].Fitness);
					}
				}
				mNumTicks += 1;
			//The current generation has lived out its max life.  Its time to do an epoch and start a new generation.
			} else {
				//Debug.WriteLine("Doing Epoch")
				mGenome.Epoch();
				mNumTicks = 0;
				for (idx = 0; idx <= mAnts.Length - 1; idx++) {
					mAnts[idx].Reset(mWidth, mHeight);
				}

			}
		}
		//The rest are simple accessor properties.
		public Ant[] Ants {
			get { return mAnts; }
		}
		public PointF[] Food {
			get { return mFood; }
		}
		public bool FastRender {
			get { return mFastRender; }
			set { mFastRender = value; }
		}
		public Genome Genome {
			get { return mGenome; }
		}
		public int Width {
			get { return mWidth; }
			set { mWidth = value; }
		}
		public int Height {
			get { return mHeight; }
			set { mHeight = value; }
		}
		public int BestAnt {
			get { return mBestAnt; }
		}
		public int BestScore {
			get { return mBestScore; }
		}
	}
}
