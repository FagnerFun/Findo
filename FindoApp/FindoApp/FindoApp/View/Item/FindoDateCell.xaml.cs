using FindoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindoApp.View.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindoDateCell : ViewCell
    {
        public static BindableProperty ParentBindingContextProperty = BindableProperty.Create(nameof(ParentBindingContext), typeof(object), typeof(FindoDateCell), null);

        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }

        public FindoDateCell()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var item = ((TappedEventArgs)e).Parameter;
            ((CheckListItemViewModel)ParentBindingContext).TitleCommand.Execute(item);
        }
    }
}