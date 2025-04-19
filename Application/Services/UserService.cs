using TaskManagement.Application.Dtos;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Application.Services
{
    public class UserService : IUserService
    {
        //do not forget validation & bassiness logic

        public IUnitOfWork UnitOfWork { get; }
        public UserService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

      

        public async Task AddUserAsync(User user)
        {
             await UnitOfWork.Users.AddAsync(user);
         
            
        }

        public async Task DeleteUserAsync(int id)
        {
            await UnitOfWork.Users.DeleteAsync(id);
            
        }

        public async Task<List<User>> GetAllUsersAsync()
        {  //for testing
           return await UnitOfWork.Users.GetAllAsync(u=>u.Tasks);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await UnitOfWork.Users.GetByNameAsync(name);
             
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
           return await  UnitOfWork.Users.GetByIdAsync(id);
        }

        public async Task UpdateUserAsync( int id,UserDto userDto  )
        {
     
            var user = await UnitOfWork.Users.GetByIdAsync(id  );
            if (user == null)
                throw new Exception("user not found");

             user.UserName= userDto.Name;
            user.Email= userDto.Email;
            user.PasswordHash = userDto.Password;
        
            await UnitOfWork.Users.UpdateAsync(user);
        }
    }
}
