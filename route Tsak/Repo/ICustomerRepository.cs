using route_Tsak.Models;

namespace route_Tsak.Repo
{
    public interface ICustomerRepository 
    {
        List<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
        Task AddAsync(ApplicationUser customer);
        Task UpdateAsync(ApplicationUser customer);
        Task DeleteAsync(ApplicationUser customer);


    }
}
