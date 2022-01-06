using System.Linq.Expressions;
using PracticePackAPI.Models;
using PracticePackAPI.Repositories;

namespace PracticePackAPI.Services
{
    public class CardService : iService<Card>
    {
        private readonly iUnitOfWork _unitOfWork; 

        public CardService(iUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public Task<ICollection<Card>> GetAll()
        {
            return _unitOfWork.CardRepo.GetAll();
        }

        public ICollection<Card> GetAllWithData(
            Expression<Func<Card, bool>> filter = null,
            Func<IQueryable<Card>, IOrderedQueryable<Card>> orderBy = null)
        {
            return _unitOfWork.CardRepo.GetAllWithData(filter, orderBy).ToList();
        }

        public Task<Card> GetById(Guid Id)
        {
            return _unitOfWork.CardRepo.GetById(Id);
        }

        public async Task<Card> Create(Card entity)
        {
            await _unitOfWork.CardRepo.Create(entity);
            await _unitOfWork.CommitChangesAsync();
            return entity;
        }

        public async Task<Card> Update(Card entity)
        {
            if(await _unitOfWork.CardRepo.Exist(entity.Id))
            {
                _unitOfWork.CardRepo.Update(entity);
                await _unitOfWork.CommitChangesAsync();
            }
            else
                return null;                
            return await _unitOfWork.CardRepo.GetById(entity.Id);
        }

        public async Task Delete(Guid Id)
        {
            await _unitOfWork.CardRepo.Delete(Id);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task<bool> Exist(Guid id)
        {
            return await _unitOfWork.CardRepo.Exist(id);
        }
    }
}