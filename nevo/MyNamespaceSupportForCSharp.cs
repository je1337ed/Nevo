using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;


// This file was created by the VB to C# converter (SharpDevelop 4.4.1.9729).
// It contains classes for supporting the VB "My" namespace in C#.
// If the VB application does not use the "My" namespace, or if you removed the usage
// after the conversion to C#, you can delete this file.

namespace Ants.My
{
	sealed partial class MyProject
	{
        //[ThreadStatic] static MyApplication application;
		
        //public static MyApplication Application {
        //    [DebuggerStepThrough]
        //    get {
        //        if (application == null)
        //            application = new MyApplication();
        //        return application;
        //    }
        //}
		
        //[ThreadStatic] static MyComputer computer;
		
        //public static MyComputer Computer {
        //    [DebuggerStepThrough]
        //    get {
        //        if (computer == null)
        //            computer = new MyComputer();
        //        return computer;
        //    }
        //}
		
        //[ThreadStatic] static User user;
		
        //public static User User {
        //    [DebuggerStepThrough]
        //    get {
        //        if (user == null)
        //            user = new User();
        //        return user;
        //    }
        //}
		
		[ThreadStatic] static MyForms forms;
		
		public static MyForms Forms {
			[DebuggerStepThrough]
			get {
				if (forms == null)
					forms = new MyForms();
				return forms;
			}
		}
		
		internal sealed class MyForms
		{
			global::Ants.FrmFarm FrmFarm_instance;
			bool FrmFarm_isCreating;
			public global::Ants.FrmFarm FrmFarm {
				[DebuggerStepThrough] get { return GetForm(ref FrmFarm_instance, ref FrmFarm_isCreating); }
				[DebuggerStepThrough] set { SetForm(ref FrmFarm_instance, value); }
			}
			
			[DebuggerStepThrough]
			static T GetForm<T>(ref T instance, ref bool isCreating) where T : Form, new()
			{
				if (instance == null || instance.IsDisposed) {
					if (isCreating) {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        ResourceManager resourceManager = new ResourceManager("Resources.Strings", assembly);
                        string myString = resourceManager.GetString("WinForms_RecursiveFormCreate");

                        throw new InvalidOperationException(myString);
					}
					isCreating = true;
					try {
						instance = new T();
					} catch (System.Reflection.TargetInvocationException ex) {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        ResourceManager resourceManager = new ResourceManager("Resources.Strings", assembly);
                        string myString = resourceManager.GetString("WinForms_SeeInnerException");

						throw new InvalidOperationException(myString);
					} finally {
						isCreating = false;
					}
				}
				return instance;
			}
			
			[DebuggerStepThrough]
			static void SetForm<T>(ref T instance, T value) where T : Form
			{
				if (instance != value) {
					if (value == null) {
						instance.Dispose();
						instance = null;
					} else {
						throw new ArgumentException("Property can only be set to null");
					}
				}
			}
		}
	}
	
    //partial class MyApplication : WindowsFormsApplicationBase
    //{
    //    [STAThread]
    //    public static void Main(string[] args)
    //    {
    //        Application.SetCompatibleTextRenderingDefault(UseCompatibleTextRendering);
    //        MyProject.Application.Run(args);
    //    }
    //}
	
    //partial class MyComputer : Computer
    //{
    //}
}
