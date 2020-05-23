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

        public CheckListTypeTemplate()
        {
            TextTemplate = new DataTemplate(typeof(FindoTextCell));
            ComboTemplate = new DataTemplate(typeof(FindoComboCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Domain.Model.CheckListItem menu = item as Domain.Model.CheckListItem;
            switch (menu.AnswerType)
            {
                case enAnswerType.Combo:
                    ComboTemplate.SetValue(FindoComboCell.ParentBindingContextProperty, container.BindingContext);
                    return ComboTemplate;
                default:
                    TextTemplate.SetValue(FindoTextCell.ParentBindingContextProperty, container.BindingContext);
                    return TextTemplate;
            }
        }
    }
}
