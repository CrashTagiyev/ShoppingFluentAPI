using Entities.Concretes.Orders;
using Entities.Concretes.Products;
using Entities.Concretes.Users;
using Microsoft.Win32;
using ShoppingDataAccess.DTO;
using ShoppingDataAccess.DTO.OrdersDTO;
using ShoppingDataAccess.DTO.UsersDTO;
using ShoppingDataAccess.Repositories.Concretes.CategoriesRepos;
using ShoppingDataAccess.Repositories.Concretes.OrdersRepos;
using ShoppingDataAccess.Repositories.Concretes.ProductReps;
using ShoppingDataAccess.Repositories.Concretes.UserAccountsRepos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ShoppingFluentAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        //Repositories
        //Category Repositories
        public CategoriesRepository CategoryREpository { get; set; } = new();
        //Users Repositories
        public UsersAccountsRepository UsersAccountsRepo { get; set; } = new();
        public UserPasswordRepository UserPasswordRepo { get; set; } = new();

        //Product Repositories
        public ProductEntityRepository ProductsRepo { get; set; } = new();


        //Orders Repositories
        public ProductsOrdersRepository ProductsOrdersRepo { get; set; } = new();
        //Other
        //Products
        private ObservableCollection<ProductDTO> products;

        public ObservableCollection<ProductDTO> Products
        {
            get { return products; }
            set { products = value; OnPropertyChanged(nameof(products)); }
        }

        private ProductDTO selectedProduct;

        public ProductDTO SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; OnPropertyChanged(nameof(selectedProduct)); }
        }


        //Users
        //Logged User
        private UsersAccountsDTO loggedUser;

        public UsersAccountsDTO LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; OnPropertyChanged(nameof(loggedUser)); }
        }
        private ObservableCollection<UsersOrdersDTO> loggedUsersOrders;

        public ObservableCollection<UsersOrdersDTO> LoggedUsersOrders
        {
            get { return loggedUsersOrders; }
            set { loggedUsersOrders = value; OnPropertyChanged(nameof(loggedUsersOrders)); }
        }

        public UsersAccountsDTO LogInCheck(string username, string password)
        {
            UsersAccounts CurrentUsersAccount = UsersAccountsRepo.GetAllUserAccounts().FirstOrDefault(u => u.UsernamePasswrodIdNavigation.Username == username && u.UsernamePasswrodIdNavigation.Password == password);
            if (CurrentUsersAccount == null) throw new Exception("Username of password did not found");
            List<ProductsOrders> ProductsOrders = ProductsOrdersRepo.GetAllProductsOrders().Where(po => po.UsersAccountsId == CurrentUsersAccount.Id).ToList();

            ObservableCollection<UsersOrdersDTO> usersOrders = new();



            foreach (var pOrder in ProductsOrders)
            {
                ProductDTO PDTO = new(pOrder.ProductEntityIdNavigation.Id, pOrder.ProductEntityIdNavigation.ProductName, pOrder.ProductEntityIdNavigation.ProductDescription, pOrder.ProductEntityIdNavigation.Price, ByteArrayToBitmapImage(pOrder.ProductEntityIdNavigation.Picture), pOrder.ProductEntityIdNavigation.CategoriesIdNavigation.CategoryName);
                usersOrders.Add(new(PDTO, pOrder.CreatedTime));
            }
            UsersAccountsDTO loggedUserDTO;

            if (usersOrders.Count > 0)
            {
                loggedUserDTO = new(CurrentUsersAccount.Id, CurrentUsersAccount.Username, CurrentUsersAccount.Firstname, CurrentUsersAccount.Lastname, CurrentUsersAccount.Email, usersOrders);

            }
            else
            {
                loggedUserDTO = new(CurrentUsersAccount.Id, CurrentUsersAccount.Username, CurrentUsersAccount.Firstname, CurrentUsersAccount.Lastname, CurrentUsersAccount.Email);
            }
            loggedUsersOrders = loggedUserDTO.UsersOrders;
            return loggedUserDTO;

        }

        public void RegisterAccount()
        {
            UsersAccounts CurrentUsersAccount = UsersAccountsRepo.GetAllUserAccounts().FirstOrDefault(u => u.UsernamePasswrodIdNavigation.Username == Registry_Username_TB.Text);
            if (CurrentUsersAccount != null) throw new Exception("This Username is already exist");

            UsernamePassword NewUserPassword = new() { Username = Registry_Username_TB.Text, Password = Registry_Password_TB.Text };
            UserPasswordRepo.AddUserAccount(NewUserPassword);
            UsersAccounts NewAccount = new() { Username = Registry_Username_TB.Text, Firstname = Registry_Firstname_TB.Text, Lastname = Registry_Lastname_TB.Text, Email = Registry_Email_TB.Text, UsernamePasswordId = UserPasswordRepo.GetUserAccountById(NewUserPassword.Id).Id };
            UsersAccountsRepo.AddUserAccount(NewAccount);

            MessageBox.Show("Account has succesfully registrated");
            Login_Account_Grid.Visibility = Visibility.Visible;
            Profile_Account_Grid.Visibility = Visibility.Hidden;
            Register_Account_Grid.Visibility = Visibility.Hidden;
        }

        private byte[] selectedImageAsByte;

        public byte[] SelectedImageAsByte
        {
            get { return selectedImageAsByte; }
            set { selectedImageAsByte = value; OnPropertyChanged(nameof(selectedImageAsByte)); }
        }

        public MainWindow()
        {

            InitializeComponent();
            products = new();
            DataContext = this;

            FillAllProducts();
        }



        public event PropertyChangedEventHandler? PropertyChanged;


        public ObservableCollection<string> AddCategories(ICollection<Categories> categoriesCollection)
        {
            ObservableCollection<string> result = new();
            foreach (var item in categoriesCollection)
            {
                result.Add(item.CategoryName);
            }
            return result;
        }
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Open_Close_Product_Menu(object sender, RoutedEventArgs e)
        {

            if (Product_Menu.IsEnabled && Product_Menu.IsVisible)
            {
                Product_Menu.Visibility = Visibility.Hidden;
                Product_Menu.IsEnabled = false;
            }
            else
            {
                Product_Menu.Visibility = Visibility.Visible;
                Product_Menu.IsEnabled = true;
            }

        }
        private void Select_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.png; *.bmp)|*.jpg; *.png; *.bmp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                selectedImageAsByte = imageBytes;
                BitmapImage myImage = ByteArrayToBitmapImage(imageBytes);

                Add_New_Product_Selected_Image.Source = myImage;
            }
        }
        public BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(byteArray);
            bitmapImage.EndInit();

            return bitmapImage;
        }
        public void FillAllProducts()
        {
            Products = new();

            foreach (var item in ProductsRepo.GetAllProductEntities())
            {
                string PetDescription =
                    $"Id:{item.Id}\n" +
                    $"Category:{item.CategoriesIdNavigation.CategoryName}\n" +
                    $"Description:{item.ProductDescription}";

                Products.Add(new ProductDTO(item.Id, item.ProductName, PetDescription, item.Price, ByteArrayToBitmapImage(item.Picture), item.CategoriesIdNavigation.CategoryName));
            }

        }

        private void RegiesterNewAccount_Event(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Registry_Username_TB.Text == null || Registry_Firstname_TB.Text == null || Registry_Lastname_TB.Text == null || Registry_Email_TB.Text == null || Registry_Password_TB.Text == null) throw new Exception("Fill every text boxes");
                else
                {
                    RegisterAccount();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void LoginAccount_Event(object sender, RoutedEventArgs e)
        {
            try
            {

                LoggedUser = LogInCheck(Login_Username_TB.Text, Login_Password_TB.Text);
                Login_Account_Grid.Visibility = Visibility.Hidden;
                Products_Showup_SP.Visibility = Visibility.Visible;
                Profile_Account_Grid.Visibility = Visibility.Visible;
                Register_Account_Grid.Visibility = Visibility.Hidden;
                Menu_Open_Button.Visibility = Visibility.Visible;
                Open_Close_Product_Menu(sender, e);
            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message);
            }
        }

        private void LoginPanel_Register_Button(object sender, RoutedEventArgs e)
        {
            Login_Account_Grid.Visibility = Visibility.Hidden;
            Profile_Account_Grid.Visibility = Visibility.Hidden;
            Register_Account_Grid.Visibility = Visibility.Visible;
        }

        private void Back_To_LoginPage_Button_Click(object sender, RoutedEventArgs e)
        {
            Login_Account_Grid.Visibility = Visibility.Visible;
            Profile_Account_Grid.Visibility = Visibility.Hidden;
            Register_Account_Grid.Visibility = Visibility.Hidden;
        }

        private void Add_Product(object sender, RoutedEventArgs e)
        {
            try
            {

                ProductsRepo.AddProductEntity(new() { ProductName = Add_New_Product_Name_TB.Text, ProductDescription = Add_New_Product_Description_TB.Text, Picture = selectedImageAsByte, Price = Convert.ToInt32(Add_New_Product_Price_TB.Text), CategoriesId = Convert.ToInt32(Add_New_Product_Category_TB.Text) });

                FillAllProducts();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Add_Category_TextBox.Text == null) throw new Exception("Category name did not found");
                CategoryREpository.AddCategory(new() { CategoryName = Add_Category_TextBox.Text });
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Buy_Product(object sender, RoutedEventArgs e)
        {
            try
            {

                if (SelectedProduct != null)
                {
                    ProductsOrdersRepo.AddProductsOrder(new() { UsersAccountsId = loggedUser.Id, ProductEntityId = SelectedProduct.Id });
                    UsersOrdersDTO uoDTO = new(SelectedProduct, DateTime.Now);
                    loggedUser.UsersOrders.Add(uoDTO);
                    loggedUsersOrders = LoggedUser.UsersOrders;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
