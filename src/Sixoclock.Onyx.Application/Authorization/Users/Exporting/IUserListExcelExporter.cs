using System.Collections.Generic;
using Sixoclock.Onyx.Authorization.Users.Dto;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}