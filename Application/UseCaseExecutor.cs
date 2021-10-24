using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class UseCaseExecutor
    {
        public TResult ExecuteQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
        {
            /*logger.Log(query, user, search);

            if (!user.AllowedUseCases.Contains(query.Id))
            {
                throw new UnauthorizedUseCaseException(query, user);
            }*/

            return query.Execute(search);
        }

        public void ExecuteCommand<TRequest>(
            ICommand<TRequest> command,
            TRequest request)
        {
            /*logger.Log(command, user, request);

            if (!user.AllowedUseCases.Contains(command.Id))
            {
                throw new UnauthorizedUseCaseException(command, user);
            }*/

            command.Execute(request);

        }
    }
}
