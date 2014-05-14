using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Ants
{
	//Controls the antfarm class.  Sorry but there isn't a lot of fancy graphics here.
	public class FrmFarm : System.Windows.Forms.Form
	{
		private AntFarm mAntFarm;

		private System.Threading.Thread mFarmThread;
		private System.Drawing.Pen mFoodPen;
		private System.Drawing.Rectangle mFoodRec;
		private System.Drawing.Pen mAntPen;
		private System.Drawing.Pen mUberAntPen;

		private System.Drawing.Rectangle mAntRec;
		#region " Windows Form Designer generated code "

		public FrmFarm() : base()
		{
			Load += FrmFarm_Load;

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if ((components != null)) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer

		private System.ComponentModel.IContainer components;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		private System.Windows.Forms.Button withEventsField_CmdStart;
		internal System.Windows.Forms.Button CmdStart {
			get { return withEventsField_CmdStart; }
			set {
				if (withEventsField_CmdStart != null) {
					withEventsField_CmdStart.Click -= CmdStart_Click;
				}
				withEventsField_CmdStart = value;
				if (withEventsField_CmdStart != null) {
					withEventsField_CmdStart.Click += CmdStart_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_CmdReset;
		internal System.Windows.Forms.Button CmdReset {
			get { return withEventsField_CmdReset; }
			set {
				if (withEventsField_CmdReset != null) {
					withEventsField_CmdReset.Click -= CmdReset_Click;
				}
				withEventsField_CmdReset = value;
				if (withEventsField_CmdReset != null) {
					withEventsField_CmdReset.Click += CmdReset_Click;
				}
			}
		}
		private System.Windows.Forms.CheckBox withEventsField_ChkLock;
		internal System.Windows.Forms.CheckBox ChkLock {
			get { return withEventsField_ChkLock; }
			set {
				if (withEventsField_ChkLock != null) {
					withEventsField_ChkLock.CheckedChanged -= ChkLock_CheckedChanged;
				}
				withEventsField_ChkLock = value;
				if (withEventsField_ChkLock != null) {
					withEventsField_ChkLock.CheckedChanged += ChkLock_CheckedChanged;
				}
			}
		}
		internal System.Windows.Forms.CheckBox ChkRender;
		internal System.Windows.Forms.TextBox TxtAnts;
		internal System.Windows.Forms.TextBox TxtFood;
		internal System.Windows.Forms.Label LblAnts;
		internal System.Windows.Forms.Label LblFood;
		private System.Windows.Forms.Panel withEventsField_PnlField;
		internal System.Windows.Forms.Panel PnlField {
			get { return withEventsField_PnlField; }
			set {
				if (withEventsField_PnlField != null) {
					withEventsField_PnlField.Paint -= PnlField_Paint;
				}
				withEventsField_PnlField = value;
				if (withEventsField_PnlField != null) {
					withEventsField_PnlField.Paint += PnlField_Paint;
				}
			}
		}
		private System.Windows.Forms.Timer withEventsField_TimerPaint;
		internal System.Windows.Forms.Timer TimerPaint {
			get { return withEventsField_TimerPaint; }
			set {
				if (withEventsField_TimerPaint != null) {
					withEventsField_TimerPaint.Tick -= TimerPaint_Tick;
				}
				withEventsField_TimerPaint = value;
				if (withEventsField_TimerPaint != null) {
					withEventsField_TimerPaint.Tick += TimerPaint_Tick;
				}
			}
		}
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label LblAvg;
		internal System.Windows.Forms.Label LblTot;
		internal System.Windows.Forms.Label LblBest;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.Label LblNumGen;
		internal System.Windows.Forms.Panel PnlControl;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label LblCurBestScore;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.PnlControl = new System.Windows.Forms.Panel();
			this.LblFood = new System.Windows.Forms.Label();
			this.LblAnts = new System.Windows.Forms.Label();
			this.TxtFood = new System.Windows.Forms.TextBox();
			this.TxtAnts = new System.Windows.Forms.TextBox();
			this.ChkRender = new System.Windows.Forms.CheckBox();
			this.ChkLock = new System.Windows.Forms.CheckBox();
			this.CmdReset = new System.Windows.Forms.Button();
			this.CmdStart = new System.Windows.Forms.Button();
			this.Label7 = new System.Windows.Forms.Label();
			this.Label6 = new System.Windows.Forms.Label();
			this.LblBest = new System.Windows.Forms.Label();
			this.LblTot = new System.Windows.Forms.Label();
			this.LblAvg = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.PnlField = new System.Windows.Forms.Panel();
			this.TimerPaint = new System.Windows.Forms.Timer(this.components);
			this.Label1 = new System.Windows.Forms.Label();
			this.LblNumGen = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.LblCurBestScore = new System.Windows.Forms.Label();
			this.PnlControl.SuspendLayout();
			this.SuspendLayout();
			//
			//PnlControl
			//
			this.PnlControl.Controls.AddRange(new System.Windows.Forms.Control[] {
				this.Label3,
				this.LblFood,
				this.LblAnts,
				this.TxtFood,
				this.TxtAnts,
				this.ChkRender,
				this.ChkLock,
				this.CmdReset,
				this.CmdStart,
				this.Label7,
				this.Label6,
				this.LblBest,
				this.LblTot,
				this.LblAvg,
				this.Label2
			});
			this.PnlControl.Location = new System.Drawing.Point(346, 24);
			this.PnlControl.Name = "PnlControl";
			this.PnlControl.Size = new System.Drawing.Size(136, 387);
			this.PnlControl.TabIndex = 0;
			//
			//LblFood
			//
			this.LblFood.Location = new System.Drawing.Point(21, 308);
			this.LblFood.Name = "LblFood";
			this.LblFood.Size = new System.Drawing.Size(32, 23);
			this.LblFood.TabIndex = 8;
			this.LblFood.Text = "Food";
			//
			//LblAnts
			//
			this.LblAnts.Location = new System.Drawing.Point(21, 280);
			this.LblAnts.Name = "LblAnts";
			this.LblAnts.Size = new System.Drawing.Size(32, 23);
			this.LblAnts.TabIndex = 7;
			this.LblAnts.Text = "Ants";
			//
			//TxtFood
			//
			this.TxtFood.Location = new System.Drawing.Point(64, 308);
			this.TxtFood.Name = "TxtFood";
			this.TxtFood.Size = new System.Drawing.Size(44, 20);
			this.TxtFood.TabIndex = 6;
			this.TxtFood.Text = "25";
			//
			//TxtAnts
			//
			this.TxtAnts.Location = new System.Drawing.Point(64, 280);
			this.TxtAnts.Name = "TxtAnts";
			this.TxtAnts.Size = new System.Drawing.Size(44, 20);
			this.TxtAnts.TabIndex = 5;
			this.TxtAnts.Text = "20";
			//
			//ChkRender
			//
			this.ChkRender.Checked = true;
			this.ChkRender.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ChkRender.Location = new System.Drawing.Point(28, 39);
			this.ChkRender.Name = "ChkRender";
			this.ChkRender.Size = new System.Drawing.Size(79, 24);
			this.ChkRender.TabIndex = 4;
			this.ChkRender.Text = "Render";
			//
			//ChkLock
			//
			this.ChkLock.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.ChkLock.Checked = true;
			this.ChkLock.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ChkLock.Location = new System.Drawing.Point(28, 8);
			this.ChkLock.Name = "ChkLock";
			this.ChkLock.Size = new System.Drawing.Size(79, 24);
			this.ChkLock.TabIndex = 3;
			this.ChkLock.Text = "Lock";
			//
			//CmdReset
			//
			this.CmdReset.Location = new System.Drawing.Point(21, 336);
			this.CmdReset.Name = "CmdReset";
			this.CmdReset.Size = new System.Drawing.Size(88, 28);
			this.CmdReset.TabIndex = 1;
			this.CmdReset.Text = "Reset";
			//
			//CmdStart
			//
			this.CmdStart.Location = new System.Drawing.Point(23, 71);
			this.CmdStart.Name = "CmdStart";
			this.CmdStart.Size = new System.Drawing.Size(88, 28);
			this.CmdStart.TabIndex = 0;
			this.CmdStart.Text = "&Start";
			//
			//Label7
			//
			this.Label7.Location = new System.Drawing.Point(17, 159);
			this.Label7.Name = "Label7";
			this.Label7.Size = new System.Drawing.Size(71, 16);
			this.Label7.TabIndex = 16;
			this.Label7.Text = "Avg Fitness";
			//
			//Label6
			//
			this.Label6.Location = new System.Drawing.Point(17, 183);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(71, 16);
			this.Label6.TabIndex = 15;
			this.Label6.Text = "Best Fitness";
			//
			//LblBest
			//
			this.LblBest.Location = new System.Drawing.Point(92, 183);
			this.LblBest.Name = "LblBest";
			this.LblBest.Size = new System.Drawing.Size(36, 16);
			this.LblBest.TabIndex = 14;
			//
			//LblTot
			//
			this.LblTot.Location = new System.Drawing.Point(92, 135);
			this.LblTot.Name = "LblTot";
			this.LblTot.Size = new System.Drawing.Size(36, 16);
			this.LblTot.TabIndex = 13;
			//
			//LblAvg
			//
			this.LblAvg.Location = new System.Drawing.Point(92, 159);
			this.LblAvg.Name = "LblAvg";
			this.LblAvg.Size = new System.Drawing.Size(36, 16);
			this.LblAvg.TabIndex = 12;
			//
			//Label2
			//
			this.Label2.Location = new System.Drawing.Point(17, 135);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(71, 16);
			this.Label2.TabIndex = 11;
			this.Label2.Text = "Total Fitness";
			//
			//PnlField
			//
			this.PnlField.BackColor = System.Drawing.Color.Black;
			this.PnlField.Location = new System.Drawing.Point(4, 24);
			this.PnlField.Name = "PnlField";
			this.PnlField.Size = new System.Drawing.Size(332, 388);
			this.PnlField.TabIndex = 1;
			//
			//TimerPaint
			//
			this.TimerPaint.Interval = 33;
			//
			//Label1
			//
			this.Label1.Location = new System.Drawing.Point(7, 4);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(73, 16);
			this.Label1.TabIndex = 9;
			this.Label1.Text = "Generations:";
			//
			//LblNumGen
			//
			this.LblNumGen.Location = new System.Drawing.Point(84, 4);
			this.LblNumGen.Name = "LblNumGen";
			this.LblNumGen.Size = new System.Drawing.Size(36, 16);
			this.LblNumGen.TabIndex = 10;
			//
			//Label3
			//
			this.Label3.Location = new System.Drawing.Point(4, 112);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(132, 16);
			this.Label3.TabIndex = 17;
			this.Label3.Text = "Last Generation Stats";
			this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//Label4
			//
			this.Label4.Location = new System.Drawing.Point(132, 4);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(64, 16);
			this.Label4.TabIndex = 11;
			this.Label4.Text = "Best Score:";
			//
			//LblCurBestScore
			//
			this.LblCurBestScore.Location = new System.Drawing.Point(200, 4);
			this.LblCurBestScore.Name = "LblCurBestScore";
			this.LblCurBestScore.Size = new System.Drawing.Size(44, 16);
			this.LblCurBestScore.TabIndex = 12;
			//
			//FrmFarm
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(484, 417);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
				this.LblCurBestScore,
				this.Label4,
				this.LblNumGen,
				this.PnlField,
				this.PnlControl,
				this.Label1
			});
			this.Name = "FrmFarm";
			this.Text = "Ant Farm";
			this.PnlControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion


		private void FrmFarm_Load(System.Object sender, System.EventArgs e)
		{
			mFoodPen = new System.Drawing.Pen(Color.Yellow);
			mFoodRec = new System.Drawing.Rectangle(1, 1, 5, 5);
			mAntPen = new System.Drawing.Pen(Color.Red);
			mUberAntPen = new System.Drawing.Pen(Color.Azure);
			mAntRec = new System.Drawing.Rectangle(1, 1, 5, 5);
            TimerPaint.Interval = 1;//Ants.SleepInterval; - jb check later
		}

		private void PnlField_Paint(System.Object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int idx = 0;
			if (ChkRender.Checked == false)
				return;
			if (mAntFarm == null)
				return;
			//don't want to draw nothing.
			System.Drawing.Graphics g = PnlField.CreateGraphics();
			PointF p = default(PointF);
			for (idx = 0; idx <= mAntFarm.Ants.Length - 1; idx++) {
				p = mAntFarm.Ants[idx].Position;
				mAntRec.X = (int)p.X;
				mAntRec.Y = (int)p.Y;
				if (mAntFarm.Ants[idx].Fitness == mAntFarm.BestScore & mAntFarm.BestScore != 0) {
					g.DrawEllipse(mUberAntPen, mAntRec);
				} else {
					g.DrawEllipse(mAntPen, mAntRec);
				}

			}

			for (idx = 0; idx <= mAntFarm.Food.Length - 1; idx++) {
				p = mAntFarm.Food[idx];
				mFoodRec.X =(int) p.X;
				mFoodRec.Y = (int)p.Y;
				g.DrawRectangle(mFoodPen, mFoodRec);
			}
			g.Dispose();
		}

		private void CmdStart_Click(System.Object sender, System.EventArgs e)
		{
			StartRun();
			CmdStart.Enabled = false;
		}
		private void StartRun()
		{
			int numAnts = 0;
			int numFood = 0;
			numAnts = int.Parse(TxtAnts.Text);
			numFood = int.Parse(TxtFood.Text);
			mAntFarm = new AntFarm(numAnts, numFood, PnlField.Width, PnlField.Height);
			float[] x = mAntFarm.Ants[1].Brain.Weights;
			float[] y = mAntFarm.Ants[2].Brain.Weights;

			if (mFarmThread == null == false) {
				mFarmThread.Abort();
				mFarmThread = null;
			}
			mFarmThread = new System.Threading.Thread(mAntFarm.Start);
			mFarmThread.Start();
			TimerPaint.Enabled = true;
		}
		protected override void OnClosed(System.EventArgs e)
		{
			//Debug.WriteLine("closed")
			if (mAntFarm == null == false) {
				mAntFarm.Suspend();
			}
		}

		private void TimerPaint_Tick(System.Object sender, System.EventArgs e)
		{
			PnlField.Invalidate();
			LblNumGen.Text = mAntFarm.Genome.Generations.ToString();
			LblTot.Text = mAntFarm.Genome.TotalFitness.ToString();
			LblAvg.Text = mAntFarm.Genome.AvgFitness.ToString();
			LblBest.Text = mAntFarm.Genome.BestFitness.ToString();
			LblCurBestScore.Text = mAntFarm.BestScore.ToString();
		}

		private void ChkLock_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (mAntFarm == null == false) {
				mAntFarm.FastRender = !ChkLock.Checked;
				if (mAntFarm.FastRender) {
					TimerPaint.Interval = 1;
				} else {
					TimerPaint.Interval = 33;
				}
			}
		}

		private void CmdReset_Click(System.Object sender, System.EventArgs e)
		{
			StartRun();
		}

		protected override void OnResize(System.EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
				return;
			PnlControl.Left = this.Width - PnlControl.Width - 10;
			PnlField.Width = this.Width - 10 - PnlControl.Width - 20;
			PnlField.Height = this.Height - PnlField.Top - 30;
			if (mAntFarm == null)
				return;
			mAntFarm.Width = PnlField.Width;
			mAntFarm.Height = PnlField.Height;
		}
	}
}
