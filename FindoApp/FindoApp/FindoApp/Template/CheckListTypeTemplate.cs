using FindoApp.View;
using Xamarin.Forms;

namespace FindoApp.Template
{
    public class CheckListTypeTemplate : DataTemplateSelector
    {
        DataTemplate TextTemplate;

        public CheckListTypeTemplate()
        {
            TextTemplate = new DataTemplate(typeof(FindoTextCell));
        }

        protected override Xamarin.Forms.DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Domain.Model.CheckListItem menu = item as Domain.Model.CheckListItem;
            switch (menu.AnswerType)
            {
                default:
                    return TextTemplate;
            }
        }
    }
}
