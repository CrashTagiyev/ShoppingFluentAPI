using ShoppingDataAccess.DTO.OrdersDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDataAccess.DTO.UsersDTO
{
    public class UsersAccountsDTO : INotifyPropertyChanged
    {
        public UsersAccountsDTO(int id, string username, string firstname, string lastname, string email)
        {
            Id = id;
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            UsersOrders = new();
        }

        public UsersAccountsDTO(int id, string username, string firstname, string lastname, string email, ObservableCollection<UsersOrdersDTO> usersOrders) : this(id, username, firstname, lastname, email)
        {
            UsersOrders = usersOrders;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }



        private ObservableCollection<UsersOrdersDTO> usersOrders;

        public ObservableCollection<UsersOrdersDTO> UsersOrders
        {
            get { return usersOrders; }
            set { usersOrders = value;OnPropertyChanged(nameof(usersOrders)); }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
