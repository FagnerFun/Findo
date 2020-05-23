using FindoApp.Control;
using FindoApp.Domain.Model.Enum;
using FindoApp.View;
using FindoApp.View.Item;
using Xamarin.Forms;

namespace FindoApp.Template
{
    public class CheckListTypeTemplate : DataTemplateSelector
    {
        DataTemplate TextTemplate;
        DataTemplate ComboTemplate;
        DataTemplate MultiSelectionTemplate;
        DataTemplate DateTemplate;

        public CheckListTypeTemplate()
        {
            TextTemplate = new DataTemplate(typeof(FindoTextCell));
            ComboTemplate = new DataTemplate(typeof(FindoComboCell));
            MultiSelectionTemplate = new DataTemplate(typeof(FindoMultiSelectionCell));
            DateTemplate = new DataTemplate(typeof(FindoDateCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Domain.Model.CheckListItem menu = item as Domain.Model.CheckListItem;
            switch (menu.AnswerType)
            {
                case enAnswerType.Combo:
                    ComboTemplate.SetValue(FindoComboCell.ParentBindingContextProperty, container.BindingContext);
                    return ComboTemplate;
                case enAnswerType.Date:
                    DateTemplate.SetValue(FindoDateCell.ParentBindingContextProperty, container.BindingContext);
                    return DateTemplate;
                case enAnswerType.MultiSelection:
                    MultiSelectionTemplate.SetValue(FindoMultiSelectionCell.ParentBindingContextProperty, container.BindingContext);
                    return MultiSelectionTemplate;
                default:
                    TextTemplate.SetValue(FindoTextCell.ParentBindingContextProperty, container.BindingContext);
                    return TextTemplate;
            }
        }
    }
}
