﻿using FindoApp.Domain.Model;
using FindoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindoApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckListItemPage : ContentPage
	{
		public CheckListItemPage ()
		{
			InitializeComponent ();
		}
	}
}