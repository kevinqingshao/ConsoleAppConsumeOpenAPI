using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleAppConsumeOpenAPI
{
    class Program
    {
        private static string APIUrl = "https://localhost:44350/api/StandardAccountsAPI";
        private static string BaseUrl = "https://localhost:44350/";
        static void Main(string[] args)
        {
            do { DemoMenuOptions(); } while (true);
        }
        static async void DemoMenuOptions()
        {
            int userChoice;
            int accountId;
            StandardAccount account = new StandardAccount();

            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to Console Application Consume Open API demo!");
                Console.WriteLine("\nChoose one of the following options:\n");
                Console.WriteLine("[ 1 ] Get All Accounts");
                Console.WriteLine("[ 2 ] Get Account by Id");
                Console.WriteLine("[ 3 ] Add Account");
                Console.WriteLine("[ 4 ] Update Account");
                Console.WriteLine("[ 5 ] Delete Account");
                Console.WriteLine("[ 0 ] Quit demo\n");
            } while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 0 || userChoice > 5);

            Console.Clear();
            switch (userChoice)
            {
                case 1:
                    await GetAll();
                    break;
                case 2:
                    do {
                        Console.Write("Please enter Account Id:");
                    } while (!int.TryParse(Console.ReadLine(), out accountId) || accountId < 0 || accountId > 9999);
                    await GetById(accountId);
                    break;
                case 3:
                    EnterAccountData(account);
                    await AddAccountData(account);
                    break;
                case 4:
                    do
                    {
                        Console.Write("Please enter Account Id:");
                    } while (!int.TryParse(Console.ReadLine(), out accountId) || accountId < 0 || accountId > 9999);
                    EnterAccountData(account);
                    await UpdateAccountData(accountId, account);
                    break;
                case 5:
                    do
                    {
                        Console.Write("Please enter Account Id:");
                    } while (!int.TryParse(Console.ReadLine(), out accountId) || accountId < 0 || accountId > 9999);
                    await DeleteById(accountId);
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Try again!!");
                    break;
            }
        }
        private static void EnterAccountData(StandardAccount account)
        {
            try
            {
                Console.WriteLine("Enter Account Code:");
                account.AccountCode = Console.ReadLine();
                Console.WriteLine("Enter Account Name:");
                account.Name = Console.ReadLine();
                Console.WriteLine("Enter Account Type:");
                account.Type = Console.ReadLine();
                Console.WriteLine("Enter Account OpenDate:");
                account.OpenDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter Account Currency:");
                account.Currency = Console.ReadLine();
            }
            catch (Exception e)
            {

            }
        }
        private static void DisplayAccount(StandardAccount account)
        {
            Console.WriteLine($"Account.Id: {account.Id}");
            Console.WriteLine($"Account.AccountCode: {account.AccountCode}");
            Console.WriteLine($"Account.Name: {account.Name}");
            Console.WriteLine($"Account.Type: {account.Type}");
            Console.WriteLine("Account.OpenDate: {0:MM/dd/yyyy}", account.OpenDate);
            Console.WriteLine($"Account.Currency: {account.Currency}");
            Console.WriteLine();
        }
        public static async Task GetAll()
        {
            using (var httpClient = new HttpClient())
            {
                var client = new swaggerClient(BaseUrl, httpClient);
                var accounts = await client.StandardAccountsAPIAllAsync().ConfigureAwait(false);
                foreach (var account in accounts)
                {
                    DisplayAccount(account);
                }
            }
        }

        public static async Task GetById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var client = new swaggerClient(BaseUrl, httpClient);
                    var account = await client.StandardAccountsAPI2Async(id);
                    if (account == null)
                    {
                        Console.WriteLine($"Account {id} data can not be found!");
                    }
                    else
                    {
                        DisplayAccount(account);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Account {id} data can not be found!");
                }
            }
        }
        public static async Task AddAccountData(StandardAccount account)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var client = new swaggerClient(BaseUrl, httpClient);
                    await client.StandardAccountsAPIAsync(account);
                    Console.WriteLine($"Account data has been inserted.");
                }
                catch (Exception e)
                {

                }
            }
        }
        public static async Task UpdateAccountData(int id, StandardAccount account)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var client = new swaggerClient(BaseUrl, httpClient);
                    await client.StandardAccountsAPI3Async(id, account);
                    Console.WriteLine($"Account data has been updated.");
                }
                catch (Exception e)
                {

                }
            }
        }

        public static async Task DeleteById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var client = new swaggerClient(BaseUrl, httpClient);
                    await client.StandardAccountsAPI4Async(id);
                    Console.WriteLine($"Account {id} data has been deleted!");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Account {id} data can not be found!");
                }
            }
        }

        public static async void UpdateDataWithoutAuthentication()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(APIUrl);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();
                    Console.WriteLine(rawResponse);
                }
                Console.WriteLine("Complete");
            }
        }

        public static async void DeleteDataWithoutAuthentication()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(APIUrl);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();
                    Console.WriteLine(rawResponse);
                }
                Console.WriteLine("Complete");
            }
        }
    }
}
