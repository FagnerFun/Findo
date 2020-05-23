using FindoApp.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindoApp.View.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindoComboCell : ViewCell
    {
        public static BindableProperty ParentBindingContextProperty = BindableProperty.Create(nameof(ParentBindingContext), typeof(object), typeof(FindoComboCell), null);

        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }

        public FindoComboCell()
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