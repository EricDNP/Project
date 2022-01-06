using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PracticePackAPI.Models;
using PracticePackAPI.Repositories;

namespace PracticePackAPI.Services
{
    public class UserService : iService<User>
    {
        private readonly iUnitOfWork _unitOfWork;

        public UserService(iUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public Task<ICollection<User>> GetAll()
        {
            return _unitOfWork.UserRepo.GetAll();
        }

        public ICollection<User> GetAllWithData(
            Expression<Func<User, bool>> filter = null,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null)
        {
            return _unitOfWork.UserRepo.GetAllWithData(filter, orderBy,
                includes: u => u.Include(u => u.Branch)
                                .Include(u => u.Adresses)
                                .Include(u => u.Packages)
                                .Include(u => u.Cards)
            ).ToList();
        }

        public Task<User> GetById(Guid Id)
        {
            return _unitOfWork.UserRepo.GetById(Id,
                includes: u => u.Include(u => u.Branch)
                                .Include(u => u.Adresses)
                                .Include(u => u.Packages)
                                .Include(u => u.Cards)
            );
        }

        public async Task<User> Create(User entity)
        {
            await _unitOfWork.UserRepo.Create(entity);
            await _unitOfWork.CommitChangesAsync();
            return entity;
        }

        public async Task<User> Update(User entity)
        {
            if(await _unitOfWork.UserRepo.Exist(entity.Id))
            {
                _unitOfWork.UserRepo.Update(entity);
                await _unitOfWork.CommitChangesAsync();
            }
            else
                return null;                
            return await _unitOfWork.UserRepo.GetById(entity.Id);
        }

        public async Task Delete(Guid Id)
        {
            await _unitOfWork.UserRepo.Delete(Id);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task<bool> Exist(Guid id)
        {
            return await _unitOfWork.UserRepo.Exist(id);
        }
    }
}