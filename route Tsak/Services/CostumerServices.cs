using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using route_Tsak.Dto;
using route_Tsak.Models;
using route_Tsak.Repo;
using route_Tsak.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace route_Tsak.Services
{
    public class CustomerService : IcostumerServices
    {
        public readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository _customerrepo) { 
            _customerRepository = _customerrepo;
        }

        //public async Task AddCustomerAsync(Costumerdto customerDto)
        //{
        //    var customer = new ApplicationUser();
        //    customer.Name = customerDto.Name;
        //    customer.Email = customerDto.Email;
        //    await _customerRepository.AddAsync(customer);
        //}

        public Task DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Costumerdto> GetAllCustomers()
        {
            List<ApplicationUser> customers = _customerRepository.GetAll();
            List<Costumerdto> custom = new List<Costumerdto>();
            foreach (var customer in customers)
            {
                Costumerdto customerDto = new Costumerdto()
                {
                    Name = customer.UserName,
                    Email = customer.Email,
                };
                custom.Add(customerDto);
            }
            return custom;
        }

        public Costumerdto GetCustomerById(string id)
        {
            var result = _customerRepository.GetById(id);
            var cus = new Costumerdto();
            cus.Name = result.UserName;
            cus.Email = result.Email;
            return cus;

        }
    }
}