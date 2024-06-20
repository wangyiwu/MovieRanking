using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Common
{
	public interface IRepositoryFactory
	{
		IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
	}
}
