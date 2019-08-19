using Microsoft.WindowsAzure.MobileServices;

namespace FindoApp.Services
{
    public class AzureService
    {
        private AzureService()
        {
            CurrentClient = new MobileServiceClient(Settings.ApplicationURL);
        }

        public static AzureService DefaultManager { get; private set; } = new AzureService();

        public MobileServiceClient CurrentClient { get; }

    }
}