using MicroService.User.APi.ExampleValues;
using Swashbuckle.AspNetCore.Examples;

namespace MicroService.User.APi.SwaggerExamples
{
    public class UserExample : IExamplesProvider
    {
        /// <summary>
        /// Get example Login model
        /// </summary>
        /// <returns> Object represent <see cref="Domain.Model.User"/></returns>
        public object GetExamples()
        {
            return UserExampleValues.GetUser();
        }
    }
}