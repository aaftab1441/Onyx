using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Sixoclock.Onyx.AuthorizationStatuses.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.AuthorizationStatuses
{
    public class AuthorizationStatusAppService : OnyxAppServiceBase, IAuthorizationStatusAppService
    {
        private readonly IRepository<AuthorizationStatus> _authorizationStatusRepository;
        public AuthorizationStatusAppService(IRepository<AuthorizationStatus> authorizationStatusRepository)
        {
            _authorizationStatusRepository = authorizationStatusRepository;
        }
        public async Task CreateOrUpdateAuthorizationStatus(CreateOrUpdateAuthorizationStatusInput input)
        {
            var authorizationStatus = ObjectMapper.Map<AuthorizationStatus>(input);

            if (input.Id == 0)
            {
                await _authorizationStatusRepository.InsertAsync(authorizationStatus);
            }
            else
            {
                await _authorizationStatusRepository.UpdateAsync(authorizationStatus);
            }
        }
        public async Task<GetAuthorizationStatusForEditOutput> GetAuthorizationStatusForEdit(EntityDto<int> input)
        {
            //Editing an existing authorization Status
            var output = new GetAuthorizationStatusForEditOutput();
            if (input.Id == 0)
            {
                output.AuthorizationStatus = new AuthorizationStatusDto();
            }
            else
            {
                var authorizationStatus = await _authorizationStatusRepository.GetAsync(input.Id);

                output.AuthorizationStatus = ObjectMapper.Map<AuthorizationStatusDto>(authorizationStatus);
            }

            return output;
        }
        public GetAuthorizationStatussListOutput GetAuthorizationStatusesList()
        {
            IEnumerable<AuthorizationStatusListDto> _authorizationStatussList = from authorizationStatus in _authorizationStatusRepository.GetAll()
                                                        select new AuthorizationStatusListDto
                                                        {
                                                            Id = authorizationStatus.Id,
                                                            Name = authorizationStatus.Value
                                                        };
            return new GetAuthorizationStatussListOutput { AuthorizationStatuses = _authorizationStatussList.ToList() };
        }
        public ListResultDto<AuthorizationStatusDto> GetAuthorizationStatus(GetAuthorizationStatusInput input)
        {
            var authorizationStatuses = from authorizationStatus in _authorizationStatusRepository.GetAll()
            .WhereIf(!input.Filter.IsNullOrEmpty(),
                p => p.Value.ToLower().Contains(input.Filter.ToLower()) ||
                        p.Comment.ToLower().Contains(input.Filter.ToLower())
            )
            .WhereIf(!input.Name.IsNullOrEmpty(),
            p => p.Value.ToLower().Contains(input.Name.ToLower()))
            .WhereIf(!input.Comment.IsNullOrEmpty(),
            p => p.Comment.ToLower().Contains(input.Comment.ToLower()))
            .OrderBy(p => p.Value)
            .ThenBy(p => p.Comment)
                           select new AuthorizationStatusDto
                           {
                               Id = authorizationStatus.Id,
                               Value = authorizationStatus.Value,
                               Comment = authorizationStatus.Comment,
                               CreationTime = authorizationStatus.CreationTime
                           };

            return new ListResultDto<AuthorizationStatusDto>(ObjectMapper.Map<List<AuthorizationStatusDto>>(authorizationStatuses));
        }
        public async Task DeleteAuthorizationStatus(EntityDto<int> input)
        {
            var authorizationStatus = await _authorizationStatusRepository.GetAsync(input.Id);
            await _authorizationStatusRepository.DeleteAsync(authorizationStatus);
        }
    }
}
