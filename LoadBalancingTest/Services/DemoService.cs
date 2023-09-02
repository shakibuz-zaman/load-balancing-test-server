using System;
using LoadBalancingTest.Models;

namespace LoadBalancingTest.Services
{
	public class DemoService: IDemoService
	{
        private readonly IMongoRepository<UserInfo> _mailRepository;
        public DemoService(IMongoRepository<UserInfo> mailRepository)
        {
            _mailRepository = mailRepository;
        }

        public async Task SaveUserInfo(int cnt)
        {
            for (int i = 0; i < cnt; i++)
            {
                var user = GetUserInfoModel();
                await _mailRepository.InsertOneAsync(user);
            }
        }
        public async Task GetUserInfo(int cnt)
        {
            var users = await _mailRepository.GetFirstXItems(1);
            if(users != null && users.Count() > 0)
            {
                Console.WriteLine("-----User Found----");
                for (int i = 0; i < cnt; i++)
                {
                    var user = await _mailRepository.FindOneAsync(x => x.Id == users.First().Id); ;
                }
            }
        }

        private UserInfo GetUserInfoModel()
        {
            var user = new UserInfo()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                Father = "Test Father Name",
                Mother = "Test Mother Name",
                Address = "Test Address",
                Phone = "01712232323",
                BirthDate = DateTime.UtcNow,
            };
            return user;
        }

        public async Task ReadWriteUserInfo(int rCnt, int wCnt)
        {
            await SaveUserInfo(wCnt);
            await GetUserInfo(rCnt);
        }

        public async Task<IEnumerable<UserInfo>> GetFirstXUserInfo(int cnt)
        {
            return await _mailRepository.GetFirstXItems(cnt);
        }
    }

    public interface IDemoService
    {
        Task SaveUserInfo(int cnt);
        Task ReadWriteUserInfo(int rCnt, int wCnt);
        Task GetUserInfo(int cnt);
        Task<IEnumerable<UserInfo>> GetFirstXUserInfo(int cnt);
    }
}

