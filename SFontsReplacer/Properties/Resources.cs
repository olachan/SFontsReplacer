using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SystemFontReplacer.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("SystemFontReplacer.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Bitmap rep_deletesxs
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("rep_deletesxs", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap rep_readyrestore
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("rep_readyrestore", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap rep_replacefonts
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("rep_replacefonts", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap rep_restore
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("rep_restore", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap rep_scaning
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("rep_scaning", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Saved
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Saved", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Spalsh
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Spalsh", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal Resources()
		{
		}
	}
}
