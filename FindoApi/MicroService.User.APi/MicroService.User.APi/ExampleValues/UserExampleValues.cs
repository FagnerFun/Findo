namespace MicroService.User.APi.ExampleValues
{
    public static class UserExampleValues
    {
        /// <summary>
        /// Get Object Login
        /// </summary>
        /// <returns> Object reference <see cref="Domain.Model.User"/></returns>
        public static Domain.Model.User GetUser()
        {
            return new Domain.Model.User
            {
                Mail = "fagner.muniz@lobu.com.br",
                Password = "1234"
            };
        }
    }
}
