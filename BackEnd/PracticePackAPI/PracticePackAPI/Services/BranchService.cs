using System.Linq.Expressions;
using PracticePackAPI.Models;
using PracticePackAPI.Repositories;

namespace PracticePackAPI.Services
{
    public class BranchService : iService<Branch>
    {
        private readonly iUnitOfWork _unitOfWork;

        public BranchService(iUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public Task<ICollection<Branch>> GetAll()
        {
            return _unitOfWork.BranchRepo.GetAll();
        }

        public ICollection<Branch> GetAllWithData(
            Expression<Func<Branch, bool>> filter = null,
            Func<IQueryable<Branch>, IOrderedQueryable<Branch>> orderBy = null)
        {
            return _unitOfWork.BranchRepo.GetAllWithData(filter, orderBy).ToList();
        }

        public Task<Branch> GetById(Guid Id)
        {
            return _unitOfWork.BranchRepo.GetById(Id);
        }

        public async Task<Branch> Create(Branch entity)
        {
            await _unitOfWork.BranchRepo.Create(entity);
            await _unitOfWork.CommitChangesAsync();
            return entity;
        }

        public async Task<Branch> Update(Branch entity)
        {
            if(await _unitOfWork.BranchRepo.Exist(entity.Id))
            {
                _unitOfWork.BranchRepo.Update(entity);
                await _unitOfWork.CommitChangesAsync();
            }
            else
                return null;                
            return await _unitOfWork.BranchRepo.GetById(entity.Id);
        }

        public async Task Delete(Guid Id)
        {
            await _unitOfWork.BranchRepo.Delete(Id);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task<bool> Exist(Guid id)
        {
            return await _unitOfWork.BranchRepo.Exist(id);
        }
    }
}