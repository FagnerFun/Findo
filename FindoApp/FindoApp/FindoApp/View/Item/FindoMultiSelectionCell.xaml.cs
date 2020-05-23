using FindoApp.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindoApp.View.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindoMultiSelectionCell : ViewCell
    {
        public static BindableProperty ParentBindingContextProperty = BindableProperty.Create(nameof(ParentBindingContext), typeof(object), typeof(FindoComboCell), null);

        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }

        public FindoMultiSelectionCell()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (e == null) return;

            var item = ((TappedEventArgs)e).Parameter;
            ((CheckListItemViewModel)ParentBindingContext).TitleCommand.Execute(item);
        }


        private void Selected_Multi_Item(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;

            var item = e.Item;
            ((CheckListItemViewModel)ParentBindingContext).SelectedItemMultiSelectionCommand.Execute(item);
        }
    }
}