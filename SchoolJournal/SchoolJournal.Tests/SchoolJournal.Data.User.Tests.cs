using SchoolJournal.Data;
using SchoolJournal.Data.Repos;
using SchoolJournal.Models.DB;

namespace SchoolJournal.Tests
{
    [TestClass]
    public class SchoolJournalDataUserTests
    {
        private BaseTest baseTest;
        SchoolJournalDBContext? dBContext;
        IUserRepository userRepository;

        public SchoolJournalDataUserTests()
        {
            baseTest = new BaseTest();
            dBContext = new SchoolJournalDBContext(baseTest.dbContextOptions);
            userRepository = new UserRepository(dBContext);
        }

        [TestMethod]
        public async Task UserRepo_Create()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await baseTest.AddRolesToDatabase(dBContext);

            //Run method
            await userRepository.Create(new User
            {
                Email = "test@mail.ru",
                Name = "TestName",
                Password = "TestPassword"
            });

            //Analyze
            Assert.IsTrue(dBContext.Users.Count() == 1, "User was not created");
            Assert.AreEqual(dBContext.Roles.Where(r => r.Name == "user").FirstOrDefault(),
                            dBContext.Users.FirstOrDefault().Role,
                            "Wrong role is assigned to user");
        }

        [TestMethod]
        public async Task UserRepo_GetByEmailPassword()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await baseTest.AddRolesToDatabase(dBContext);

            await userRepository.Create(new User
            {
                Email = "test@mail.ru",
                Name = "TestName",
                Password = "TestPassword"
            });

            //Run method
            var user = await userRepository.GetByEmailPassword("test@mail.ru", "TestPassword");

            //Analyze
            Assert.IsTrue(dBContext.Users.Count() == 1, "User was not created");
            Assert.AreEqual(user.Id,
                            dBContext.Users.FirstOrDefault().Id,
                            "Wrong user is returned");
        }

        [TestMethod]
        public async Task UserRepo_GetByEmail()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await baseTest.AddRolesToDatabase(dBContext);

            await userRepository.Create(new User
            {
                Email = "test@mail.ru",
                Name = "TestName",
                Password = "TestPassword"
            });

            //Run method
            var user = await userRepository.GetByEmail("test@mail.ru");

            //Analyze
            Assert.IsTrue(dBContext.Users.Count() == 1, "User was not created");
            Assert.AreEqual(user.Id,
                            dBContext.Users.FirstOrDefault().Id,
                            "Wrong user is returned");
        }
    }
}