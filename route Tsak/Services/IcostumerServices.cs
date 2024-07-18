using Microsoft.AspNetCore.Mvc;
using route_Tsak.Dto;
using route_Tsak.Models;

namespace route_Tsak.Services
{
    public interface IcostumerServices
    {
        List<Costumerdto> GetAllCustomers();
        Costumerdto GetCustomerById(string id);
        //Task AddCustomerAsync(Costumerdto customerDto);
        //Task UpdateCustomerAsync(Costumerdto customerDto);
        Task DeleteCustomerAsync(int id);
    }
}
