using System.Collections.Generic;
using Tccp.PlayBall.GroupManagement.Business.Models;

namespace Tccp.PlayBall.GroupManagement.Business.Services
{
    public interface IGroupsService
    {
        IReadOnlyCollection<Group> GetAll();

        Group GetById(long id);

        Group Update(Group group);

        Group Add(Group group);
    }
}