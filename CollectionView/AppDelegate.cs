using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace CollectionView
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		private MainWindowController mainWindowController;
		private NSObject[] titles;
		private NSCollectionView cv;
		private MyCollectionViewItem cvi;
		
		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			mainWindowController = new MainWindowController ();
			var window = mainWindowController.Window;
			window.MakeKeyAndOrderFront (this);
			
			titles = new NSObject[] { 
				new NSString("Item 1"), 
				new NSString("Item 2"),
				new NSString("Item 3"), 
				new NSString("Item 4"),
				new NSString("Item 5"), 
				new NSString("Item 6"),
				new NSString("Item 7"), 
				new NSString("Item 8"),
			};
				                                                               
    		cv = new NSCollectionView(window.ContentView.Frame);
			cvi = new MyCollectionViewItem();
			cv.ItemPrototype = cvi;
			cv.Content = titles;
			
			cv.AutoresizingMask = NSViewResizingMask.MinXMargin | NSViewResizingMask.WidthSizable | NSViewResizingMask.MaxXMargin | NSViewResizingMask.MinYMargin | NSViewResizingMask.HeightSizable | NSViewResizingMask.MaxYMargin;    
			window.ContentView.AddSubview(cv);
		}
	}
	
	public class MyCollectionViewItem : NSCollectionViewItem
	{
		private static readonly NSString EMPTY_NSSTRING = new NSString(string.Empty);
		private MyView view;
		
		public MyCollectionViewItem() : base()
		{
			
		}
		
		public MyCollectionViewItem(IntPtr ptr) : base(ptr)
		{
			
		}
			
		public override void LoadView ()
		{
			view = new MyView();
			View = view;
		}
		
		public override NSObject RepresentedObject 
		{
			get { return base.RepresentedObject; }
			
			set 
			{
				if (value == null)
				{
					// Need to do this because setting RepresentedObject in base to null 
					// throws an exception because of the MonoMac generated wrappers,
					// and even though we don't have any null values, this will get 
					// called during initialization of the content with a null value.
					base.RepresentedObject = EMPTY_NSSTRING;
					view.Button.Title = string.Empty;
				}
				else
				{
					base.RepresentedObject = value;
					view.Button.Title = value.ToString();
				}
			}
		}
	}
	
	public class MyView : NSView
	{
		private NSButton button;
		
		public MyView() : base(new RectangleF(0,0,100,40))
		{
			button = new NSButton(new RectangleF(10,10,80,20));
			AddSubview(button);
		}
		
		public NSButton Button
		{
			get { return button; }	
		}		
	}
}

