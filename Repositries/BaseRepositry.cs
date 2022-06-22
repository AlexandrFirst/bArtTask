using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using testWork.Abstractions;
using testWork.Data;

namespace testWork.Repositries
{
    public abstract class BaseRepositry<T, L> : IRepositry<T> where T : class
    {
        protected readonly DataContext context;
        protected readonly ILogger<L> logger;
        public BaseRepositry(DataContext context,
                             ILogger<L> logger)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                context.Add(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception on adding: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                context.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                logger.LogError("Exception on deleting: " + ex.Message);
                return false;
            }
            return true;
        }

        public abstract Task<T> Read(Guid id);

        public async Task<bool> Update(T entity)
        {
            try
            {
                context.Update(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception on updating: " + ex.Message);
                return false;
            }
        }
    }
}